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
    public class PublicCharDAO
    {
        private string zfc = "Data Source=.;Initial Catalog=HR_DB;Integrated Security=True";

        /// <summary>
        /// 进行查询所有信息
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<PublicChar>> ChaSyAsync(string xin)
        {
            using (SqlConnection sqlConnection = new SqlConnection(zfc))
            {
                string sql = $"SELECT * FROM [dbo].[config_public_char] WHERE attribute_kind = '{xin}'";
                return await sqlConnection.QueryAsync<PublicChar>(sql);
            }
        }

        /// <summary>
        /// 进行添加
        /// </summary>
        /// <returns></returns>
        public async Task<int> TinaAsync(PublicChar publicChar)
        {
            using (SqlConnection sqlConnection = new SqlConnection(zfc))
            {
                string sql = $"INSERT INTO [dbo].[config_public_char](attribute_kind, attribute_name) VALUES ('{publicChar.attribute_kind}','{publicChar.attribute_name}')";
                return await sqlConnection.ExecuteAsync(sql);
            }
        }

        /// <summary>
        /// 进行删除
        /// </summary>
        /// <returns></returns>
        public async Task<int> ScAsync(int id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(zfc))
            {
                string sql = $"DELETE FROM [dbo].[config_public_char] WHERE pbc_id = '{id}'";
                return await sqlConnection.ExecuteAsync(sql);
            }
        }
    }
}