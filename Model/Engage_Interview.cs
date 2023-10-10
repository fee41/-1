using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Engage_Interview
    {
        //面试表
       
        public int ein_id { set; get; }  //主键，自动增长列
        public string human_name { set; get; } //: 姓名                                                                         */
        public string interview_amount { set; get; } //: 面试次数                                                                 *
        public string human_major_kind_id { set; get; } //: 职位分类编号                                                           

        public string human_major_kind_name { set; get; }//: 职位分类名称                                                         
        public string human_major_id { set; get; } //: 职位编号                                                                   
        public string human_major_name { set; get; }//: 职位名称                                                                 
        public string image_degree { set; get; }//: 形象等级                                                                     
        public string native_language_degree { set; get; } //: 口才等级                                                           
        public string foreign_language_degree { set; get; }//: 外语水平等级                                                      
        public string response_speed_degree { set; get; }//: 应变能力                                                            
        public string EQ_degree { set; get; }//: EQ等级                                                                        
        public string IQ_degree { set; get; }//: IQ_等级                                                                       
        public string multi_quality_degree { set; get; }//: 综合素质                                                             
        public string register { set; get; } //面试人               
        public string checker { set; get; }//筛选人                                                                           
        public DateTime registe_time { set; get; }// : 面试时间                                                                     
        public DateTime check_time { set; get; } //: 筛选时间                                                                       
        public int resume_id { set; get; }//: 简历编号                                                                       /
        public string result { set; get; }//: 面试结果                                                                          
        public string interview_comment { set; get; }// : 面试评价                                                                
        public string check_comment { set; get; }//: 筛选评价                                                                    
        public string interview_status { set; get; } //: 面试状态                                                               

        public string check_status { set; get; }// : 筛选状态       //0   1  2 3        
    }
}
