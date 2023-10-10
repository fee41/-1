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
    public class ChangeDAO
    {
        private string zfc = "Data Source=.;Initial Catalog=HR_DB;Integrated Security=True";

        /// <summary>
        /// 进行添加
        /// </summary>
        /// <returns></returns>
        public async Task<int> Tian(Change change)
        {
            using (SqlConnection sqlConnection = new SqlConnection(zfc))
            {
                string sql1 = $"UPDATE [dbo].[human_file]  SET diao = 1 where human_id= '{change.human_id}'";
                int c = await sqlConnection.ExecuteAsync(sql1);
                if (c == 0)
                {
                    return 0;
                }
                string sql = $"INSERT INTO [dbo].[major_change](first_kind_id, first_kind_name, second_kind_id, second_kind_name, third_kind_id, third_kind_name, major_kind_id, major_kind_name, major_id, major_name, new_first_kind_id, new_first_kind_name, new_second_kind_id, new_second_kind_name, new_third_kind_id, new_third_kind_name, new_major_kind_id, new_major_kind_name, new_major_id, new_major_name, human_id, human_name, salary_standard_id, salary_standard_name, salary_sum, new_salary_standard_id, new_salary_standard_name, new_salary_sum, change_reason, check_status, register, regist_time) VALUES ('{change.first_kind_id}','{change.first_kind_name}' , '{change.second_kind_id}', '{change.second_kind_name}', '{change.third_kind_id}','{change.third_kind_name}' ,'{change.major_kind_id}' ,'{change.major_kind_name}' ,'{change.major_id}' ,'{change.major_name}' , '{change.new_first_kind_id}','{change.new_first_kind_name}' ,'{change.new_second_kind_id}' , '{change.new_second_kind_name}','{change.new_third_kind_id}' , '{change.new_third_kind_name}','{change.new_major_kind_id}' , '{change.new_major_kind_name}', '{change.new_major_id}', '{change.new_major_name}', '{change.human_id}', '{change.human_name}', '{change.salary_standard_id}', '{change.salary_standard_name}', '{change.salary_sum}', '{change.new_salary_standard_id}','{change.new_salary_standard_name}' ,'{change.new_salary_sum}' ,'{change.change_reason}' , '{change.check_status}', '{change.register}', '{change.regist_time}')";
                return await sqlConnection.ExecuteAsync(sql);
            }
        }

        /// <summary>
        /// 进行分页显示
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="currentPage"></param>
        /// <returns></returns>
        public async Task<FenYe<Change>> FenYe(int pageSize, int currentPage)
        {
            using (SqlConnection sqlConnection = new SqlConnection(zfc))
            {
                DynamicParameters dd = new DynamicParameters();
                dd.Add("pageSize", pageSize);
                dd.Add("keyName", "mch_id");
                dd.Add("tabelName", "major_change ");
                dd.Add("where", "check_status = 0");
                dd.Add("currentPage", currentPage);
                dd.Add("lie", "*");
                dd.Add("rows", direction: ParameterDirection.Output, dbType: DbType.Int32);

                string sql = "exec [dbo].[proc_Fenye] @pageSize,@keyName,@tabelName,@where,@currentPage,@lie,@rows out";
                IEnumerable<Change> list = await sqlConnection.QueryAsync<Change>(sql, dd);
                int row = dd.Get<int>("rows");
                FenYe<Change> fenYe = new FenYe<Change>();
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
        public async Task<Change> Yg(string id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(zfc))
            {
                string sql = $"SELECT * FROM [dbo].[major_change] WHERE check_status = 0 AND human_id = {id}";
                return await sqlConnection.QueryFirstAsync<Change>(sql);
            }
        }

        /// <summary>
        /// 通过进行修改
        /// </summary>
        /// <param name="change"></param>
        /// <returns></returns>
        public async Task<int> Xiu(Change change)
        {
            using (SqlConnection sqlConnection = new SqlConnection(zfc))
            {
                string sql = $"UPDATE [dbo].[major_change] SET new_first_kind_id = '{change.new_first_kind_id}',new_first_kind_name = '{change.new_first_kind_name}',new_second_kind_id = '{change.new_second_kind_id}', new_second_kind_name='{change.new_second_kind_name}',new_major_kind_id='{change.new_major_kind_id}',new_third_kind_id='{change.new_third_kind_id}',new_third_kind_name='{change.new_third_kind_name}',new_major_kind_name='{change.new_major_kind_id}',new_major_id='{change.new_major_id}',new_major_name='{change.new_major_name}',new_salary_standard_id='{change.new_salary_standard_id}',new_salary_standard_name='{change.new_salary_standard_name}',new_salary_sum='{change.new_salary_sum}',check_reason = '{change.check_reason}',check_status=1,checker='{change.checker}',check_time='{change.check_time}' WHERE mch_id = '{change.mch_id}'";

                int c = await sqlConnection.ExecuteAsync(sql);
                if (c == 0)
                {
                    return 0;
                }
                SalaryStandardDAO dAO = new SalaryStandardDAO();
                SalaryStandard salary = await dAO.ChaD(change.new_salary_standard_id);

                string sql1 = $"UPDATE [dbo].[human_file]  SET diao = 0 ,major_change_amount +=1, salary_standard_id =  {salary.standard_id},salary_standard_name = '{change.new_salary_standard_name}',salary_sum = '{change.new_salary_sum}',first_kind_id = '{change.new_first_kind_id}',first_kind_name = '{change.new_first_kind_name}',second_kind_id = '{change.new_second_kind_id}',second_kind_name = '{change.new_second_kind_name}',third_kind_id = '{change.new_third_kind_id}',third_kind_name='{change.new_third_kind_name}' where human_id = '{change.human_id}'";
                return await sqlConnection.ExecuteAsync(sql1);
            }
        }

        /// <summary>
        ///不 通过进行修改
        /// </summary>
        /// <param name="change"></param>
        /// <returns></returns>
        public async Task<int> Xiu1(Change change)
        {
            using (SqlConnection sqlConnection = new SqlConnection(zfc))
            {
                string sql = $"UPDATE [dbo].[major_change] SET check_status =2 where mch_id = '{change.mch_id}'";

                int c = await sqlConnection.ExecuteAsync(sql);
                if (c == 0)
                {
                    return 0;
                }
                string sql1 = $"UPDATE [dbo].[human_file]  SET diao = 0 where human_id = '{change.human_id}'";
                return await sqlConnection.ExecuteAsync(sql1);
            }
        }

        /// <summary>
        /// 进行查看我要需要的信息
        /// </summary>
        /// <param name="tiao"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Change>> changesAsync(string tiao)
        {
            using (SqlConnection sqlConnection = new SqlConnection(zfc))
            {
                string sql = "SELECT * FROM [dbo].[major_change] " + tiao;
                return await sqlConnection.QueryAsync<Change>(sql);
            }
        }

        /// <summary>
        /// 进行查看我要需要的信息根据id查
        /// </summary>
        /// <param name="tiao"></param>
        /// <returns></returns>
        public async Task<Change> changesAsync1(string tiao)
        {
            using (SqlConnection sqlConnection = new SqlConnection(zfc))
            {
                string sql = "SELECT * FROM [dbo].[major_change] where mch_id = " + tiao;
                return await sqlConnection.QueryFirstAsync<Change>(sql);
            }
        }
    }
}