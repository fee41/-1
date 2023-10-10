using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class UserRolesDAO
    {
        private string zfc = "Data Source=.;Initial Catalog=HR_DB;Integrated Security=True";

        /// <summary>
        /// 进行查询有多少条
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<int> Ji(int id)
        {
            using (SqlConnection con = new SqlConnection(zfc))
            {
                string sql = $"SELECT COUNT(*) FROM [dbo].[UserRoles] WHERE RolesID = {id}";
                return await con.QueryFirstAsync<int>(sql);
            }
        }
    }
}