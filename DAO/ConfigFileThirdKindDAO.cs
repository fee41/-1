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
    public class ConfigFileThirdKindDAO
    {
        private string zfc = "Data Source=.;Initial Catalog=HR_DB;Integrated Security=True";


        public async Task<IEnumerable<ConfigFileThirdKind>> IIshow(int id,string name)
        {
            using (SqlConnection con=new SqlConnection(zfc))
            {
                string sql = $"select * from config_file_third_kind where {name}={id}";
                return await con.QueryAsync<ConfigFileThirdKind>(sql);
            }
        }


        /// <summary>
        /// 进行查询所有信息
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<ConfigFileThirdKind>> ChaSyAsync()
        {
            using (SqlConnection sqlConnection = new SqlConnection(zfc))
            {
                string sql = "SELECT * FROM [dbo].[config_file_third_kind]";
                return await sqlConnection.QueryAsync<ConfigFileThirdKind>(sql);
            }
        }

        /// <summary>
        /// 进行添加
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public async Task<int> Tian(ConfigFileThirdKind config)
        {
            using (SqlConnection sqlConnection = new SqlConnection(zfc))
            {
                First_kindDAO kindDAO = new First_kindDAO();
                FileSecondKindDAO file = new FileSecondKindDAO();

                //进行查询一级名称
                First_kind first = await kindDAO.ChaYiAsyncE(config.first_kind_id);
                //查询二级名称
                FileSecondKind fil = await file.ChaYgAsync(config.second_kind_id);
                //进行获取三级的id
                string sql1 = "SELECT TOP 1 CASE  WHEN third_kind_id + 1 < 10 THEN '0' + CAST(third_kind_id + 1 AS VARCHAR(2)) ELSE CAST(third_kind_id + 1 AS VARCHAR(2)) END AS FormattedValue FROM [dbo].[config_file_third_kind] ORDER BY ftk_id DESC";
                string id = await sqlConnection.QueryFirstAsync<string>(sql1);
                string sql = $"INSERT INTO [dbo].[config_file_third_kind] ( first_kind_id, first_kind_name, second_kind_id, second_kind_name, third_kind_id, third_kind_name, third_kind_sale_id, third_kind_is_retail) VALUES ('{config.first_kind_id}','{first.first_kind_name}','{config.second_kind_id}','{fil.second_kind_name}','{id}','{config.third_kind_name}','{config.third_kind_sale_id}','{config.third_kind_is_retail}')";
                return await sqlConnection.ExecuteAsync(sql);
            }
        }

        /// <summary>
        /// 进行删除
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public async Task<int> ScAsync(ConfigFileThirdKind config)
        {
            using (SqlConnection sqlConnection = new SqlConnection(zfc))
            {
                string sql = $"DELETE FROM [dbo].[config_file_third_kind] WHERE ftk_id = '{config.ftk_id}'";
                return await sqlConnection.ExecuteAsync(sql);
            }
        }

        /// <summary>
        /// 进行查询具体信息
        /// </summary>
        /// <returns></returns>
        public async Task<ConfigFileThirdKind> ChaYiAsync(string id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(zfc))
            {
                string sql = $"SELECT * FROM [dbo].[config_file_third_kind] WHERE third_kind_id = '{id}'";
                return await sqlConnection.QueryFirstAsync<ConfigFileThirdKind>(sql);
            }
        }

        /// <summary>
        /// 进行修改
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public async Task<int> XIuAsync(ConfigFileThirdKind config)
        {
            using (SqlConnection sqlConnection = new SqlConnection(zfc))
            {
                string sql = $"UPDATE [dbo].[config_file_third_kind] SET third_kind_sale_id = '{config.third_kind_sale_id}',third_kind_is_retail = '{config.third_kind_is_retail}' WHERE third_kind_id = '{config.third_kind_id}'";
                return await sqlConnection.ExecuteAsync(sql);
            }
        }
    }
}