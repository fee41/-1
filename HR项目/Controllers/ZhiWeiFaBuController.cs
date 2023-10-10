using DAO;
using Microsoft.Ajax.Utilities;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HR项目.Controllers
{
    public class ZhiWeiFaBuController : Controller
    {
        // GET: ZhiWeiFaBu

        private First_kindDAO kindDAO = new First_kindDAO();
        private UsersDAO usersDAO = new UsersDAO();
        private FileSecondKindDAO file = new FileSecondKindDAO();

        private ConfigFileThirdKindDAO config = new ConfigFileThirdKindDAO();

        private Engage_Major_ReleaseDAO san = new Engage_Major_ReleaseDAO();
        private Config_MajorDAO cm = new Config_MajorDAO();
        private Config_Major_KindDAO config_Major_KindDAO = new Config_Major_KindDAO();

        #region 职位发布查询

        public async Task<ActionResult> ZhiWeiDDSELECT(int? currentPage)
        {
            FY<Engage_Major_Release> chars = await san.FenYe(currentPage);
            ViewBag.z = chars.Rows;
            ViewBag.s = chars.List;
            ViewBag.m = chars.CurrentPage;
            ViewBag.h = ++chars.CurrentPage;
            ViewBag.a = --chars.CurrentPage;
            return View();
        }

        #endregion 职位发布查询

        #region 职位发布变更

        public async Task<ActionResult> Show_UPDATE(Engage_Major_Release engage)
        {
            int se = await san.update_in(engage);
            if (se > 0)
            {
                return RedirectToAction("ZhiWeBianGen");
            }
            else
            {
                return Content("0");
            }
        }

        /// <summary>
        ///
        /// 打开页面
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> biangen_Show(int id)
        {
            ViewBag.s = await san.Engage_Major_Release___idcx(id);
            return View();
        }

        public async Task<ActionResult> ZhiWeBianGen(int? currentPage)
        {
            FY<Engage_Major_Release> chars = await san.FenYe(currentPage);
            ViewBag.z = chars.Rows;
            ViewBag.s = chars.List;
            ViewBag.m = chars.CurrentPage;
            ViewBag.h = ++chars.CurrentPage;
            ViewBag.a = --chars.CurrentPage;
            return View();
        }

        /// <summary>
        ///
        /// 职位发布更改删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> Engage_Major_Release___DEL(int id)
        {
            int sum = await san.Engage_Major_Release___DEL(id);
            if (sum > 0)
            {
                return RedirectToAction("ZhiWeBianGen");
            }
            else
            {
                return RedirectToAction("ZhiWeBianGen");
            }
        }

        #endregion 职位发布变更

        #region 职位发布登记

        /// <summary>
        ///
        ///   显示三级
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //public async Task<ActionResult> ChaSanYg(string id)
        //{
        //   IEnumerable<LianJi> ie = await san.ChaLian(); //查询一级信息

        //    return Json(ie, JsonRequestBehavior.AllowGet);
        //}

        //一级查询
        private async Task GetDropListl()
        {
            IEnumerable<config_file_first_kind> eng = await san.yiji();
            SelectList selectListItems = new SelectList(eng, "first_kind_id", "first_kind_name");
            ViewBag.s1 = selectListItems;
        }

        /// <summary>
        /// 二级查询
        /// </summary>
        /// <returns></returns>
        private async Task GetDropListl2()
        {
            IEnumerable<FileSecondKind> eng = await san.erji();
            SelectList selectListItems = new SelectList(eng, "second_kind_id", "second_kind_name");
            ViewBag.s1t = selectListItems;
        }

        /// <summary>
        /// 三级查询
        /// </summary>
        /// <returns></returns>
        private async Task GetDropListl3()
        {
            IEnumerable<ConfigFileThirdKind> eng = await san.sanji();
            SelectList selectListItems = new SelectList(eng, "third_kind_id", "third_kind_name");
            ViewBag.s1s = selectListItems;
        }

        public async Task<JsonResult> GetLists(string id)
        {
            IEnumerable<Config_Major> zhaoping = await cm.idcx(id);

            return Json(zhaoping, JsonRequestBehavior.AllowGet);
        }

        public async Task CMK()
        {
            ViewBag.s = await config_Major_KindDAO.ZhiWeiCX();
        }

        public async Task<ActionResult> Index()
        {
            IEnumerable<Users> users = await usersDAO.UsersName();
            SelectList selectListItems = new SelectList(users, "u_id", "u_name");
            ViewBag.u = selectListItems;
            await CMK();
            ViewBag.d = await san.yiji();

            return View();
        }

        public async Task<ActionResult> INSERT(Engage_Major_Release engage_Major_Release)
        {
            //  engage_Major_Release.second_kind_name = san.secondname(engage_Major_Release.second_kind_id);
            engage_Major_Release.first_kind_name = san.namese1(engage_Major_Release.first_kind_id);
            engage_Major_Release.second_kind_name = san.namese2(engage_Major_Release.second_kind_id);
            engage_Major_Release.third_kind_name = san.namese3(engage_Major_Release.third_kind_id);
            engage_Major_Release.major_kind_name = san.secondname1(engage_Major_Release.major_kind_id);
            engage_Major_Release.major_name = san.zhiweifenlei2(engage_Major_Release.major_id);
            int cg = await san.Add(engage_Major_Release);

            if (cg > 0)
            {
                return RedirectToAction("ZhiWeBianGen");
            }
            else
            {
                return Content("0");
            }
        }

        #endregion 职位发布登记
    }
}