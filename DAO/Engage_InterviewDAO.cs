using Dapper;
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
    public class Engage_InterviewDAO
    {
        private string constr = "Data Source=.;Initial Catalog=HR_DB;Integrated Security=True";

        public async Task<int> MianShi_DEL(int id)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                string sql = $"DELETE FROM engage_interview WHERE ein_id={id}";

                return await con.ExecuteAsync(sql);
            }
        }

        /// <summary>
        ///
        ///  添加筛选时间与筛选人
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public async Task<int> MianShiXG2(Engage_Interview s)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                string sql = $"update engage_interview set checker={s.checker} ,check_time='{s.check_time}'  where ein_id={s.ein_id} ";
                return await con.ExecuteAsync(sql);
            }
        }

        /// <summary>
        /// 根据副id查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Engage_Interview> ShuanXuan_SIDCX(int id)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                string sql = $"select * from engage_interview where resume_id={id}";
                return await con.QueryFirstAsync<Engage_Interview>(sql);
            }
        }

        public async Task<Engage_Interview> ShuanXuan_IDCX(int id)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                string sql = $"select * from engage_interview where ein_id={id}";
                return await con.QueryFirstAsync<Engage_Interview>(sql);
            }
        }

        /// <summary>
        ///
        ///   栓选状态修改
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<int> MianShi_ZhuangTaiUPDATE(int id, int name)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                string s = "";
                if (name == 1)
                {
                    s = "建议面试";
                }
                else if (name == 2)
                {
                    s = "建议笔试";
                }
                else if (name == 3)
                {
                    s = "建议录用";
                    string sr = $"update engage_interview set interview_status={name},check_comment='{s}' where ein_id={id} ";
                    return await con.ExecuteAsync(sr);
                }
                string sql = $"update engage_interview set interview_status={name},check_comment='{s}' where ein_id={id} ";
                return await con.ExecuteAsync(sql);
            }
        }

        /// <summary>
        ///
        ///   添加数据
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public async Task<int> MianShiJieGuo(Engage_Interview s)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                string sql = $@"insert into  [dbo].[engage_interview](human_name, interview_amount, human_major_kind_id, human_major_kind_name, human_major_id,
             human_major_name, image_degree, native_language_degree, foreign_language_degree, response_speed_degree,
             EQ_degree, IQ_degree, multi_quality_degree, register, checker,interview_comment,
              check_status,registe_time,resume_id)
            values('{s.human_name}','{s.interview_amount}','{s.human_major_kind_id}','{s.human_major_kind_name}','{s.human_major_id}',
                '{s.human_major_name}', '{s.image_degree}', '{s.native_language_degree}', '{s.foreign_language_degree}', '{s.response_speed_degree}',
                 '{s.EQ_degree}', '{s.IQ_degree}', '{s.multi_quality_degree}', '{s.register}', '{s.checker}',{1},
                    '{s.check_status}','{s.registe_time}','{s.resume_id}'
                )"; //,
                return await con.ExecuteAsync(sql);
            }
        }

        public async Task<FY<Engage_Interview>> FYshow_ShuanXuan(int? currentPage)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                if (currentPage == null)
                {
                    currentPage = 1;
                }
                string sum = " interview_comment=1";
                DynamicParameters dynamic = new DynamicParameters();
                dynamic.Add("pageSize", 2);//每页显示
                dynamic.Add("columns", "ein_id, human_name, interview_amount,resume_id, human_major_kind_id, human_major_kind_name, human_major_id, human_major_name, image_degree, native_language_degree, foreign_language_degree, response_speed_degree, registe_time,  interview_status, check_status ,multi_quality_degree");
                dynamic.Add("tableName", "engage_interview");
                dynamic.Add("currentPage", currentPage);//当前页
                dynamic.Add("order", "ein_id");//排序
                dynamic.Add("where", sum);//条件
                dynamic.Add("totalCount", direction: System.Data.ParameterDirection.Output, dbType: DbType.Int32);
                string sql = "exec per @tableName,@columns,@order,@pageSize,@currentPage,@where,@totalCount out";
                var sd = con.Query<Engage_Interview>(sql, dynamic).ToList();
                int rows = dynamic.Get<int>("totalCount");
                FY<Engage_Interview> fY = new FY<Engage_Interview>();
                fY.List = sd;
                fY.Rows = rows;
                fY.CurrentPage = (int)currentPage;
                return fY;
            }
        }
    }
}