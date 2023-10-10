using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Model
{
    public class Engage_Major_Release
    {

       
        public int mre_id { set; get; }// 主键，自动增长列 
        public int first_kind_id { set; get; }//一级机构编号
        [DisplayName("一级")]
        public string first_kind_name { set; get; }//一级机构名称 
        public int second_kind_id { set; get; }//二级机构编号 
        public string second_kind_name { set; get; }//  二级机构名称 
        public int third_kind_id { set; get; }//三级机构编号 
        public string third_kind_name { set; get; }// 三级机构名称 
        public int major_id { set; get; }//职位编号 
        public string major_name { set; get; }// 职位名称 
        public int human_amount { set; get; }// 招聘人数 
        public int major_kind_id { set; get; }
        public string major_kind_name { set; get; }
        public string engage_type { set; get; }//招聘类型 
        public DateTime deadline { set; get; }//截至日期 
        public string register { set; get; }//登记人 
        public string changer { set; get; }//变更人 
        public DateTime regist_time { set; get; }//登记时间 
        public DateTime change_time { set; get; }//变更时间 
        public string major_describe { set; get; }//  职位描述 
        public string engage_required { set; get; }// 招聘要求     

        

    }
}
