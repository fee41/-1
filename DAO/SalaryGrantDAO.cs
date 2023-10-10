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
    public class SalaryGrantDAO
    {
        private string zfc = "Data Source=.;Initial Catalog=HR_DB;Integrated Security=True";

        /// <summary>
        /// 进行筛选
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IEnumerable<SalaryGrant>> SXuanAsync(string id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(zfc))
            {
                string sql = "SELECT * FROM [dbo].[salary_grant]";
                if (id == "1")
                {
                    sql += " WHERE first_kind_id is not NULL ";
                }
                else if (id == "2")
                {
                    sql += " WHERE second_kind_id is not NULL";
                }
                else if (id == "3")
                {
                    sql += " WHERE third_kind_id is not NULL ";
                }
                sql += " AND check_status = 0";
                return await sqlConnection.QueryAsync<SalaryGrant>(sql);
            }
        }

        /// <summary>
        /// 进行查询总人数
        /// </summary>
        /// <returns></returns>
        public async Task<string> ChaZongRenAsync(string id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(zfc))
            {
                string sql = "SELECT SUM(human_amount) FROM [dbo].[salary_grant]";
                if (id == "1")
                {
                    sql += " WHERE first_kind_id is not NULL ";
                }
                else if (id == "2")
                {
                    sql += " WHERE second_kind_id is not NULL";
                }
                else if (id == "3")
                {
                    sql += " WHERE third_kind_id is not NULL ";
                }
                sql += " AND check_status = 0";
                string c = await sqlConnection.QueryFirstAsync<string>(sql);
                if (c != null)
                {
                    return c;
                }
                else
                {
                    return "0";
                }
            }
        }

        /// <summary>
        /// 进行查询总条数
        /// </summary>
        /// <returns></returns>
        public async Task<string> ChaZongTiaoAsync(string id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(zfc))
            {
                string sql = "SELECT COUNT(*) FROM [dbo].[salary_grant]";
                if (id == "1")
                {
                    sql += " WHERE first_kind_id is not NULL ";
                }
                else if (id == "2")
                {
                    sql += " WHERE second_kind_id is not NULL";
                }
                else if (id == "3")
                {
                    sql += " WHERE third_kind_id is not NULL ";
                }
                sql += " AND check_status = 0";
                string c = await sqlConnection.QueryFirstAsync<string>(sql);
                if (c != null)
                {
                    return c;
                }
                else
                {
                    return "0";
                }
            }
        }

        /// <summary>
        /// 进行查询总金额
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<string> ChaZongJinAsync(string id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(zfc))
            {
                string sql = "SELECT SUM(salary_standard_sum) FROM [dbo].[salary_grant]";
                if (id == "1")
                {
                    sql += " WHERE first_kind_id is not NULL ";
                }
                else if (id == "2")
                {
                    sql += " WHERE second_kind_id is not NULL";
                }
                else if (id == "3")
                {
                    sql += " WHERE third_kind_id is not NULL ";
                }
                sql += " AND check_status = 0";
                string c = await sqlConnection.QueryFirstAsync<string>(sql);
                if (c != null)
                {
                    return c;
                }
                else
                {
                    return "0";
                }
            }
        }

        /// <summary>
        /// 进行查询代复核信息<分页查询>
        /// </summary>
        /// <returns></returns>
        public async Task<FenYe<SalaryGrant>> FenYe(int pageSize, int currentPage)
        {
            using (SqlConnection sqlConnection = new SqlConnection(zfc))
            {
                DynamicParameters dd = new DynamicParameters();
                dd.Add("pageSize", pageSize);
                dd.Add("keyName", "sgr_id");
                dd.Add("tabelName", "salary_grant");
                dd.Add("where", "check_status =0");
                dd.Add("currentPage", currentPage);
                dd.Add("lie", "*");
                dd.Add("rows", direction: ParameterDirection.Output, dbType: DbType.Int32);

                string sql = "exec [dbo].[proc_Fenye] @pageSize,@keyName,@tabelName,@where,@currentPage,@lie,@rows out";
                IEnumerable<SalaryGrant> list = await sqlConnection.QueryAsync<SalaryGrant>(sql, dd);
                int row = dd.Get<int>("rows");
                FenYe<SalaryGrant> fenYe = new FenYe<SalaryGrant>();
                fenYe.CList = list;
                fenYe.currentPage = currentPage;
                fenYe.Totalpage = row % 5 == 0 ? row / 5 : row / 5 + 1;
                fenYe.Totalnumber = row;
                return fenYe;
            }
        }

        /// <summary>
        /// 进行查询代复核信息<分页查询>
        /// </summary>
        /// <returns></returns>
        public async Task<FenYe<SalaryGrant>> FenYetiao(int pageSize, int currentPage, string tiao)
        {
            using (SqlConnection sqlConnection = new SqlConnection(zfc))
            {
                DynamicParameters dd = new DynamicParameters();
                dd.Add("pageSize", pageSize);
                dd.Add("keyName", "sgr_id");
                dd.Add("tabelName", "salary_grant");
                dd.Add("where", tiao);
                dd.Add("currentPage", currentPage);
                dd.Add("lie", "*");
                dd.Add("rows", direction: ParameterDirection.Output, dbType: DbType.Int32);

                string sql = "exec [dbo].[proc_Fenye] @pageSize,@keyName,@tabelName,@where,@currentPage,@lie,@rows out";
                IEnumerable<SalaryGrant> list = await sqlConnection.QueryAsync<SalaryGrant>(sql, dd);
                int row = dd.Get<int>("rows");
                FenYe<SalaryGrant> fenYe = new FenYe<SalaryGrant>();
                fenYe.CList = list;
                fenYe.currentPage = currentPage;
                fenYe.Totalpage = row % 5 == 0 ? row / 5 : row / 5 + 1;
                fenYe.Totalnumber = row;
                return fenYe;
            }
        }

        /// <summary>
        /// 进行查具体信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<SalaryGrant> ChaYi(string id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(zfc))
            {
                string sql = $"SELECT * FROM [dbo].[salary_grant] WHERE salary_grant_id = '{id}'";
                return await sqlConnection.QueryFirstAsync<SalaryGrant>(sql);
            }
        }
    }
}