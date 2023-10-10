using Dapper;
using Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Lifetime;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class SalaryGrantDetailsDAO
    {
        private string zfc = "Data Source=.;Initial Catalog=HR_DB;Integrated Security=True";

        /// <summary>
        /// 进行添加
        /// </summary>
        /// <param name="sd"></param>
        /// <param name="djr"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public async Task<int> FindAsync(List<SalaryGrantDetails> sd, string djr, string time, List<SalaryGrant> grants, string id)
        {
            using (SqlConnection con = new SqlConnection(zfc))
            {
                double sum = 0;
                double sum1 = 0;
                int r = 0;

                Random random = new Random();
                string result = random.Next(100000000, 1000000000).ToString(); // 生成9位数随机字符串

                foreach (var item in sd)
                {
                    r++;
                    sum += item.salary_paid_sum;
                    sum1 += item.salary_standard_sum;
                    string sql = $@"INSERT INTO [dbo].[salary_grant_details]( [salary_grant_id], [human_id], [human_name], [bouns_sum], [sale_sum], [deduct_sum], [salary_standard_sum], [salary_paid_sum]) VALUES('{result}','{item.human_id}','{item.human_name}','{item.bouns_sum}','{item.sale_sum}','{item.deduct_sum}','{item.salary_standard_sum}','{item.salary_paid_sum}')";
                    int dore = await con.ExecuteAsync(sql);
                    if (dore == 0)
                    {
                        return 0;
                    }
                }
                string sql2 = $@"INSERT INTO [dbo].[salary_grant](salary_grant_id, salary_standard_id, first_kind_id, first_kind_name, second_kind_id, second_kind_name, third_kind_id, third_kind_name, human_amount, salary_standard_sum, salary_paid_sum, register, regist_time, check_status) VALUES ('{result}','{sd[0].salary_grant_id}','{grants[0].first_kind_id}','{grants[0].first_kind_name}','{grants[0].second_kind_id}','{grants[0].second_kind_name}','{grants[0].third_kind_id}','{grants[0].third_kind_name}','{r}','{sum}','{sum1}','{djr}','{time}','0')";
                int c = await con.ExecuteAsync(sql2);
                if (c == 0)
                {
                    return 0;
                }
                string sql3 = $"UPDATE [dbo].[human_file] SET check_status = 4 WHERE huf_id  = '{id}'";
                return await con.ExecuteAsync(sql3);
            }
        }

        /// <summary>
        /// 进行复核
        /// </summary>
        /// <param name="sd"></param>
        /// <param name="djr"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public async Task<int> FuAsync(List<SalaryGrantDetails> sd, string djr, string time, string jin, List<HumanFile> files)
        {
            using (SqlConnection con = new SqlConnection(zfc))
            {
                double sum = 0;
                foreach (var item in sd)
                {
                    sum += item.salary_paid_sum;
                    string sql = $@"UPDATE salary_grant_details SET bouns_sum = '{item.bouns_sum}',sale_sum = '{item.sale_sum}',deduct_sum = '{item.deduct_sum}', salary_standard_sum = '{item.salary_standard_sum}'  WHERE salary_grant_id = '{item.salary_grant_id}'";
                    int dore = await con.ExecuteAsync(sql);
                    if (dore == 0)
                    {
                        return 0;
                    }
                }
                string sql2 = $@"UPDATE [dbo].[salary_grant] SET [checker]='{djr}', [check_time]='{time}',check_status='2',[salary_paid_sum] = '{jin}' where  salary_grant_id='{sd[0].salary_grant_id}'";
                int c = await con.ExecuteAsync(sql2);
                if (c == 0) { return 0; }
                int cg = 0;
                foreach (var item in files)
                {
                    string sql3 = $"UPDATE [dbo].[human_file] SET paid_salary_sum = '{sum}' , check_status = 5 WHERE huf_id = '{item.huf_id}'";
                    cg = await con.ExecuteAsync(sql3);
                }
                return cg;
            }
        }

        /// <summary>
        /// 进行查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IEnumerable<SalaryGrantDetails>> ChaAsync(string id)
        {
            using (SqlConnection con = new SqlConnection(zfc))
            {
                string sql = $"SELECT * FROM [dbo].[salary_grant_details] WHERE salary_grant_id = '{id}'";
                return await con.QueryAsync<SalaryGrantDetails>(sql);
            }
        }
    }
}