using Antlr.Runtime.Misc;
using DAO;
using Microsoft.Win32;
using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace HR项目.Controllers
{
    public class DangAaController : Controller
    {
        // GET: DangAa
        private First_kindDAO kindDAO = new First_kindDAO();

        private FileSecondKindDAO file = new FileSecondKindDAO();
        private UsersDAO usersDAO = new UsersDAO();
        private ConfigFileThirdKindDAO config = new ConfigFileThirdKindDAO();

        private Engage_Major_ReleaseDAO san = new Engage_Major_ReleaseDAO();
        private Config_MajorDAO cm = new Config_MajorDAO();
        private Config_Major_KindDAO config_Major_KindDAO = new Config_Major_KindDAO();
        private Engage_ResumeDAO jianli = new Engage_ResumeDAO();
        private Engage_InterviewDAO EngageDAO = new Engage_InterviewDAO();
        private Human_FileDAO hf = new Human_FileDAO();
        private SalaryStandardDAO sas = new SalaryStandardDAO();

        public async Task<ActionResult> RLZYUPDATE(Human_File s)
        {
            int se = await hf.QBUPDATE(s);
            if (se > 0)
            {
                return RedirectToAction("RLZY_fuhe");
            }
            else
            {
                return RedirectToAction("RLZY_fuhe");
            }
        }

        public ActionResult WSC()
        {
            var file = HttpContext.Request.Files["file"];//获取上传文件对象
                                                         //生成文件名
            string name = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            //获取后缀名
            string ext = file.FileName.Substring(file.FileName.LastIndexOf('.'));
            //相对路径
            string path = $"~/Uploaders/{DateTime.Now.ToString("yyyy/MM/dd/")}" + name + ext;
            //绝对路径
            string jpath = Server.MapPath(path);
            //创建上传文件对应的文件夹
            (new FileInfo(jpath)).Directory.Create();
            file.SaveAs(jpath);
            string result = path.Substring(path.LastIndexOf("~") + 1);
            return Json(new { path = result });
        }

        public async Task<ActionResult> RLZY_IDCX(int? id)
        {
            Human_File se = await hf.FuHe_idcx(id);
            se.register = usersDAO.gjid_name(se.register);
            ViewBag.s = se;
            return View();
        }

        public async Task<ActionResult> RLZY_IDCX2(int? id)
        {
            Human_File se = await hf.FuHe_idcx(id);
            se.register = usersDAO.gjid_name(se.register);
            ViewBag.s = se;
            return View();
        }

        public async Task<ActionResult> RLDA_ChanXu2(Human_File hu, int? currentPage)
        {
            FY<Human_File> chars = await hf.RLDA_ChanXu(hu, currentPage);
            ViewBag.z = chars.Rows;
            ViewBag.s = chars.List;
            ViewBag.m = chars.CurrentPage;
            ViewBag.h = ++chars.CurrentPage;
            ViewBag.a = --chars.CurrentPage;
            return View();
        }

        public async Task<ActionResult> RLDA_ChanXu(Human_File hu, int? currentPage)
        {
            FY<Human_File> chars = await hf.RLDA_ChanXu(hu, currentPage);
            ViewBag.z = chars.Rows;
            ViewBag.s = chars.List;
            ViewBag.m = chars.CurrentPage;
            ViewBag.h = ++chars.CurrentPage;
            ViewBag.a = --chars.CurrentPage;
            return View();
        }

        public async Task<ActionResult> RLZY_DACX2()
        {
            // await GetLists();
            await CMK();
            ViewBag.s = await san.yiji();
            return View();
        }

        /// <summary>
        ///    打开档案查询   //查询了一级分类  与 职位分类 职位名称
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> RLZY_DACX()
        {
            //await GetLists();
            await CMK();
            ViewBag.s = await san.yiji();
            return View();
        }

        public async Task<JsonResult> IIshow(int id, string name)
        {
            IEnumerable<ConfigFileThirdKind> se = await config.IIshow(id, name);
            return Json(se, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///   人力复核点击复核的页面
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> RLZY_fuheClick(int? id)
        {
            Human_File se = await hf.FuHe_idcx(id);
            se.register = usersDAO.gjid_name(se.register);
            ViewBag.s = se;
            return View();
        }

        /// <summary>
        ///  人力复核
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> RLZY_fuhe()
        {
            FY<Human_File> fy = await hf.show();
            ViewBag.s = fy.List;
            return View();
        }

        public async Task<ActionResult> show(int? currentPage)
        {
            FY<Engage_Resume> chars = await jianli.CXQB(currentPage);
            ViewBag.z = chars.Rows;
            ViewBag.s = chars.List;
            ViewBag.m = chars.CurrentPage;
            ViewBag.h = ++chars.CurrentPage;
            ViewBag.a = --chars.CurrentPage;
            return View();
        }

        /// <summary>
        ///  复核通过
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> FUHe_UPDATE(string id)
        {
            int se = await hf.FuHe_UPDATE(id);
            if (se > 0)
            {
                return RedirectToAction("RLZY_DACX");
            }
            else
            {
                return Content("0");
            }
        }

        /// <summary>
        ///  人力资源登记 显示页面
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> Index(int id)
        {
            ViewBag.sas = await sas.showALL();

            await idcx(id);
            await GetDropListl();
            await GetDropListl2();
            await GetDropListl3();
            return View();
        }

        /// <summary>
        ///  人力资源登记 添加
        /// </summary>
        /// <param name="files">salary_standard_id</param>
        /// <returns></returns>
        public async Task<ActionResult> HF_INSERT(Human_File files)
        {
            SalaryStandard salary = await sas.ChaYi(files.salary_standard_id);
            files.salary_standard_name = salary.standard_name;
            files.salary_sum = salary.salary_sum.ToString();

            files.first_kind_name = san.namese1(files.first_kind_id);
            files.second_kind_name = san.namese2(files.second_kind_id);
            files.third_kind_name = san.namese3(files.third_kind_id);
            int re = await hf.HF_INSERT(files);
            int rs = await jianli.RL_DJ(files.human_id);
            if (re > 0)
            {
                return RedirectToAction("RLZY_fuhe");
            }
            else
            {
                return Content("0");
            }
        }

        public async Task idcx(int id)
        {
            IEnumerable<Users> users = await usersDAO.UsersName();
            SelectList selectListItems = new SelectList(users, "u_id", "u_name");
            ViewBag.u = selectListItems;

            Engage_Resume en = await jianli.ID_Show(id);
            Engage_Interview se = await EngageDAO.ShuanXuan_SIDCX(en.res_id);

            se.checker = usersDAO.gjid_name(se.checker);

            ViewBag.s = se;
            ViewBag.d = en;
        }

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

        //public async Task GetLists()
        //{
        //    IEnumerable<Config_Major> zhaoping = await cm.ZhiWeiSheCX();
        //    SelectList selectListItems = new SelectList(zhaoping, "major_id", "major_name");
        //    ViewBag.f = selectListItems;
        //    ViewBag.q = zhaoping;

        //}

        /// <summary>
        ///
        /// 职位分类
        /// </summary>
        /// <returns></returns>
        public async Task CMK()
        {
            ViewBag.sd = await config_Major_KindDAO.ZhiWeiCX();
        }
    }
}