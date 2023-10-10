using DAO;
using Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing.Printing;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace HR项目.Controllers
{
    public class MianShiController : Controller
    {
        // GET: MianShi
        private First_kindDAO kindDAO = new First_kindDAO();

        private FileSecondKindDAO file = new FileSecondKindDAO();
        private UsersDAO usersDAO = new UsersDAO();
        private ConfigFileThirdKindDAO config = new ConfigFileThirdKindDAO();

        private Engage_Major_ReleaseDAO san = new Engage_Major_ReleaseDAO();
        private Config_MajorDAO cm = new Config_MajorDAO();
        private Config_Major_KindDAO config_Major_KindDAO = new Config_Major_KindDAO();
        private Engage_ResumeDAO jianli = new Engage_ResumeDAO();
        private Engage_InterviewDAO EngageDAO = new Engage_InterviewDAO();

        /// <summary>
        /// 删除
        /// </summary>
        /// <param ></param>
        /// <returns></returns>

        public async Task<ActionResult> MianShi_DEL(int id, int id2)
        {
            int rs = await jianli.DEL(id2);
            int rs2 = await EngageDAO.MianShi_DEL(id);
            if (rs > 0 & rs2 > 0)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public async Task<ActionResult> MianShiXG2(Engage_Interview engage)
        {
            int name = 0;
            int name2 = 0;
            if (engage.check_comment == "建议录用")
            {
                name = 3;
                name2 = 3;
            }
            if (engage.check_comment == "建议笔试")
            {
                name = 2;
                name2 = 2;
            }
            if (engage.check_comment == "建议面试")
            {
                name = 1;
                name2 = 1;
            }

            int se2 = await jianli.GenGaiMianShi(engage.resume_id, name2);
            int se = await EngageDAO.MianShi_ZhuangTaiUPDATE(engage.ein_id, name);
            int re = await EngageDAO.MianShiXG2(engage);
            if (se > 0 & re > 0 & se2 > 0)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        //public async Task<ActionResult> MianShi_UPDATE(int id,int name,int bid)
        //{
        //    if (se > 0& se2>0)
        //    {
        //        return RedirectToAction("/MianShi/Index");
        //    }
        //    else
        //    {
        //        return RedirectToAction("Index");
        //    }

        //}

        public async Task<ActionResult> MianShi_SXID_CX(int id)
        {
            IEnumerable<Users> users = await usersDAO.UsersName();
            SelectList selectListItems = new SelectList(users, "u_id", "u_name");
            ViewBag.u = selectListItems;

            Engage_Interview se = await EngageDAO.ShuanXuan_IDCX(id);
            ViewBag.d = await jianli.ID_Show(se.resume_id);
            ViewBag.s = se;
            return View();
        }

        public async Task<ActionResult> Index(int? currentPage)
        {
            await mianshiDengJi(currentPage);
            return View();
        }

        /// <summary>
        ///
        /// 打开面试涮选
        /// </summary>
        /// <param name="currentPage"></param>
        /// <returns></returns>
        public async Task<ActionResult> MianShiSX_Show(int? currentPage)
        {
            await SX_Show(currentPage);
            return View();
        }

        /// <summary>
        ///
        /// 查询合格的面试刷选
        /// </summary>
        /// <param name="currentPage"></param>
        /// <returns></returns>
        public async Task SX_Show(int? currentPage)
        {
            FY<Engage_Interview> chars = await EngageDAO.FYshow_ShuanXuan(currentPage);
            ViewBag.z = chars.Rows;
            ViewBag.s = chars.List;
            ViewBag.m = chars.CurrentPage;
            ViewBag.h = ++chars.CurrentPage;
            ViewBag.a = --chars.CurrentPage;
        }

        /// <summary>
        ///
        ///  查询登记
        /// </summary>
        /// <param name="currentPage"></param>
        /// <returns></returns>
        public async Task mianshiDengJi(int? currentPage)
        {
            FY<Engage_Resume> chars = await jianli.FYshow_QuanBu(currentPage);

            foreach (Engage_Resume s in chars.List)
            {
                if (s.interview_status == null)
                {
                    s.interview_status = "待面试";
                }
                else
                if (s.interview_status == "1")
                {
                    s.interview_status = "待面试";
                }
                else
                if (s.interview_status == "2")
                {
                    s.interview_status = "待笔试";
                }
                else
                if (s.interview_status == "3")
                {
                    s.interview_status = "通过";
                }
            }
            ViewBag.z = chars.Rows;
            ViewBag.s = chars.List;
            ViewBag.m = chars.CurrentPage;
            ViewBag.h = ++chars.CurrentPage;
            ViewBag.a = --chars.CurrentPage;
        }

        public async Task<ActionResult> DengJi(int id)
        {
            IEnumerable<Users> users = await usersDAO.UsersName();
            SelectList selectListItems = new SelectList(users, "u_id", "u_name");
            ViewBag.u = selectListItems;
            ViewBag.s = await jianli.ID_Show(id);
            return View();
        }

        /// <summary>
        /// /
        ///   新增面试结果登记
        /// </summary>
        /// <param name="engage"></param>
        /// <returns></returns>
        public async Task<ActionResult> MianShiINSERT(Engage_Interview engage)
        {
            int se = await EngageDAO.MianShiJieGuo(engage);
            if (se > 0)
            {
                return RedirectToAction("MianShiSX_Show");
            }
            else
            {
                return Content("0");
            }
        }
    }
}