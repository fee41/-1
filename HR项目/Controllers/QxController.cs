using DAO;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HR项目.Controllers
{
    public class QxController : Controller
    {
        private UsersDAO usersDAO = new UsersDAO();

        private RolesDAO rolesDAO = new RolesDAO();

        private UserRolesDAO userRolesDAO = new UserRolesDAO();

        private RolesJurisdictionDAO roles1 = new RolesJurisdictionDAO();

        // GET: Qx
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 进行查询
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Cha()
        {
            IEnumerable<Users> ie = await usersDAO.ChaSyAsync1();
            return Json(ie, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 进行删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> Sc(int id)
        {
            int gc = await usersDAO.ScAsync(id);
            if (gc > 0)
            {
                return Content("1");
            }
            else
            {
                return Content("0");
            }
        }

        /// <summary>
        /// 进行添加
        /// </summary>
        /// <returns></returns>
        public ActionResult Tian()
        {
            return View();
        }

        /// <summary>
        /// 进行查询下拉框
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Xia()
        {
            IEnumerable<Roles> roles = await rolesDAO.ChaSyAsync();
            return Json(roles, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 进行添加
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Tj(string zh, string mi, string name, int zw)
        {
            int cg = await usersDAO.TianAsync(zh, mi, name, zw);
            if (cg > 0)
            {
                return Content("1");
            }
            else
            {
                return Content("0");
            }
        }

        /// <summary>
        /// 进行编辑窗口
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Bj(int id)
        {
            ViewBag.s = id;
            return View();
        }

        /// <summary>
        /// 进行显示修改的信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> Xju(string id)
        {
            Users u = await usersDAO.ChaZwMo(id);
            return Json(u, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 进行添加
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Xiu(string zh, string mi, string name, int zw, int id)
        {
            int cg = await usersDAO.XiuAsync(zh, mi, name, zw, id);
            if (cg > 0)
            {
                return Content("1");
            }
            else
            {
                return Content("0");
            }
        }

        /// <summary>
        /// 进入角色管理
        /// </summary>
        /// <returns></returns>
        public ActionResult Jue()
        {
            return View();
        }

        /// <summary>
        /// 进行分页显示
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="currentPage"></param>
        /// <returns></returns>
        public async Task<ActionResult> ChaJu(int pageSize, int currentPage)
        {
            FenYe<Roles> fenYe = await rolesDAO.FenYe(pageSize, currentPage);
            return Json(fenYe, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 进入添加角色
        /// </summary>
        /// <returns></returns>
        public ActionResult TianJue()
        {
            return View();
        }

        /// <summary>
        /// 进行添加角色
        /// </summary>
        /// <param name="roles"></param>
        /// <returns></returns>
        public async Task<ActionResult> Tianj(Roles roles)
        {
            int cg = await rolesDAO.Tian(roles);
            if (cg > 0)
            {
                return Content("1");
            }
            else
            {
                return Content("0");
            }
        }

        /// <summary>
        /// 进行删除角色
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> JueSc(int id)
        {
            int ji = await userRolesDAO.Ji(id);
            if (ji > 0)
            {
                return Content("2");
            }
            else
            {
                int cg = await rolesDAO.ScAsync(id);
                if (cg > 0)
                {
                    return Content("1");
                }
                else
                {
                    return Content("0");
                }
            }
        }

        /// <summary>
        /// 进行编辑角色
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> BianJue(int id)
        {
            Roles roles = await rolesDAO.ChaJuAsync(id);
            ViewBag.s = roles;
            return View();
        }

        /// <summary>
        /// 进行查询具体信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //public async Task<ActionResult> ChaJue(int id)
        //{
        //    return Json(roles, JsonRequestBehavior.AllowGet);
        //}

        /// <summary>
        /// 进行查询具体信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> ChaQx()
        {
            IEnumerable<Tree> list = await rolesDAO.GetTree();

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 进行查询具体权限
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> Ys(string id)
        {
            IEnumerable<int> roles = await roles1.ChaJu(id);
            return Json(roles, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 进行修改
        /// </summary>
        /// <param name="roles"></param>
        /// <param name="zi"></param>
        /// <returns></returns>
        public async Task<ActionResult> XIUqX(Roles roles, string zi)
        {
            int cg = await rolesDAO.Xiu(roles, zi);
            if (cg > 0)
            {
                return Content("1");
            }
            else
            {
                return Content("0");
            }
        }
    }
}