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
    public class JianLiGuanLiController : Controller
    {
        // GET: JianLiGuanLi

        private First_kindDAO kindDAO = new First_kindDAO();

        private FileSecondKindDAO file = new FileSecondKindDAO();
        private SalaryStandardDAO sas = new SalaryStandardDAO();
        private ConfigFileThirdKindDAO config = new ConfigFileThirdKindDAO();

        private Engage_Major_ReleaseDAO san = new Engage_Major_ReleaseDAO();
        private Config_MajorDAO cm = new Config_MajorDAO();
        private Config_Major_KindDAO config_Major_KindDAO = new Config_Major_KindDAO();
        private Engage_ResumeDAO jianli = new Engage_ResumeDAO();

        public async Task<ActionResult> fuhe_UPDATE(int id)
        {
            int se = await jianli.fuhe_UPDATE(id);
            if (se > 0)
            {
                return RedirectToAction("ShuaiXuan_Index");
            }
            else
            {
                return Content("0");
            }
        }

        public async Task qd_CX2(Engage_Resume resume)
        {
            IEnumerable<Engage_Resume> chars = await jianli.resumes_TICX2(resume);
            foreach (Engage_Resume s in chars)
            {
                s.check_status = s.check_status == "1" ? "已复核" : "";
            }
            ViewBag.s = chars;
        }

        public async Task qd_CX(Engage_Resume resume)
        {
            IEnumerable<Engage_Resume> chars = await jianli.resumes_TICX(resume);
            foreach (Engage_Resume s in chars)
            {
                s.check_status = s.check_status == "0" ? "未复核" : "";
            }
            ViewBag.s = chars;
        }

        public async Task<ActionResult> qd_show(Engage_Resume resume)
        {
            await qd_CX(resume);
            return View();
        }

        public async Task<ActionResult> qd_show2(Engage_Resume resume)
        {
            await qd_CX2(resume);
            return View();
        }

        /// <summary>
        ///
        /// 打开简历涮选
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> ShuaiXuan_Index()
        {
            await GetLists();
            await CMK();
            return View();
        }

        /// <summary>
        ///
        /// 打开有效简历筛选
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> ShuaiXuan_Index2()
        {
            await GetLists();
            await CMK();
            return View();
        }

        /// <summary>
        ///
        /// 增加简历
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> JianLiINSERT(Engage_Resume config_Major)
        {
            config_Major.human_major_kind_name = san.zhiweifenlei2(config_Major.human_major_kind_id);
            config_Major.human_major_name = san.secondname1(config_Major.human_major_id);
            int se = await jianli.JianLIINSERT(config_Major);
            if (se > 0)
            {
                return RedirectToAction("ShuaiXuan_Index");
            }
            else
            {
                return Content("0");
            }
        }

        public async Task<ActionResult> Index()
        {
            ViewBag.d = await san.yiji();
            IEnumerable<SalaryStandard> zhaoping = await sas.showALL();
            SelectList selectListItems = new SelectList(zhaoping, "standard_id", "salary_sum");
            ViewBag.sas = selectListItems;
            await CMK();
            return View();
        }

        /// <summary>
        ///
        /// 职位名称
        /// </summary>
        /// <returns></returns>
        public async Task GetLists()
        {
            IEnumerable<Config_Major> zhaoping = await cm.ZhiWeiSheCX();
            SelectList selectListItems = new SelectList(zhaoping, "major_id", "major_name");
            ViewBag.f = selectListItems;
            ViewBag.q = zhaoping;
        }

        /// <summary>
        ///
        /// 职位分类
        /// </summary>
        /// <returns></returns>
        public async Task CMK()
        {
            ViewBag.s = await config_Major_KindDAO.ZhiWeiCX();
        }
    }
}