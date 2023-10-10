using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Model;

namespace DAO
{
    public class Config_MajorDAO
    {
        //职位设置
        string constr = "Data Source=.;Initial Catalog=HR_DB;Integrated Security=True";

        /// <summary>
        /// 根据 id 查询职位
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Config_Major>> idcx(string id)
        {

            using (SqlConnection con = new SqlConnection(constr))
            {
                string sql = $"select * from [dbo].[config_major] where [major_kind_id]={id}";
                return await con.QueryAsync<Config_Major>(sql);
            }
        }

        /// <summary>
        ///    查询
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Config_Major>> ZhiWeiSheCX()
        {

            using (SqlConnection con = new SqlConnection(constr))
            {
                string sql = $"select * from [dbo].[config_major]";
                return await con.QueryAsync<Config_Major>(sql);
            }
        }

        /// <summary>
        ///    删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<int> ZhiWeiSheDEL(int id)
        {
            using (SqlConnection con=new SqlConnection(constr))
            {
                string sql = $"delete from config_major where mak_id={id} ";
                return await con.ExecuteAsync(sql);
            }
        }
        /// <summary>
        ///
        /// 添加
        /// </summary>
        /// <param name="config_Major"></param>
        /// <returns></returns>
        public async Task<int> ZhiWeiSheINSERT(Config_Major config)
        {
            using (SqlConnection con=new SqlConnection(constr))
            {
                string sql = $@"insert into config_major( major_kind_id, major_kind_name, major_id, major_name)
                            VALUES({config.Major_Kind_Id},{config.Major_Kind_Name},{config.Major_Id},{config.Major_Name})  ";
                return await con.ExecuteAsync(sql);
            }
        }


    
      
    }
}
