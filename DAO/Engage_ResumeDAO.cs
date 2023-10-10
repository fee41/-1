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
    public  class Engage_ResumeDAO
    {
        //简历表
        string constr = "Data Source=.;Initial Catalog=HR_DB;Integrated Security=True";


        public string QQYX(string id)
        {
            using (SqlConnection con=new SqlConnection(constr))
            {
                string sql = $"select* from engage_resume where res_id={id}";
                List<Engage_Resume> list = new List<Engage_Resume>();
                Engage_Resume config = new Engage_Resume();
                list = con.Query<Engage_Resume>(sql).ToList();
                string name = list[0].human_email;

                return name;

            }
        }








        /// <summary>
        ///   录用查询全部III
        /// </summary>
        /// <param name="currentPage"></param>
        /// <returns></returns>
        public async Task<FY<Engage_Resume>> CXQB(int? currentPage)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                if (currentPage == null)
                {
                    currentPage = 1;
                }
                string sum = "pass_check_status=3";
                DynamicParameters dynamic = new DynamicParameters();
                dynamic.Add("pageSize", 2);//每页显示
                dynamic.Add("columns", "res_id, human_name, engage_type, human_address, human_postcode, human_major_kind_id, human_major_kind_name, human_major_id, human_major_name, human_telephone, human_homephone, human_mobilephone, human_email, human_hobby, human_specility, human_sex, human_religion, human_party, human_nationality, human_race, human_birthday, human_age, human_educated_degree, human_educated_years, human_educated_major, human_college, human_idcard, human_birthplace, demand_salary_standard, human_history_records, remark, recomandation, human_picture, attachment_name, check_status, register, regist_time, checker, check_time, interview_status, total_points, test_amount, test_checker, test_check_time, pass_register, pass_regist_time, pass_checker, pass_check_time, pass_check_status, pass_checkComment, pass_passComment ");
                dynamic.Add("tableName", "engage_resume");
                dynamic.Add("currentPage", currentPage);//当前页
                dynamic.Add("order", "res_id");//排序
                dynamic.Add("where", sum);//条件
                dynamic.Add("totalCount", direction: System.Data.ParameterDirection.Output, dbType: DbType.Int32);
                string sql = "exec per @tableName,@columns,@order,@pageSize,@currentPage,@where,@totalCount out";
                var sd = con.Query<Engage_Resume>(sql, dynamic).ToList();
                int rows = dynamic.Get<int>("totalCount");
                FY<Engage_Resume> fY = new FY<Engage_Resume>();
                fY.List = sd;
                fY.Rows = rows;
                fY.CurrentPage = (int)currentPage;
                return fY;
            }
        }




        /// <summary>
        /// 录用审批页面查询
        /// </summary>
        /// <param name="currentPage"></param>
        /// <returns></returns>
        public async Task<FY<Engage_Resume>> shenpi(int? currentPage)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                if (currentPage == null)
                {
                    currentPage = 1;
                }
                string sum = "pass_check_status=2";
                DynamicParameters dynamic = new DynamicParameters();
                dynamic.Add("pageSize", 2);//每页显示
                dynamic.Add("columns", "res_id, human_name, engage_type, human_address, human_postcode, human_major_kind_id, human_major_kind_name, human_major_id, human_major_name, human_telephone, human_homephone, human_mobilephone, human_email, human_hobby, human_specility, human_sex, human_religion, human_party, human_nationality, human_race, human_birthday, human_age, human_educated_degree, human_educated_years, human_educated_major, human_college, human_idcard, human_birthplace, demand_salary_standard, human_history_records, remark, recomandation, human_picture, attachment_name, check_status, register, regist_time, checker, check_time, interview_status, total_points, test_amount, test_checker, test_check_time, pass_register, pass_regist_time, pass_checker, pass_check_time, pass_check_status, pass_checkComment, pass_passComment ");
                dynamic.Add("tableName", "engage_resume");
                dynamic.Add("currentPage", currentPage);//当前页
                dynamic.Add("order", "res_id");//排序
                dynamic.Add("where", sum);//条件
                dynamic.Add("totalCount", direction: System.Data.ParameterDirection.Output, dbType: DbType.Int32);
                string sql = "exec per @tableName,@columns,@order,@pageSize,@currentPage,@where,@totalCount out";
                var sd = con.Query<Engage_Resume>(sql, dynamic).ToList();
                int rows = dynamic.Get<int>("totalCount");
                FY<Engage_Resume> fY = new FY<Engage_Resume>();
                fY.List = sd;
                fY.Rows = rows;
                fY.CurrentPage = (int)currentPage;
                return fY;
            }
        }


        /// <summary>
        /// 
        ///  人力登记
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <returns></returns>

        public async Task<int> RL_DJ (string id)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                string sql = $"update [dbo].[engage_resume] set pass_check_status=4 where [res_id]={id} ";
                return await con.ExecuteAsync(sql);
            }
        }


        /// <summary>
        /// 
        ///  录用审批修改
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <returns></returns>

        public async Task<int> luyou_shenpi(string id, string name)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                string sql = $"update [dbo].[engage_resume] set [pass_passComment]={name} ,pass_check_status=3 where [res_id]={id} ";
                return await con.ExecuteAsync(sql);
            }
        }




        /// <summary>
        /// 
        ///  录用申请修改
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <returns></returns>

        public async Task<int> luyou_shenqin(string id,string name)
        {
            using (SqlConnection con=new SqlConnection(constr))
            {
                string sql = $"update [dbo].[engage_resume] set [pass_checkComment]={name} ,pass_check_status=2 where [res_id]={id} ";
                return await con.ExecuteAsync(sql);
            }
        }

        public async Task<int> DEL(int id)
        {
            using (SqlConnection con=new SqlConnection(constr))
            {
                string sql = $"delete from engage_resume where res_id={id} ";
                return await con.ExecuteAsync(sql);
            }
        }
        public async Task<int> GenGaiMianShi(int id, int name)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                if (name == 3)
                {
                    string se = $"update engage_resume set interview_status={name},pass_check_status='1' where res_id={id}";
                    return await con.ExecuteAsync(se);
                }
                string sql = $"update engage_resume set interview_status={name} where res_id={id}";
                return await con.ExecuteAsync(sql);
            }
        }

        public async Task<Engage_Resume> ID_Show(int id)
        {
            using (SqlConnection con=new SqlConnection(constr))
            {
                string sql = $"select * from [dbo].[engage_resume] where res_id={id}";
                return await con.QueryFirstAsync<Engage_Resume>(sql);
            }
        }


        public async Task<int> fuhe_UPDATE(int id)
        {
            using (SqlConnection con=new SqlConnection(constr))
            {
                string sql = $"update engage_resume set [check_status]=1  where [res_id]={id}";
                return await con.ExecuteAsync(sql);
            }
        }

     

        /// <summary>
        /// 查询录用申请
        /// </summary>
        /// <param name = "currentPage" ></ param >
        /// < returns ></ returns >
        ///
        public async Task<FY<Engage_Resume>> FYshow_LuYou(int? currentPage)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                if (currentPage == null)
                {
                    currentPage = 1;
                }
                string sum = "pass_check_status=1";
                DynamicParameters dynamic = new DynamicParameters();
                dynamic.Add("pageSize", 2);//每页显示
                dynamic.Add("columns", "res_id, human_name, engage_type, human_address, human_postcode, human_major_kind_id, human_major_kind_name, human_major_id, human_major_name, human_telephone, human_homephone, human_mobilephone, human_email, human_hobby, human_specility, human_sex, human_religion, human_party, human_nationality, human_race, human_birthday, human_age, human_educated_degree, human_educated_years, human_educated_major, human_college, human_idcard, human_birthplace, demand_salary_standard, human_history_records, remark, recomandation, human_picture, attachment_name, check_status, register, regist_time, checker, check_time, interview_status, total_points, test_amount, test_checker, test_check_time, pass_register, pass_regist_time, pass_checker, pass_check_time, pass_check_status, pass_checkComment, pass_passComment ");
                dynamic.Add("tableName", "engage_resume");
                dynamic.Add("currentPage", currentPage);//当前页
                dynamic.Add("order", "res_id");//排序
                dynamic.Add("where", sum);//条件
                dynamic.Add("totalCount", direction: System.Data.ParameterDirection.Output, dbType: DbType.Int32);
                string sql = "exec per @tableName,@columns,@order,@pageSize,@currentPage,@where,@totalCount out";
                var sd = con.Query<Engage_Resume>(sql, dynamic).ToList();
                int rows = dynamic.Get<int>("totalCount");
                FY<Engage_Resume> fY = new FY<Engage_Resume>();
                fY.List = sd;
                fY.Rows = rows;
                fY.CurrentPage = (int)currentPage;
                return fY;
            }
        }



        public async Task<FY<Engage_Resume>> FYshow_QuanBu(int? currentPage)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                if (currentPage == null)
                {
                    currentPage = 1;
                }
                string sum = "check_status=1";
                DynamicParameters dynamic = new DynamicParameters();
                dynamic.Add("pageSize", 2);//每页显示
                dynamic.Add("columns", "res_id, human_name, engage_type, human_address, human_postcode, human_major_kind_id, human_major_kind_name, human_major_id, human_major_name, human_telephone, human_homephone, human_mobilephone, human_email, human_hobby, human_specility, human_sex, human_religion, human_party, human_nationality, human_race, human_birthday, human_age, human_educated_degree, human_educated_years, human_educated_major, human_college, human_idcard, human_birthplace, demand_salary_standard, human_history_records, remark, recomandation, human_picture, attachment_name, check_status, register, regist_time, checker, check_time, interview_status, total_points, test_amount, test_checker, test_check_time, pass_register, pass_regist_time, pass_checker, pass_check_time, pass_check_status, pass_checkComment, pass_passComment ");
                dynamic.Add("tableName", "engage_resume");
                dynamic.Add("currentPage", currentPage);//当前页
                dynamic.Add("order", "res_id");//排序
                dynamic.Add("where", sum);//条件
                dynamic.Add("totalCount", direction: System.Data.ParameterDirection.Output, dbType: DbType.Int32);
                string sql = "exec per @tableName,@columns,@order,@pageSize,@currentPage,@where,@totalCount out";
                var sd = con.Query<Engage_Resume>(sql, dynamic).ToList();
                int rows = dynamic.Get<int>("totalCount");
                FY<Engage_Resume> fY = new FY<Engage_Resume>();
                fY.List = sd;
                fY.Rows = rows;
                fY.CurrentPage = (int)currentPage;
                return fY;
            }
        }


























        /// <summary>
        /// 
        ///   已复核
        /// </summary>
        /// <param name="resume"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Engage_Resume>> resumes_TICX2(Engage_Resume resume)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                string sql = "select * from engage_resume where 1=1 and check_status=1  ";
                if (resume.human_major_id != 0)
                {
                    sql += $" and human_major_id={resume.human_major_id}";
                }
                if (resume.human_major_kind_id != 0)
                {
                    sql += $" and human_major_kind_id={resume.human_major_kind_id} ";
                }
                if (resume.pass_passComment != "" && resume.pass_passComment != null)
                {
                    sql += $" and  {resume.pass_checkComment}< [regist_time]";
                }
                if (resume.pass_checkComment != "" && resume.pass_checkComment != null)
                {
                    sql += $" and [regist_time]>{resume.pass_passComment}";
                }
                return await con.QueryAsync<Engage_Resume>(sql);
            }
        }

        /// <summary>
        ///  
        /// 未复核
        /// </summary>
        /// <param name="resume"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Engage_Resume>> resumes_TICX(Engage_Resume resume )
        {
            using (SqlConnection con=new SqlConnection(constr))
            {
                string sql = "select * from engage_resume where 1=1 and check_status=0  ";
                if (resume.human_major_id !=0)
                {
                    sql += $" and human_major_id={resume.human_major_id}";
                }
                if (resume.human_major_kind_id != 0)
                {
                    sql +=$" and human_major_kind_id={resume.human_major_kind_id} ";
                }
                if (resume.pass_passComment != "" && resume.pass_passComment != null)
                {
                    sql += $" and  {resume.pass_checkComment}< [regist_time]";
                }
                if ( resume.pass_checkComment != ""&& resume.pass_checkComment != null)
                {
                    sql += $" and [regist_time]>{resume.pass_passComment}";
                }
                return await con.QueryAsync<Engage_Resume>(sql);
            }
        }


        /// <summary>
        /// 
        /// 添加
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        /// 
        public async Task<int> JianLIINSERT(Engage_Resume s)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                


                string sql = $@"insert into [dbo].[engage_resume](human_major_name,human_major_kind_name,engage_type,human_name
                ,human_sex,human_email,human_telephone,human_homephone,human_mobilephone,human_address,human_postcode
                ,human_nationality,human_birthplace,human_birthday,human_race,human_religion,human_party,human_idcard,
                human_age,human_college,human_educated_degree,human_educated_years,human_educated_major,demand_salary_standard,
                regist_time,human_specility,human_hobby,human_history_records,remark,check_status,human_major_id,human_major_kind_id,human_picture ) 
                VALUES('{s.human_major_name}','{s.human_major_kind_name}','{s.engage_type}','{s.human_name}'
                ,'{s.human_sex}','{s.human_email}','{s.human_telephone}','{s.human_homephone}','{s.human_mobilephone}','{s.human_address}','{s.human_postcode}'
                ,'{s.human_nationality}','{s.human_birthplace}','{s.human_birthday}','{s.human_race}','{s.human_religion}','{s.human_party}','{s.human_idcard}'
                ,'{s.human_age}','{s.human_specility}','{s.human_hobby}','{s.human_educated_years}','{s.human_educated_major}','{s.demand_salary_standard}',
                '{s.regist_time}','{s.human_specility}','{s.human_hobby}','{s.human_history_records}','{s.remark}','{0}',{s.human_major_id},{s.human_major_kind_id},'{s.human_picture}')";
                return await con.ExecuteAsync(sql);
            }
        }


    }
}
