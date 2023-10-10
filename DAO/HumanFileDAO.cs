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
    public class HumanFileDAO
    {
        private string zfc = "Data Source=.;Initial Catalog=HR_DB;Integrated Security=True";

        /// <summary>
        /// 进行查询具体信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IEnumerable<HumanFile>> ChaJuAsync(string id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(zfc))
            {
                string sql = $"SELECT * FROM [dbo].[human_file] WHERE salary_standard_id =" + id;
                return await sqlConnection.QueryAsync<HumanFile>(sql);
            }
        }

        /// <summary>
        /// 进行分页查询
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="currentPage"></param>
        /// <returns></returns>
        public async Task<FenYe<HumanFile>> FenYe(int pageSize, int currentPage, string tiao)
        {
            using (SqlConnection sqlConnection = new SqlConnection(zfc))
            {
                DynamicParameters dd = new DynamicParameters();
                dd.Add("pageSize", pageSize);
                dd.Add("keyName", "huf_id");
                dd.Add("tabelName", "human_file");
                dd.Add("where", tiao);
                dd.Add("currentPage", currentPage);
                dd.Add("lie", "*");
                dd.Add("rows", direction: ParameterDirection.Output, dbType: DbType.Int32);

                string sql = "exec [dbo].[proc_Fenye] @pageSize,@keyName,@tabelName,@where,@currentPage,@lie,@rows out";
                IEnumerable<HumanFile> list = await sqlConnection.QueryAsync<HumanFile>(sql, dd);
                int row = dd.Get<int>("rows");
                FenYe<HumanFile> fenYe = new FenYe<HumanFile>();
                fenYe.CList = list;
                fenYe.currentPage = currentPage;
                fenYe.Totalpage = row % 5 == 0 ? row / 5 : row / 5 + 1;
                fenYe.Totalnumber = row;
                return fenYe;
            }
        }

        /// <summary>
        /// 进行查询具体信息单个
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<HumanFile> ChaYiAsync(string id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(zfc))
            {
                string sql = $"SELECT * FROM [dbo].[human_file] WHERE huf_id = '{id}'  ";
                return await sqlConnection.QueryFirstAsync<HumanFile>(sql);
            }
        }

        /// <summary>
        /// 进行查询具体信息单个
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<HumanFile> ChaYiAsync1(string id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(zfc))
            {
                string sql = $"SELECT * FROM [dbo].[human_file] WHERE human_id = '{id}'  ";
                return await sqlConnection.QueryFirstAsync<HumanFile>(sql);
            }
        }

        /// <summary>
        /// 进行筛选
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IEnumerable<HumanFile>> SXuanAsync(string tiao)
        {
            using (SqlConnection sqlConnection = new SqlConnection(zfc))
            {
                string sql = $"SELECT  [first_kind_id],[first_kind_name],[second_kind_id],[second_kind_name],[third_kind_id],[third_kind_name], sum([demand_salaray_sum]) as [demand_salaray_sum],sum(salary_sum) as salary_sum,sum([paid_salary_sum]) as [paid_salary_sum], sum(case when {tiao} then 1 else 0 end) as Number FROM [dbo].[human_file] where {tiao} GROUP BY [first_kind_name],[second_kind_name],[third_kind_name],[first_kind_id],[second_kind_id],[third_kind_id]";
                return await sqlConnection.QueryAsync<HumanFile>(sql);
            }
        }

        public async Task<IEnumerable<HumanFile>> SXuanAsync3(string tiao)
        {
            using (SqlConnection sqlConnection = new SqlConnection(zfc))
            {
                string sql = $"SELECT salary_standard_id, human_name,human_id, huf_id , [first_kind_id],[first_kind_name],[second_kind_id],[second_kind_name],[third_kind_id],[third_kind_name], sum([demand_salaray_sum]) as [demand_salaray_sum],sum(salary_sum) as salary_sum,sum([paid_salary_sum]) as [paid_salary_sum], sum(case when {tiao} then 1 else 0 end) as Number FROM [dbo].[human_file] where {tiao} GROUP BY [first_kind_name],[second_kind_name],[third_kind_name],[first_kind_id],[second_kind_id],[third_kind_id],human_name,human_id, huf_id,salary_standard_id ";
                return await sqlConnection.QueryAsync<HumanFile>(sql);
            }
        }

        /// <summary>
        /// 进行筛选
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IEnumerable<HumanFile>> SXuanAsync1(string tiao)
        {
            using (SqlConnection sqlConnection = new SqlConnection(zfc))
            {
                string sql = $"select * from [dbo].[salary_grant_details] l inner join [dbo].[human_file] h on l.human_id=h.human_id where {tiao} ";
                return await sqlConnection.QueryAsync<HumanFile>(sql);
            }
        }

        /// <summary>
        /// 进行查询总人数
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<int> ChaZongRenAsync(string id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(zfc))
            {
                string sql = "SELECT COUNT(*)   FROM [dbo].[human_file] ";
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
                return await sqlConnection.QueryFirstAsync<int>(sql);
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
                string sql = "SELECT SUM(salary_sum) FROM [dbo].[human_file] ";
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
    }
}