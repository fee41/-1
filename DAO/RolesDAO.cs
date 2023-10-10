using Dapper;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class RolesDAO
    {
        private string zfc = "Data Source=.;Initial Catalog=HR_DB;Integrated Security=True";

        /// <summary>
        /// 查询所有数据
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Roles>> ChaSyAsync()
        {
            using (SqlConnection con = new SqlConnection(zfc))
            {
                string sql = "SELECT * FROM [dbo].[Roles]";
                return await con.QueryAsync<Roles>(sql);
            }
        }

        /// <summary>
        /// 进行分页查询
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="currentPage"></param>
        /// <returns></returns>
        public async Task<FenYe<Roles>> FenYe(int pageSize, int currentPage)
        {
            using (SqlConnection sqlConnection = new SqlConnection(zfc))
            {
                DynamicParameters dd = new DynamicParameters();
                dd.Add("pageSize", pageSize);
                dd.Add("keyName", "RolesID");
                dd.Add("tabelName", "Roles");
                dd.Add("where", "1=1");
                dd.Add("currentPage", currentPage);
                dd.Add("lie", "*");
                dd.Add("rows", direction: ParameterDirection.Output, dbType: DbType.Int32);

                string sql = "exec [dbo].[proc_Fenye] @pageSize,@keyName,@tabelName,@where,@currentPage,@lie,@rows out";
                IEnumerable<Roles> list = await sqlConnection.QueryAsync<Roles>(sql, dd);
                int row = dd.Get<int>("rows");
                FenYe<Roles> fenYe = new FenYe<Roles>();
                fenYe.CList = list;
                fenYe.currentPage = currentPage;
                fenYe.Totalpage = row % 5 == 0 ? row / 5 : row / 5 + 1;
                fenYe.Totalnumber = row;
                return fenYe;
            }
        }

        /// <summary>
        /// 进行添加
        /// </summary>
        /// <returns></returns>
        public async Task<int> Tian(Roles roles)
        {
            using (SqlConnection sqlConnection = new SqlConnection(zfc))
            {
                string sql = $"INSERT INTO  [dbo].[Roles](RolesName, RolesInstruction, RolesIf) VALUES ('{roles.RolesName}','{roles.RolesInstruction}','{roles.RolesIf}')";
                return await sqlConnection.ExecuteAsync(sql);
            }
        }

        /// <summary>
        /// 进行删除
        /// </summary>
        /// <returns></returns>
        public async Task<int> ScAsync(int id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(zfc))
            {
                string sql = $"DELETE FROM [dbo].[Roles] WHERE RolesID = '{id}'";
                return await sqlConnection.ExecuteAsync(sql);
            }
        }

        /// <summary>
        /// 进行查询具体信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Roles> ChaJuAsync(int id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(zfc))
            {
                string sql = $"SELECT * FROM [dbo].[Roles] WHERE RolesID = '{id}'";
                return await sqlConnection.QueryFirstAsync<Roles>(sql);
            }
        }

        public async Task<IEnumerable<Tree>> GetTree()
        {
            using (SqlConnection connection = new SqlConnection(zfc))
            {
                string sql = "SELECT JuriID,JurName,Pid FROM [dbo].[Jurisdiction]";
                IEnumerable<Jurisdiction> quans = await connection.QueryAsync<Jurisdiction>(sql);
                IEnumerable<Tree> trees = new List<Tree>();
                //把quans转换为trees的结构

                trees = GetTreeData(quans, 0);//获取父级
                return trees;
            }
        }

        private List<Tree> GetTreeData(IEnumerable<Jurisdiction> list, int pid)
        {
            List<Tree> tress = new List<Tree>();
            //根据pid做数据过滤
            List<Jurisdiction> quans = list.Where(e => e.Pid == pid).ToList();
            foreach (Jurisdiction item in quans)
            {
                Tree tree = new Tree()
                {
                    Id = item.JuriID,
                    authName = item.JurName,

                    children = GetTreeData(list, item.JuriID)
                };

                tress.Add(tree);
            }
            return tress;
        }

        /// <summary>
        ///进行修改的事务
        /// </summary>
        /// <param name="jin"></param>
        /// <param name="name"></param>
        /// <param name="bh"></param>
        /// <param name="salary"></param>
        /// <returns></returns>
        public async Task<int> Xiu(Roles roles, string zi)
        {
            using (SqlConnection sqlConnection = new SqlConnection(zfc))
            {
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@RolesID", roles.RolesID);
                dynamicParameters.Add("@NumberString", zi);
                dynamicParameters.Add("@RolesName", roles.RolesName);
                dynamicParameters.Add("@RolesInstruction", roles.RolesInstruction);
                dynamicParameters.Add("@RolesIf", roles.RolesIf);
                return await sqlConnection.ExecuteAsync("Qx", dynamicParameters, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<List<Tree>> GetList(int rid)
        {
            using (SqlConnection ss = new SqlConnection(zfc))
            {
                string sql = $@"select j.JuriID, JurName, GroupID, JurAddress, Pid from [dbo].[Jurisdiction] j inner join [dbo].[RolesJurisdiction] r on j.JuriID=r.JuriID where r.RolesID='{rid}'";
                IEnumerable<Jurisdiction> quans = await ss.QueryAsync<Jurisdiction>(sql);
                List<Tree> trees = GetTreeData1(quans, 0);
                return trees;
            }
        }

        /// <summary>
        /// 递归调用权限数据
        /// </summary>
        /// <param name="list"></param>
        /// <param name="pid"></param>
        /// <returns></returns>

        private List<Tree> GetTreeData1(IEnumerable<Jurisdiction> list, int pid)
        {
            List<Tree> trees = new List<Tree>();
            List<Jurisdiction> plist = list.Where(e => e.Pid == pid).ToList();
            foreach (Jurisdiction item in plist)
            {
                Tree trees1 = new Tree()
                {
                    Id = item.JuriID,
                    authName = item.JurName,
                    Url = item.JurAddress,
                    children = GetTreeData1(list, item.JuriID)
                };
                trees.Add(trees1);
            }
            return trees;
        }
    }
}