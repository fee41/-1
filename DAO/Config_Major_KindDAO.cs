using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Model;

namespace DAO
{
    public class Config_Major_KindDAO
    {
        string constr = "Data Source=.;Initial Catalog=HR_DB;Integrated Security=True";

    
        public async Task<IEnumerable<Config_Major_Kind>> ZhiWeiCX()
        {
         
            using (SqlConnection con=new SqlConnection(constr))
            {
                string sql = "select * from config_major_kind";
                return await con.QueryAsync<Config_Major_Kind>(sql);
            }
        }


        public async Task<int> ZhiWeiDel(int id)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                string sql = $"delete from config_major_kind where Mfk_id={id}";
                return await con.ExecuteAsync(sql);
            }
        }
        public int 查询最大的值()
        {
            using (SqlConnection con=new SqlConnection(constr))
            {
                string sql = $"select max([major_kind_id]) as se from[dbo].[config_major_kind]";
                IEnumerable<dynamic> sum= con.Query(sql);
                foreach (string item in sum)
                {
                    Console.WriteLine(item);
                }
                return 3;
            }
        }

        public async Task<int> ZhiWeiINSERT(string configs)
        {
            
            using (SqlConnection con = new SqlConnection(constr))
            {   
                string sql = $@"insert into [dbo].[config_major_kind]([major_kind_id],[major_kind_name])
values((SELECT TOP 1  CASE  WHEN  + 1 < 10 THEN '0' + CAST([major_kind_id] + 1 AS VARCHAR(2)) ELSE 
CAST([major_kind_id] + 1 AS VARCHAR(2))  END AS FormattedValue FROM [dbo].[config_major_kind] ORDER BY [major_kind_id] DESC),'{configs}')";
                return await con.ExecuteAsync(sql);
            }
        }
    }
}
