using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public  class FY<T>
    {
        public int Rows { set; get; }//返回的值
        public int CurrentPage { set; get; }//当前页
        public int PageSize { set; get; }//每页显示数
        public IEnumerable<T> List { set; get; }//返回的值
    }
}
