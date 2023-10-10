using Dapper;
using Microsoft.Win32;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DAO
{
    public class Engage_Major_ReleaseDAO
    {
        private string constr = "Data Source=.;Initial Catalog=HR_DB;Integrated Security=True";

        /// <summary>
        ///
        /// 职位发布变更的id查询
        ///
        /// </summary>
        /// <param name="engage"></param>
        /// <returns></returns>
        public async Task<Engage_Major_Release> Engage_Major_Release___idcx(int id)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                string sql = $" select * from engage_major_release  where mre_id={id}";

                return await con.QueryFirstAsync<Engage_Major_Release>(sql);
            }
        }

        /// <summary>
        ///
        /// 职位发布变更的删除
        ///
        /// </summary>
        /// <param name="engage"></param>
        /// <returns></returns>
        public async Task<int> Engage_Major_Release___DEL(int id)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                string sql = $" delete from engage_major_release  where mre_id={id}";
                return await con.ExecuteAsync(sql);
            }
        }

        /// <summary>
        ///
        /// 职位发布变更的修改
        ///
        /// </summary>
        /// <param name="engage"></param>
        /// <returns></returns>
        public async Task<int> update_in(Engage_Major_Release engage)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                string sql = $@"update [dbo].[engage_major_release] set
            human_amount='{engage.human_amount}',deadline='{engage.deadline}',changer='{engage.changer}',change_time='{engage.change_time}',engage_type='{engage.engage_type}'
           ,major_describe='{engage.major_describe}' where mre_id='{engage.mre_id}'";
                return await con.ExecuteAsync(sql);
            }
        }

        /// <summary>
        ///
        ///   查询职位发布变更
        /// </summary>
        /// <param name="fY"></param>
        /// <returns></returns>
        public async Task<FY<Engage_Major_Release>> FenYe(int? currentPage)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                if (currentPage == null)
                {
                    currentPage = 1;
                }
                DynamicParameters parms = new DynamicParameters();
                parms.Add("@currentPage", currentPage);
                parms.Add("@keyName", "mre_id");
                parms.Add("@tableName", "engage_major_release");
                parms.Add("@pageSize", 2);
                parms.Add("@rows", direction: ParameterDirection.Output, dbType: DbType.Int32);
                string sql = "exec FY @pageSize, @keyName, @tableName, @currentPage, @rows out";
                FY<Engage_Major_Release> fY = new FY<Engage_Major_Release>();
                fY.List = await con.QueryAsync<Engage_Major_Release>(sql, parms);
                fY.Rows = parms.Get<int>("rows");
                fY.CurrentPage = (int)currentPage;
                return fY;
            }
        }

        public async Task<IEnumerable<config_file_first_kind>> yiji()
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                string sql = "SELECT * FROM [dbo].[config_file_first_kind]";
                return await con.QueryAsync<config_file_first_kind>(sql);
            }
        }

        public async Task<IEnumerable<FileSecondKind>> erji()
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                string sql = "SELECT * FROM [dbo].[config_file_second_kind]";
                return await con.QueryAsync<FileSecondKind>(sql);
            }
        }

        public async Task<IEnumerable<ConfigFileThirdKind>> sanji()
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                string sql = "SELECT * FROM [dbo].[config_file_third_kind]";
                return await con.QueryAsync<ConfigFileThirdKind>(sql);
            }
        }

        public async Task<int> Add(Engage_Major_Release s)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                string sql = $@"INSERT INTO [dbo].[engage_major_release](
                 first_kind_id, first_kind_name, second_kind_id, second_kind_name,
                 third_kind_id, third_kind_name,
            major_id, major_name, human_amount, engage_type,
            deadline, register, changer, regist_time,
             major_describe, engage_required,major_kind_name,major_kind_id)
          VALUES ({s.first_kind_id},'{s.first_kind_name}',{s.second_kind_id},'{s.second_kind_name}'
                ,{s.third_kind_id},'{s.third_kind_name}'
            ,{s.major_id},'{s.major_name}',{s.human_amount},'{s.engage_type}',
            '{s.deadline}','{s.register}','{s.changer}','{s.regist_time}',
            '{s.major_describe}','{s.engage_required}','{s.major_kind_name}','{s.major_kind_id}')";
                return await con.ExecuteAsync(sql);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string secondname(int id)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                string name = "";
                string sql = $"select * from [dbo].[config_major] where major_kind_id={id}";
                List<ConfigFileThirdKind> list = new List<ConfigFileThirdKind>();
                ConfigFileThirdKind config = new ConfigFileThirdKind();
                list = con.Query<ConfigFileThirdKind>(sql).ToList();
                name = list[0].first_kind_name;
                return name;
            }
        }

        public string secondname1(int id)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                string name = "";
                string sql = $"SELECT * FROM [dbo].[config_major_kind] where [major_kind_id]={id}";
                List<Config_Major_Kind> list = new List<Config_Major_Kind>();
                Config_Major config = new Config_Major();
                list = con.Query<Config_Major_Kind>(sql).ToList();
                name = list[0].Major_kind_name;

                return name;
            }
        }

        /// <summary>
        ///
        /// 根据职位分类id查询职位分类名称
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string zhiweifenlei2(int id)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                string sql = $"select* from[dbo].[config_major] where major_id={id} ";
                List<Config_Major_Kind> list = new List<Config_Major_Kind>();
                Config_Major_Kind config = new Config_Major_Kind();
                list = con.Query<Config_Major_Kind>(sql).ToList();
                string name = list[0].Major_kind_name;
                return name;
            }
        }

        //根据一级id 查询一级名称
        public string namese1(int id)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                string name = "";
                string sql = $"SELECT * FROM [dbo].[config_file_first_kind] where  [first_kind_id]={id}";
                List<FileSecondKind> list = new List<FileSecondKind>();
                FileSecondKind config = new FileSecondKind();
                list = con.Query<FileSecondKind>(sql).ToList();
                name = list[0].first_kind_name;
                return name;
            }
        }

        //根据二级id 查询二级名称
        public string namese2(int id)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                string name = "";
                string sql = $"select * from [dbo].[config_file_second_kind] where second_kind_id ={id}";
                List<FileSecondKind> list = new List<FileSecondKind>();
                FileSecondKind config = new FileSecondKind();
                list = con.Query<FileSecondKind>(sql).ToList();
                name = list[0].second_kind_name;

                return name;
            }
        }

        /// <summary>
        ///    根据三级id 查询三级id名称
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string namese3(int id)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                string name = "";
                string sql = $"select * from [dbo].[config_file_third_kind] where [third_kind_id]={id}";
                List<ConfigFileThirdKind> list = new List<ConfigFileThirdKind>();
                ConfigFileThirdKind config = new ConfigFileThirdKind();
                list = con.Query<ConfigFileThirdKind>(sql).ToList();
                name = list[0].third_kind_name;

                return name;
            }
        }

        //public async Task<IEnumerable<LianJi>> SanAsync(string id)
        //{
        //    using (SqlConnection sqlConnection = new SqlConnection(constr))
        //    {
        //        string sql = $"select [third_kind_name],[third_kind_sale_id] from[dbo].[config_file_third_kind] where [third_kind_id]={id}";
        //        IEnumerable<ConfigFileThirdKind> f = await sqlConnection.QueryAsync<ConfigFileThirdKind>(sql);
        //        List<LianJi> jis = new List<LianJi>();
        //        foreach (ConfigFileThirdKind fileSecondKind in f)
        //        {
        //            LianJi ji = new LianJi()
        //            {
        //                value = fileSecondKind.third_kind_sale_id,
        //                 label = fileSecondKind.third_kind_name,
        //            };
        //            jis.Add(ji);
        //        }
        //        return jis;
        //    }
        //}
        ///// <summary>
        ///// 根据一级查耳二级
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //public async Task<IEnumerable<LianJi>> ChaEYAsync(string id)
        //{
        //    using (SqlConnection sqlConnection = new SqlConnection(constr))
        //    {
        //        string sql = $"SELECT second_kind_id,second_kind_name FROM [dbo].[config_file_second_kind] WHERE first_kind_id = '{id}'";
        //        IEnumerable<FileSecondKind> f = await sqlConnection.QueryAsync<FileSecondKind>(sql);
        //        List<LianJi> jis = new List<LianJi>();
        //        foreach (FileSecondKind fileSecondKind in f)
        //        {
        //            LianJi ji = new LianJi()
        //            {
        //                value = fileSecondKind.second_kind_id,
        //                label = fileSecondKind.second_kind_name,
        //                children = await SanAsync(fileSecondKind.second_kind_id)

        //            };
        //            jis.Add(ji);
        //        }
        //        return jis;
        //    }
        //}

        ///// <summary>
        ///// 进行级联查
        ///// </summary>
        ///// <returns></returns>
        //public async Task<IEnumerable<LianJi>> ChaLian()
        //{
        //    using (SqlConnection sqlConnection = new SqlConnection(constr))
        //    {
        //        string sql = "SELECT * FROM [dbo].[config_file_first_kind]";
        //        IEnumerable<First_kind> firsts = await sqlConnection.QueryAsync<First_kind>(sql);
        //        List<LianJi> jis = new List<LianJi>();
        //        foreach (First_kind first in firsts)
        //        {
        //            LianJi lian = new LianJi()
        //            {
        //                value = first.first_kind_id,
        //                label = first.first_kind_name,
        //                children = await ChaEYAsync(first.first_kind_id)
        //            };
        //            jis.Add(lian);
        //        }
        //        return jis;
        //    }
        //}
    }
}