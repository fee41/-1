using DAO;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Helpers;
using System.Web.UI.WebControls;

namespace HR项目.Controllers
{
    public class LuYongController : Controller
    {
        private First_kindDAO kindDAO = new First_kindDAO();

        private FileSecondKindDAO file = new FileSecondKindDAO();
        private UsersDAO usersDAO = new UsersDAO();
        private ConfigFileThirdKindDAO config = new ConfigFileThirdKindDAO();

        private Engage_Major_ReleaseDAO san = new Engage_Major_ReleaseDAO();
        private Config_MajorDAO cm = new Config_MajorDAO();
        private Config_Major_KindDAO config_Major_KindDAO = new Config_Major_KindDAO();
        private Engage_ResumeDAO jianli = new Engage_ResumeDAO();
        Engage_InterviewDAO EngageDAO = new Engage_InterviewDAO();
        // GET: LuYong

      

        /// <summary>
        /// 
        /// 录用查询 III  查询
        /// </summary>
        /// <param name="currentPage"></param>
        /// <returns></returns>
        public async Task<ActionResult> LU_ShenQinShow(int id)
        {
            await idcx(id);
            return View();
        }


        public async Task<ActionResult> Lu_Show(int? currentPage)
        {
            await CXQB(currentPage); 
            return View();
        }
        /// <summary>
        ///   查询录用审批页面
        /// </summary>
        /// <param name="currentPage"></param>
        /// <returns></returns>
        public async Task CXQB(int? currentPage)
        {
            FY<Engage_Resume> chars = await jianli.CXQB(currentPage);

            ViewBag.z = chars.Rows;
            ViewBag.s = chars.List;
            ViewBag.m = chars.CurrentPage;
            ViewBag.h = ++chars.CurrentPage;
            ViewBag.a = --chars.CurrentPage;

        }


        /// <summary>
        ///   录用审批完
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<ActionResult> luyou_SPADD(string id, string name)
        {
            string name2 = jianli.QQYX(id);
            int re = await jianli.luyou_shenpi(id, name);
            if (re > 0)
            {
                Create(name2);
                return RedirectToAction("Lu_Show");
            }
            else
            {
                return Content("0");
            }
        }
        /// <summary>
        ///   发送邮箱
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ActionResult Create(string name)
        {
            try
            {
                WebMail.SmtpServer = "smtp.qq.com";
                WebMail.SmtpPort = 25;
                WebMail.EnableSsl = true;
                WebMail.UserName = "2946068721@qq.com";
                WebMail.Password = "qkonaqkcbjqhdeae";
                WebMail.From = "2946068721@qq.com";
                WebMail.Send(name, "欢迎您的入职", "人事部门");
                return Content("成功");
            }
            catch (Exception)
            {

                return Content("失败");
            }
        }


        /// <summary>
        /// 
        /// 录用审批
        /// </summary>
        /// <param name="currentPage"></param>
        /// <returns></returns>
        public async Task<ActionResult> Lu_SP(int? currentPage)
        {
            await shenpi(currentPage);
            return View();
        }
        /// <summary>
        ///   查询录用审批页面
        /// </summary>
        /// <param name="currentPage"></param>
        /// <returns></returns>
        public async Task shenpi(int? currentPage)
        {
            FY<Engage_Resume> chars = await jianli.shenpi(currentPage);

            ViewBag.z = chars.Rows;
            ViewBag.s = chars.List;
            ViewBag.m = chars.CurrentPage;
            ViewBag.h = ++chars.CurrentPage;
            ViewBag.a = --chars.CurrentPage;

        }



        /// <summary>
        /// 
        /// 录用审批申请查询
        /// </summary>
        /// <param name="currentPage"></param>
        /// <returns></returns>
        public  async Task<ActionResult> LU_YoShePi(int id)
        {
            await idcx(id);
            return View();
        }
        




        public async Task<ActionResult> Index(int? currentPage)
        {
            await mianshiDengJi(currentPage);
            return View();
        }

        /// <summary>
        ///    录用申请添加
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<ActionResult> luyou_shenqin(string id,string name)
        {
            int re=  await jianli.luyou_shenqin(id, name);
            if (re > 0)
            {
                return RedirectToAction("Lu_SP");
            }
            else
            {
                return Content("0");
            }
        }
        






        /// <summary>
        /// 
        ///  查询录用申请
        /// </summary>
        /// <param name="currentPage"></param>
        /// <returns></returns>
        public async Task mianshiDengJi(int? currentPage)
        {
            FY<Engage_Resume> chars = await jianli.FYshow_LuYou(currentPage);

            ViewBag.z = chars.Rows;
            ViewBag.s = chars.List;
            ViewBag.m = chars.CurrentPage;
            ViewBag.h = ++chars.CurrentPage;
            ViewBag.a = --chars.CurrentPage;

        }


        public async Task idcx(int id)
        {
           
            Engage_Resume en = await jianli.ID_Show(id);
            Engage_Interview se = await EngageDAO.ShuanXuan_SIDCX(en.res_id);

            se.checker= usersDAO.gjid_name(se.checker);


            ViewBag.s = se;
            ViewBag.d = en;

        }


        /// <summary>
        /// 
        /// 录用申请页面打开
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> LuYongShenQIN(int id)
        {
             await idcx(id);
             return View();

        }

        //public Task<ActionResult> 

    }
}