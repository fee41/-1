using Dapper;
using Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DAO
{
    public class UsersDAO
    {
        private string zfc = "Data Source=.;Initial Catalog=HR_DB;Integrated Security=True";

        /// <summary>
        /// 进行登录
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public async Task<Users> ChaYgAsync(string name, string pwd)
        {
            using (SqlConnection con = new SqlConnection(zfc))
            {
                string sql = $"SELECT u_id,u_name,u_true_name,u_password,  r.RolesIf as RolesName FROM [dbo].[users] u INNER JOIN [dbo].[UserRoles] s on s.UserID = u.u_id INNER JOIN [dbo].[Roles] r on r.RolesID = s.RolesID WHERE u_true_name = '{name}' AND u_password= '{pwd}'";
                try
                {
                    return await con.QueryFirstAsync<Users>(sql);
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// 查询所有用户
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Users>> ChaSyAsync()
        {
            using (SqlConnection con = new SqlConnection(zfc))
            {
                string sql = "SELECT * FROM [dbo].[users]";
                return await con.QueryAsync<Users>(sql);
            }
        }

        /// <summary>
        /// 查询所有用户
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Users>> ChaSyAsync1()
        {
            using (SqlConnection con = new SqlConnection(zfc))
            {
                string sql = "SELECT u_id,u_name,u_true_name,u_password,r.RolesName FROM [dbo].[users] u INNER JOIN [dbo].[UserRoles] s on s.UserID = u.u_id INNER JOIN [dbo].[Roles] r on r.RolesID = s.RolesID  ";
                return await con.QueryAsync<Users>(sql);
            }
        }

        /// <summary>
        /// 进行删除数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<int> ScAsync(int id)
        {
            using (SqlConnection con = new SqlConnection(zfc))
            {
                string sql1 = $"DELETE FROM [dbo].[UserRoles] WHERE UserID = '{id}'";
                int cg = await con.ExecuteAsync(sql1);
                if (cg == 0)
                {
                    return 0;
                }
                string sql = $"DELETE FROM [dbo].[users] WHERE u_id = '{id}'";
                return await con.ExecuteAsync(sql);
            }
        }

        /// <summary>
        /// 进行天机
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<int> TianAsync(string zh, string mi, string name, int zw)
        {
            using (SqlConnection con = new SqlConnection(zfc))
            {
                string sql = $"INSERT INTO [dbo].[users](u_name, u_true_name, u_password) VALUES ('{name}','{zh}','{mi}')";
                int cg = await con.ExecuteAsync(sql);
                if (cg == 0)
                {
                    return 0;
                }
                string sql1 = "SELECT MAX(u_id) FROM [dbo].[users]";
                int cg1 = await con.QueryFirstAsync<int>(sql1);
                string sql2 = $"INSERT INTO [dbo].[UserRoles](UserID, RolesID) VALUES ('{cg1}','{zw}')";
                return await con.ExecuteAsync(sql2);
            }
        }

        /// <summary>
        /// 查询所有用户
        /// </summary>
        /// <returns></returns>
        public async Task<Users> ChaZwMo(string id)
        {
            using (SqlConnection con = new SqlConnection(zfc))
            {
                string sql = "SELECT u_id,u_name,u_true_name,u_password,r.RolesID FROM [dbo].[users] u INNER JOIN [dbo].[UserRoles] s on s.UserID = u.u_id INNER JOIN [dbo].[Roles] r on r.RolesID = s.RolesID  where u_id =  " + id;
                return await con.QueryFirstAsync<Users>(sql);
            }
        }

        public  string gjid_name(string id)
        {
            using (SqlConnection con=new SqlConnection(zfc))
            {
                string name = "";
                string sql = $"select * from users where u_id={id}";
                List<Users> list = new List<Users>();
                Users config = new Users();
                list = con.Query<Users>(sql).ToList();
                name = list[0].u_name;
                return  name;
            }
        }

        public async Task<IEnumerable<Users>> UsersName() 
        {
            using (SqlConnection con=new SqlConnection(zfc))
            {
                string sql = $"select * from users  ";
                return await con.QueryAsync<Users>(sql);
            }
        }

        /// <summary>
        /// 进行修改
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<int> XiuAsync(string zh, string mi, string name, int zw, int id)
        {
            using (SqlConnection con = new SqlConnection(zfc))
            {
                string sql = $"UPDATE [dbo].[users] SET u_name = '{name}',u_true_name = '{zh}',u_password = '{mi}' where u_id = '{id}'";
                int cg = await con.ExecuteAsync(sql);
                if (cg == 0)
                {
                    return 0;
                }
                string sql1 = $"UPDATE [dbo].[UserRoles] SET RolesID = '{zw}' where UserID = '{id}'";
                return await con.ExecuteAsync(sql1);
            }
        }
    }
}