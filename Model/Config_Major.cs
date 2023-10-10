using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Config_Major
    {
        public int Mak_Id { set; get; } //主键

        [DisplayName("职位分类编号")]
        public string Major_Kind_Id { set; get; } //职位分类编号
        [DisplayName("职位分类名称")]
        public string Major_Kind_Name { set; get; } //职位分类名称
        [DisplayName("职位编号")]
        public string Major_Id { set; get; }//职位编号
        [DisplayName("职位名称")]
        public string Major_Name { set; get; } //职位名称
        [DisplayName("题套数量")]
        public int Test_Amount { set; get; }//题套数量

    }
}
