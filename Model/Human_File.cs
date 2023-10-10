using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Human_File
    {
        public int huf_id { set; get; } //主键，自动增长列
        public string human_id { set; get; }//档案编号
        public int first_kind_id { set; get; }//一级机构编号
        public string first_kind_name { set; get; }//一级机构名称
        public int second_kind_id { set; get; }//二级机构编号
        public string second_kind_name { set; get; }//二级机构名称
        public int third_kind_id { set; get; }//三级机构编号
        public string third_kind_name { set; get; }//三级机构名称
        public string human_name { set; get; }//名称
        public string human_address { set; get; }//地址
        public string human_postcode { set; get; }//邮政编码
        public string human_pro_designation { set; get; }//职称
        public string human_major_kind_id { set; get; }//职位分类编号
        public string human_major_kind_name { set; get; }//职位分类名称
        public string human_major_id { set; get; }//职位编号
        public string hunma_major_name { set; get; }//职位名称
        public string human_telephone { set; get; }//电话
        public string human_mobilephone { set; get; }//手机号码
        public string human_bank { set; get; }//开户银行
        public string human_account { set; get; }//银行帐号
        public string human_qq { set; get; }//QQ号码
        public string human_email { set; get; }//电子邮件
        public string human_hobby { set; get; }//爱好
        public string human_speciality { set; get; }//特长
        public string human_sex { set; get; }//性别
        public string human_religion { set; get; }//宗教信仰
        public string human_party { set; get; }//政治面貌
        public string human_nationality { set; get; }//国籍
        public string human_race { set; get; }//民族
        public DateTime human_birthday { set; get; }//出生日期
        public string human_birthplace { set; get; }//出生地
        public string human_age { set; get; }//年龄
        public string human_educated_degree { set; get; }//学历
        public string human_educated_years { set; get; }//教育年限
        public string human_educated_major { set; get; }//学历专业
        public string human_society_security_id { set; get; }//社会保障号
        public string human_id_card { set; get; }//身份证号
        public string remark { set; get; }//备注
        public string salary_standard_id { set; get; }//薪酬标准编号
        public string salary_standard_name { set; get; }//薪酬标准名称
        public string salary_sum { set; get; }//基本薪酬总额
        public string demand_salaray_sum { set; get; }//应发薪酬总额
        public string paid_salary_sum { set; get; }//实发薪酬总额
        public string major_change_amount { set; get; }//调动次数
        public string bonus_amount { set; get; }//激励累计次数
        public string training_amount { set; get; }//培训累计次数
        public string file_chang_amount { set; get; }//档案变更累计次数
        public string human_family_membership { set; get; }//家庭关系
        public string human_histroy_records { set; get; }//简历
        public string human_picture { set; get; }//相片
        public string attachment_name { set; get; }//附件名称

        public string check_status { set; get; }//复核状态
        public string register { set; get; }//档案登记人
        public string checker { set; get; }//档案复核人
        public string changer { set; get; }//档案变更人
        public DateTime regist_time { set; get; }//档案登记时间
        public DateTime check_time { set; get; }//档案复核时间
        public DateTime change_time { set; get; }// 档案变更时间
        public DateTime lastly_change_time { set; get; }//档案最近更改时间
        public DateTime delete_time { set; get; }//档案删除时间

        public DateTime recovery_time { set; get; }//档案恢复时间
        public string human_file_status { set; get; }//档案状态

        public int diao { get; set; }
    }
}