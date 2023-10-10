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
    public class Human_FileDAO
    {
        private string constr = "Data Source=.;Initial Catalog=HR_DB;Integrated Security=True";

        public async Task<int> QBUPDATE(Human_File s)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                string sql = $@"update Human_File set human_name='{s.human_name}',human_telephone='{s.human_telephone}'
,human_mobilephone='{s.human_mobilephone}',human_bank='{s.human_bank}',human_account='{s.human_account}'
            ,human_email='{s.human_email}',human_address='{s.human_address}',human_nationality='{s.human_nationality}'
,human_birthplace='{s.human_birthplace}',human_birthday='{s.human_birthday}',human_race='{s.human_race}'
,human_id_card='{s.human_id_card}',
human_society_security_id='{s.human_society_security_id}',human_age='{s.human_age}'
,human_educated_degree='{s.human_educated_degree}',human_educated_years='{s.human_educated_years}'
,human_educated_major='{s.human_educated_major}',
human_speciality='{s.human_speciality}',human_hobby='{s.human_hobby}',human_family_membership='{s.human_family_membership}'  ,human_qq='{s.human_qq}'
   where huf_id={s.huf_id}
";
                return await con.ExecuteAsync(sql);
            }
        }

        public async Task<FY<Human_File>> RLDA_ChanXu(Human_File resume, int? currentPage)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                string sql = " check_status=3  ";
                if (resume.first_kind_id != 0)
                {
                    sql += $" and first_kind_id={resume.first_kind_id}";
                }
                if (resume.second_kind_id != 0)
                {
                    sql += $" and second_kind_id={resume.second_kind_id} ";
                }
                if (resume.third_kind_id != 0)
                {
                    sql += $" and third_kind_id={resume.third_kind_id} ";
                }
                if (resume.human_major_kind_id != "全部" && resume.human_major_kind_id != null)
                {
                    sql += $" and human_major_kind_id={resume.human_major_kind_id}";
                }
                if (resume.human_major_id != "全部" && resume.human_major_id != null)
                {
                    sql += $" and human_major_id={resume.human_major_id}";
                }
                if (resume.human_major_id != "全部" && resume.human_major_id != null)
                {
                    sql += $" and human_major_id={resume.human_major_id}";
                }
                if (currentPage == null)
                {
                    currentPage = 1;
                }

                DynamicParameters dynamic = new DynamicParameters();
                dynamic.Add("pageSize", 2);//每页显示
                dynamic.Add("columns", "huf_id, human_id, first_kind_id, first_kind_name, second_kind_id, second_kind_name, third_kind_id, third_kind_name, human_name, human_address, human_postcode, human_pro_designation, human_major_kind_id, human_major_kind_name, human_major_id, hunma_major_name, human_telephone, human_mobilephone, human_bank, human_account, human_qq, human_email, human_hobby, human_speciality, human_sex, human_religion, human_party, human_nationality, human_race, human_birthday, human_birthplace, human_age, human_educated_degree, human_educated_years, human_educated_major, human_society_security_id, human_id_card, remark, salary_standard_id, salary_standard_name, salary_sum, demand_salaray_sum, paid_salary_sum, major_change_amount, bonus_amount, training_amount, file_chang_amount, human_histroy_records, human_family_membership, human_picture, attachment_name, check_status, register, checker, changer");
                dynamic.Add("tableName", "Human_File");
                dynamic.Add("currentPage", currentPage);//当前页
                dynamic.Add("order", "huf_id");//排序
                dynamic.Add("where", sql);//条件
                dynamic.Add("totalCount", direction: System.Data.ParameterDirection.Output, dbType: DbType.Int32);
                string sql2 = "exec per @tableName,@columns,@order,@pageSize,@currentPage,@where,@totalCount out";
                var sd = con.Query<Human_File>(sql2, dynamic).ToList();
                int rows = dynamic.Get<int>("totalCount");
                FY<Human_File> fY = new FY<Human_File>();
                fY.List = sd;
                fY.Rows = rows;
                fY.CurrentPage = (int)currentPage;
                return fY;
            }
        }

        public async Task<Human_File> FuHe_idcx(int? id)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                string sql = $"select * from human_file where huf_id={id}";
                return await con.QueryFirstAsync<Human_File>(sql);
            }
        }

        public async Task<FY<Human_File>> show()
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                DynamicParameters dynamic = new DynamicParameters();
                dynamic.Add("pageSize", 8);//每页显示
                dynamic.Add("columns", "huf_id, human_id, first_kind_name, second_kind_name, third_kind_name, human_name, human_address, human_postcode, human_pro_designation, human_major_kind_id, human_major_kind_name, human_major_id, hunma_major_name, human_telephone, human_mobilephone, human_bank, human_account, human_qq, human_email, human_hobby, human_speciality, human_sex, human_religion, human_party, human_nationality, human_race, human_birthday, human_birthplace, human_age, human_educated_degree, human_educated_years, human_educated_major, human_society_security_id, human_id_card, remark, salary_standard_id, salary_standard_name, salary_sum, demand_salaray_sum, paid_salary_sum, major_change_amount, bonus_amount, training_amount, file_chang_amount, human_histroy_records, human_family_membership, human_picture, attachment_name, check_status, register, checker, changer");
                dynamic.Add("tableName", "Human_File");
                dynamic.Add("currentPage", 1);//当前页
                dynamic.Add("order", "huf_id");//排序
                dynamic.Add("where", "check_status = 2");//条件
                dynamic.Add("totalCount", direction: System.Data.ParameterDirection.Output, dbType: DbType.Int32);
                string sql2 = "exec per @tableName,@columns,@order,@pageSize,@currentPage,@where,@totalCount out";
                var sd = con.Query<Human_File>(sql2, dynamic).ToList();
                int rows = dynamic.Get<int>("totalCount");
                FY<Human_File> fY = new FY<Human_File>();
                fY.List = sd;
                fY.Rows = rows;
                return fY;
            }
        }

        /// <summary>
        ///  复核通过
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<int> FuHe_UPDATE(string id)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                string sql = $"update human_file set check_status =3 where huf_id={id}";
                return await con.ExecuteAsync(sql);
            }
        }

        public async Task<int> HF_INSERT(Human_File s)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                string sql = $@"insert into  [dbo].[human_file](  first_kind_id, first_kind_name,
                second_kind_id, second_kind_name, third_kind_id,
                third_kind_name, human_name, human_address
                , human_postcode, human_pro_designation, human_major_kind_id,
                human_major_kind_name, human_major_id, hunma_major_name, human_telephone,
                human_mobilephone, human_bank, human_account, human_qq, human_email,
                 human_speciality, human_sex, human_religion,
                human_party, human_nationality, human_race, human_birthday,
                human_birthplace, human_age, human_educated_degree,
                human_educated_years, human_educated_major, human_society_security_id,
                human_id_card, remark, salary_standard_id,
                 demand_salaray_sum,  file_chang_amount, human_histroy_records, human_family_membership
                , human_picture, attachment_name,  register, regist_time,human_id,check_status,salary_standard_name,salary_sum,diao)
                values({s.first_kind_id},'{s.first_kind_name}',{s.second_kind_id},'{s.second_kind_name}','{s.third_kind_id}',
                '{s.third_kind_name}','{s.human_name}','{s.human_address}',
'{s.human_postcode}', '{s.human_pro_designation}','{s.human_major_kind_id}'
,'{s.human_major_kind_name}', '{s.human_major_id}','{s.hunma_major_name}','{s.human_telephone}',
            '{s.human_mobilephone}', '{s.human_bank}','{s.human_account}', '{s.human_qq}', '{s.human_email}',
                '{s.human_speciality}', '{s.human_sex}', '{s.human_religion}',
               '{s.human_party}', '{s.human_nationality}', '{s.human_race}', '{s.human_birthday}',
               '{s.human_birthplace}','{s.human_age}', '{s.human_educated_degree}',
               '{s.human_educated_years}', '{s.human_educated_major}', '{s.human_society_security_id}',
                '{s.human_id_card}','{s.remark}', '{s.salary_standard_id}',
                '{s.demand_salaray_sum}', '{s.file_chang_amount}', '{s.human_histroy_records}', '{s.human_family_membership}'
                ,'{s.human_picture}', '{s.attachment_name}', '{s.register}', '{s.regist_time}','{s.human_id}' ,'2','{s.salary_standard_name}','{s.salary_sum}',0
                    )";

                return await con.ExecuteAsync(sql);
            }
        }
    }
}