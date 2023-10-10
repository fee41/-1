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
    public class MajorKindDAO
    {
        private string zfc = "Data Source=.;Initial Catalog=HR_DB;Integrated Security=True";

        /// <summary>
        /// 进行联查
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<LianJi>> ChaLian()
        {
            using (SqlConnection sqlConnection = new SqlConnection(zfc))
            {
                string sql = "SELECT * FROM [dbo].[config_major_kind]";
                IEnumerable<MajorKind> firsts = await sqlConnection.QueryAsync<MajorKind>(sql);
                List<LianJi> jis = new List<LianJi>();
                foreach (MajorKind first in firsts)
                {
                    LianJi lian = new LianJi()
                    {
                        value = first.major_kind_id,
                        label = first.major_kind_name,
                        children = await ChaLian2(first.major_kind_id)
                    };
                    jis.Add(lian);
                }
                return jis;
            }
        }

        /// <summary>
        /// 进行联查2级
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<LianJi>> ChaLian2(string id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(zfc))
            {
                string sql = $"SELECT * FROM [dbo].[config_major] where major_kind_id = '{id}'  ";
                IEnumerable<Major> firsts = await sqlConnection.QueryAsync<Major>(sql);
                List<LianJi> jis = new List<LianJi>();
                foreach (Major first in firsts)
                {
                    LianJi lian = new LianJi()
                    {
                        value = first.major_id,
                        label = first.major_name
                    };
                    jis.Add(lian);
                }
                return jis;
            }
        }
    }
}