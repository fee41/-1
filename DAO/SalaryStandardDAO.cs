using Dapper;
using Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class SalaryStandardDAO
    {
        private string zfc = "Data Source=.;Initial Catalog=HR_DB;Integrated Security=True";

        public async Task<IEnumerable<SalaryStandard>> showALL()
        {
            using (SqlConnection con = new SqlConnection(zfc))
            {
                string sql = "  select * from [dbo].[salary_standard]";
                return await con.QueryAsync<SalaryStandard>(sql);
            }
        }

        /// <summary>
        /// 进行添加事务
        /// </summary>
        /// <param name="jin"></param>
        /// <param name="name"></param>
        /// <param name="bh"></param>
        /// <param name="salary"></param>
        /// <returns></returns>
        public async Task<int> Tian(string jin, string name, string bh, SalaryStandard salary)
        {
            using (SqlConnection sqlConnection = new SqlConnection(zfc))
            {
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@standard_id", salary.standard_id);
                dynamicParameters.Add("@standard_name", salary.standard_name);
                dynamicParameters.Add("@item_ids", bh);
                dynamicParameters.Add("@item_names", name);
                dynamicParameters.Add("@salaries", jin);
                dynamicParameters.Add("@designer", salary.designer);
                dynamicParameters.Add("@register", salary.register);
                dynamicParameters.Add("@regist_time", salary.regist_time);
                dynamicParameters.Add("@remark", salary.remark);
                dynamicParameters.Add("@salary_sum", salary.salary_sum);
                return await sqlConnection.ExecuteAsync("xinshiwu", dynamicParameters, commandType: CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// 进行分页显示
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="currentPage"></param>
        /// <returns></returns>
        public async Task<FenYe<SalaryStandard>> FenYe(int pageSize, int currentPage)
        {
            using (SqlConnection sqlConnection = new SqlConnection(zfc))
            {
                DynamicParameters dd = new DynamicParameters();
                dd.Add("pageSize", pageSize);
                dd.Add("keyName", "ssd_id");
                dd.Add("tabelName", "salary_standard");
                dd.Add("where", "change_status = 1");
                dd.Add("currentPage", currentPage);
                dd.Add("lie", "*");
                dd.Add("rows", direction: ParameterDirection.Output, dbType: DbType.Int32);

                string sql = "exec [dbo].[proc_Fenye] @pageSize,@keyName,@tabelName,@where,@currentPage,@lie,@rows out";
                IEnumerable<SalaryStandard> list = await sqlConnection.QueryAsync<SalaryStandard>(sql, dd);
                int row = dd.Get<int>("rows");
                FenYe<SalaryStandard> fenYe = new FenYe<SalaryStandard>();
                fenYe.CList = list;
                fenYe.currentPage = currentPage;
                fenYe.Totalpage = row % 5 == 0 ? row / 5 : row / 5 + 1;
                fenYe.Totalnumber = row;
                return fenYe;
            }
        }

        /// <summary>
        /// 进行查询具体信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<SalaryStandard> ChaYi(string id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(zfc))
            {
                string sql = $"SELECT * FROM [dbo].[salary_standard] WHERE standard_id = '{id}'";
                return await sqlConnection.QueryFirstAsync<SalaryStandard>(sql);
            }
        }

        /// <summary>
        /// 进行查询单号
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<SalaryStandard> ChaD(string id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(zfc))
            {
                string sql = $"SELECT * FROM [dbo].[salary_standard] WHERE ssd_id = '{id}'";
                return await sqlConnection.QueryFirstAsync<SalaryStandard>(sql);
            }
        }

        /// <summary>
        /// 进行修改的事务
        /// </summary>
        /// <returns></returns>
        public async Task<int> Xiu(string id, string jin, SalaryStandard salary)
        {
            using (SqlConnection sqlConnection = new SqlConnection(zfc))
            {
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@sdt_ids", id);
                dynamicParameters.Add("@salaries", jin);
                dynamicParameters.Add("@standard_id", salary.standard_id);
                dynamicParameters.Add("@standard_name", salary.standard_name);
                dynamicParameters.Add("@salary_sum", salary.salary_sum);
                dynamicParameters.Add("@designer", salary.designer);
                dynamicParameters.Add("@checker", salary.checker);
                dynamicParameters.Add("@check_time", salary.check_time);
                dynamicParameters.Add("@check_comment", salary.check_comment);
                dynamicParameters.Add("@check_status", salary.check_status);
                return await sqlConnection.ExecuteAsync("xiushiwu", dynamicParameters, commandType: CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// 进行模糊查询
        /// </summary>
        /// <param name="bh"></param>
        /// <param name="name"></param>
        /// <param name="sj1"></param>
        /// <param name="sj2"></param>
        /// <returns></returns>
        public async Task<FenYe<SalaryStandard>> MoHuCha(int pageSize, int currentPage, string tiao)
        {
            using (SqlConnection sqlConnection = new SqlConnection(zfc))
            {
                DynamicParameters dd = new DynamicParameters();

                dd.Add("pageSize", pageSize);
                dd.Add("keyName", "ssd_id");
                dd.Add("tabelName", "salary_standard");
                dd.Add("where", tiao);
                dd.Add("currentPage", currentPage);
                dd.Add("lie", "*");
                dd.Add("rows", direction: ParameterDirection.Output, dbType: DbType.Int32);

                string sql = "exec [dbo].[proc_Fenye] @pageSize,@keyName,@tabelName,@where,@currentPage,@lie,@rows out";
                IEnumerable<SalaryStandard> list = await sqlConnection.QueryAsync<SalaryStandard>(sql, dd);
                int row = dd.Get<int>("rows");
                FenYe<SalaryStandard> fenYe = new FenYe<SalaryStandard>();
                fenYe.CList = list;
                fenYe.currentPage = currentPage;
                fenYe.Totalpage = row % 5 == 0 ? row / 5 : row / 5 + 1;
                fenYe.Totalnumber = row;
                return fenYe;
            }
        }

        /// <summary>
        /// 进行更改的事务
        /// </summary>
        /// <returns></returns>
        public async Task<int> Gen(string id, string jin, SalaryStandard salary)
        {
            using (SqlConnection sqlConnection = new SqlConnection(zfc))
            {
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@sdt_ids", id);
                dynamicParameters.Add("@salaries", jin);
                dynamicParameters.Add("@standard_id", salary.standard_id);
                dynamicParameters.Add("@standard_name", salary.standard_name);
                dynamicParameters.Add("@salary_sum", salary.salary_sum);
                dynamicParameters.Add("@designer", salary.designer);
                dynamicParameters.Add("@changer", salary.changer);
                dynamicParameters.Add("@change_time ", salary.change_time);
                dynamicParameters.Add("@remark", salary.remark);
                return await sqlConnection.ExecuteAsync("genshiwu", dynamicParameters, commandType: CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// 进行查询薪酬标准单名称
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<SalaryStandard>> Cha2()
        {
            using (SqlConnection sqlConnection = new SqlConnection(zfc))
            {
                string sql = "SELECT ssd_id,standard_name FROM [dbo].[salary_standard] WHERE change_status =2";
                return await sqlConnection.QueryAsync<SalaryStandard>(sql);
            }
        }

        /// <summary>
        /// 进行查询总金额
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<int> Chajin(string id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(zfc))
            {
                string sql = $"SELECT salary_sum FROM [dbo].[salary_standard] WHERE ssd_id = '{id}'";
                return await sqlConnection.QueryFirstAsync<int>(sql);
            }
        }
    }
}