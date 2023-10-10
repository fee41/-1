using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Engage_Resume
    {
        public int res_id { set; get; } //主键，自动增长列
        public string human_name { set; get; } // 姓名
        public string engage_type { set; get; } // 招聘类型
        public string human_address { set; get; } //  地址
        public string human_postcode { set; get; } //  邮编
        public int human_major_id { set; get; } //   职位分类编号
        public string human_major_name { set; get; } //  职位分类名称
        public int human_major_kind_id { set; get; } //   职位编号
        public string human_major_kind_name { set; get; } // 职位名称
        public string human_telephone { set; get; } // 电话号码
        public string human_homephone { set; get; } // 家庭电话
        public string human_mobilephone { set; get; } //  手机
        public string human_email { set; get; } //
        public string human_hobby { set; get; } //兴趣爱好
        public string human_specility { set; get; } //特长
        public string human_sex { set; get; } //    性别
        public string human_religion { set; get; } //  宗教信仰
        public string human_party { set; get; } // 政治面貌
        public string human_nationality { set; get; } //   国籍
        public string human_race { set; get; } //  民族
        public string human_birthday { set; get; } //  出生日期
        public string human_age { set; get; } //  年龄
        public string human_educated_degree { set; get; } //学历
        public string human_educated_years { set; get; } //  教育年限\
      
        public string human_educated_major { set; get; } // 学历专业
        public string human_college { set; get; } //   毕业院校
        public string human_idcard { set; get; } // 身份证号
        public string human_birthplace { set; get; } //出生地
        public string demand_salary_standard { set; get; } //     薪酬标准
        public string human_history_records { set; get; } // 个人履历
        public string remark { set; get; } //  备注
        public string recomandation { set; get; } //  推荐意见
        public string human_picture { set; get; } //  照片
        public string attachment_name { set; get; } // 档案附件
        public string check_status { set; get; } // 复核状态   //0:    1:
        public string register { set; get; } // 登记人
        public DateTime regist_time { set; get; } // 登记时间 interview_status
        public string checker { set; get; } // 复核人姓名
        public DateTime check_time { set; get; } //   复核时间
        public string interview_status { set; get; } // 面试状态
        public string total_points { set; get; } //总分
        public string test_amount { set; get; } //  考试次数
        public string test_checker { set; get; } //测试复核人
        public DateTime test_check_time { set; get; } // 测试复核时间
        public string pass_register { set; get; } //  通过登记人姓名
        public DateTime pass_regist_time { set; get; } // 通过登记时间
        public string pass_checker { set; get; } // 通过复核人姓名
        public DateTime pass_check_time { set; get; } // 通过复核时间
        public string pass_check_status { set; get; } // 通过的复核状态
        public string pass_checkComment { set; get; } //   录用申请审核意见
        public string pass_passComment { set; get; } // 录用申请审批意见
      
    }
}
