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
    public class RolesJurisdictionDAO
    {
        private string zfc = "Data Source=.;Initial Catalog=HR_DB;Integrated Security=True";

        /// <summary>
        /// 进行查询具体权限信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IEnumerable<int>> ChaJu(string id)
        {
            using (SqlConnection con = new SqlConnection(zfc))
            {
                string sql = $"SELECT JuriID FROM [dbo].[RolesJurisdiction] WHERE  RolesID = {id}";
                return await con.QueryAsync<int>(sql);
            }
        }
    }
}