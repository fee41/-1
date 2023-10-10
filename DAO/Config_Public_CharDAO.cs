using Dapper;
using Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public  class Config_Public_CharDAO
    {//公共属性
        string constr = "Data Source=.;Initial Catalog=HR_DB;Integrated Security=True";

        /// <summary>
        ///   
        /// 公共属性添加
        /// </summary>
        /// <param name="config_Public_Char"></param>
        /// <returns></returns>
        public async Task<int> GongShuXingINSERT(Config_Public_Char config_Public_Char)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                string sql = $@"insert into config_public_char(attribute_kind, attribute_name) 
               VALUES('{config_Public_Char.Attribute_Kind}','{config_Public_Char.Attribute_Name}')";
                return await con.ExecuteAsync(sql);
            }
        }

        /// <summary>
        /// 公共属性查询
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Config_Public_Char>> GongShuXingCX()
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                string sql = "select * from Config_Public_Char ";
                return await con.QueryAsync<Config_Public_Char>(sql);
            }
        }

        /// <summary>
        /// 
        /// 职称查询
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Config_Public_Char>> ZhiChegCX()
        {

            using (SqlConnection con = new SqlConnection(constr))
            {
                string sql = "select * from Config_Public_Char where [attribute_kind]='职称' ";
                return await con.QueryAsync<Config_Public_Char>(sql);
            }
        }
        /// <summary>
        ///
        /// 职称删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<int> ZhiChengDEL(int id)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                string sql = $"delete from Config_Public_Char where pbc_id={id} ";
                return await con.ExecuteAsync(sql);
            }
        }
    }
}
