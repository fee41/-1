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
    public class FileSecondKindDAO
    {
        private string zfc = "Data Source=.;Initial Catalog=HR_DB;Integrated Security=True";

        /// <summary>
        /// 进行查询所有信息
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<FileSecondKind>> ChaSy()
        {
            using (SqlConnection sqlConnection = new SqlConnection(zfc))
            {
                string sql = "SELECT * FROM [dbo].[config_file_second_kind]";
                return await sqlConnection.QueryAsync<FileSecondKind>(sql);
            }
        }

        /// <summary>
        /// 进行添加数据
        /// </summary>
        /// <param name="fileSecondKind"></param>
        /// <returns></returns>
        public async Task<int> TianAsync(FileSecondKind fileSecondKind)
        {
            using (SqlConnection sqlConnection = new SqlConnection(zfc))
            {
                string sql1 = "SELECT TOP 1 \r\n    CASE \r\n        WHEN second_kind_id + 1 < 10 THEN '0' + CAST(second_kind_id + 1 AS VARCHAR(2))\r\n        ELSE CAST([first_kind_id] + 1 AS VARCHAR(2))\r\n    END AS FormattedValue\r\nFROM [dbo].[config_file_second_kind]\r\nORDER BY fsk_id DESC";
                string id = await sqlConnection.QueryFirstAsync<string>(sql1);
                First_kindDAO kindDAO = new First_kindDAO();
                First_kind first = await kindDAO.ChaYiAsyncE(fileSecondKind.first_kind_id);
                string sql = $"INSERT INTO [dbo].[config_file_second_kind](first_kind_id, first_kind_name, second_kind_id, second_kind_name, second_salary_id, second_sale_id) VALUES ('{first.first_kind_id}','{first.first_kind_name}','{id}','{fileSecondKind.second_kind_name}','{fileSecondKind.second_salary_id}','{fileSecondKind.second_sale_id}')";
                return await sqlConnection.ExecuteAsync(sql);
            }
        }

        /// <summary>
        /// 进行删除
        /// </summary>
        /// <param name="fileSecondKind"></param>
        /// <returns></returns>
        public async Task<int> ScAsync(string id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(zfc))
            {
                string sql = $"DELETE FROM [dbo].[config_file_second_kind] WHERE second_kind_id = '{id}'";
                return await sqlConnection.ExecuteAsync(sql);
            }
        }

        /// <summary>
        /// 进行查询具体信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<FileSecondKind> ChaYgAsync(string id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(zfc))
            {
                string sql = $"SELECT * FROM [dbo].[config_file_second_kind] WHERE second_kind_id = '{id}'";
                return await sqlConnection.QueryFirstAsync<FileSecondKind>(sql);
            }
        }

        /// <summary>
        /// 进行修改
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public async Task<int> XiuAsync(FileSecondKind file)
        {
            using (SqlConnection sqlConnection = new SqlConnection(zfc))
            {
                string sql = $"UPDATE [dbo].[config_file_second_kind] SET second_salary_id = '{file.second_salary_id}' ,second_sale_id = '{file.second_sale_id}' WHERE second_kind_id = '{file.second_kind_id}'\r\n";
                return await sqlConnection.ExecuteAsync(sql);
            }
        }

        /// <summary>
        /// 进行级联查
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<LianJi>> ChaLian()
        {
            using (SqlConnection sqlConnection = new SqlConnection(zfc))
            {
                string sql = "SELECT * FROM [dbo].[config_file_first_kind]";
                IEnumerable<First_kind> firsts = await sqlConnection.QueryAsync<First_kind>(sql);
                List<LianJi> jis = new List<LianJi>();
                foreach (First_kind first in firsts)
                {
                    LianJi lian = new LianJi()
                    {
                        value = first.first_kind_id,
                        label = first.first_kind_name,
                        children = await ChaEYAsync(first.first_kind_id)
                    };
                    jis.Add(lian);
                }
                return jis;
            }
        }

        /// <summary>
        /// 进行级联查
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<LianJi>> ChaLian1()
        {
            using (SqlConnection sqlConnection = new SqlConnection(zfc))
            {
                string sql = "SELECT * FROM [dbo].[config_file_first_kind]";
                IEnumerable<First_kind> firsts = await sqlConnection.QueryAsync<First_kind>(sql);
                List<LianJi> jis = new List<LianJi>();
                foreach (First_kind first in firsts)
                {
                    LianJi lian = new LianJi()
                    {
                        value = first.first_kind_id,
                        label = first.first_kind_name,
                        children = await ChaEYAsync1(first.first_kind_id)
                    };
                    jis.Add(lian);
                }
                return jis;
            }
        }

        /// <summary>
        /// 根据一级查耳二级
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IEnumerable<LianJi>> ChaEYAsync(string id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(zfc))
            {
                string sql = $"SELECT second_kind_id,second_kind_name FROM [dbo].[config_file_second_kind] WHERE first_kind_id = '{id}'";
                IEnumerable<FileSecondKind> f = await sqlConnection.QueryAsync<FileSecondKind>(sql);
                List<LianJi> jis = new List<LianJi>();
                foreach (FileSecondKind fileSecondKind in f)
                {
                    LianJi ji = new LianJi()
                    {
                        value = fileSecondKind.second_kind_id,
                        label = fileSecondKind.second_kind_name,
                    };
                    jis.Add(ji);
                }
                return jis;
            }
        }

        /// <summary>
        /// 根据一级查耳二级
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IEnumerable<LianJi>> ChaEYAsync1(string id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(zfc))
            {
                string sql = $"SELECT second_kind_id,second_kind_name FROM [dbo].[config_file_second_kind] WHERE first_kind_id = '{id}'";
                IEnumerable<FileSecondKind> f = await sqlConnection.QueryAsync<FileSecondKind>(sql);
                List<LianJi> jis = new List<LianJi>();
                foreach (FileSecondKind fileSecondKind in f)
                {
                    LianJi ji = new LianJi()
                    {
                        value = fileSecondKind.second_kind_id,
                        label = fileSecondKind.second_kind_name,
                        children = await ChaSanAsync(fileSecondKind.second_kind_id)
                    };
                    jis.Add(ji);
                }
                return jis;
            }
        }

        /// <summary>
        /// 根据二级查耳三级
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IEnumerable<LianJi>> ChaSanAsync(string id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(zfc))
            {
                string sql = $"SELECT third_kind_id,third_kind_name FROM [dbo].[config_file_third_kind] WHERE second_kind_id = '{id}'";
                IEnumerable<ConfigFileThirdKind> f = await sqlConnection.QueryAsync<ConfigFileThirdKind>(sql);
                List<LianJi> jis = new List<LianJi>();
                foreach (ConfigFileThirdKind fileSecondKind in f)
                {
                    LianJi ji = new LianJi()
                    {
                        value = fileSecondKind.third_kind_id,
                        label = fileSecondKind.third_kind_name,
                    };
                    jis.Add(ji);
                }
                return jis;
            }
        }
    }
}