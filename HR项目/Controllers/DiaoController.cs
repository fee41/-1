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
    public class DiaoController : Controller
    {
        private FileSecondKindDAO fileSecondKindDAO = new FileSecondKindDAO();

        private HumanFileDAO hu = new HumanFileDAO();

        private MajorKindDAO ma = new MajorKindDAO();

        private SalaryStandardDAO sa = new SalaryStandardDAO();

        private ChangeDAO changeDAO = new ChangeDAO();

        // GET: Diao
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 进行联级调动登记查询显示
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> SLian()
        {
            IEnumerable<LianJi> ie = await fileSecondKindDAO.ChaLian1();
            return Json(ie, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 进行查询信息
        /// </summary>
        /// <returns></returns>
        public ActionResult Cha(string tiao)
        {
            ViewBag.s = tiao;
            return View();
        }

        /// <summary>
        /// 进行查询具体信息
        /// </summary>
        /// <param name="tiao"></param>
        /// <returns></returns>
        public async Task<ActionResult> ChaJu(int pageSize, int currentPage, string tiao)
        {
            FenYe<HumanFile> fenYe = await hu.FenYe(pageSize, currentPage, tiao);
            return Json(fenYe, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 进入调动登记详情
        /// </summary>
        /// <returns></returns>
        public ActionResult Jin(string id)
        {
            ViewBag.s = id;
            return View();
        }

        /// <summary>
        /// 进入调动登记详情(用户)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> ChaC(string id)
        {
            HumanFile human = await hu.ChaYiAsync(id);
            return Json(human, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 进行查询职位
        /// </summary>
        public async Task<ActionResult> Lian2()
        {
            IEnumerable<LianJi> ie = await ma.ChaLian();
            return Json(ie, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 进行查询薪酬名称
        /// </summary>
        public async Task<ActionResult> Chaxin()
        {
            IEnumerable<SalaryStandard> ie = await sa.Cha2();
            return Json(ie, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 进行查询总薪酬
        /// </summary>
        public async Task<ActionResult> CZong(string id)
        {
            int ie = await sa.Chajin(id);
            return Json(ie, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 进行添加成功
        /// </summary>
        /// <param name="change"></param>
        /// <returns></returns>
        public async Task<ActionResult> Tina(Change change)
        {
            int cg = await changeDAO.Tian(change);
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
        /// 薪酬发放管理
        /// </summary>
        /// <returns></returns>
        public ActionResult TianSh()
        {
            return View();
        }

        /// <summary>
        /// 进入调动登记详情(用户)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> ChaC1(string id)
        {
            HumanFile human = await hu.ChaYiAsync1(id);
            return Json(human, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 进行显示复核信息
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="currentPage"></param>
        /// <returns></returns>
        public async Task<ActionResult> Xs(int pageSize, int currentPage)
        {
            FenYe<Change> salaries = await changeDAO.FenYe(pageSize, currentPage);
            return Json(salaries, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 进行审核页面
        /// </summary>
        /// <returns></returns>
        public ActionResult F(string id)
        {
            ViewBag.s = id;
            return View();
        }

        /// <summary>
        /// 进行显示套更改的信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> Xs1(string id)
        {
            Change change = await changeDAO.Yg(id);
            return Json(change, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 进行修改
        /// </summary>
        /// <param name="change"></param>
        /// <returns></returns>
        public async Task<ActionResult> Xiu(Change change, string tg)
        {
            if (tg == "通过")
            {
                int cg = await changeDAO.Xiu(change);
                if (cg > 0)
                {
                    return Content("1");
                }
                else
                {
                    return Content("0");
                }
            }
            else
            {
                int cg = await changeDAO.Xiu1(change);
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
        /// 进行入调动查询
        /// </summary>
        /// <returns></returns>
        public ActionResult ChaDiao()
        {
            return View();
        }

        /// <summary>
        /// 进入调动查询列表
        /// </summary>
        /// <param name="tiao"></param>
        /// <returns></returns>
        public ActionResult ChaKan(string tiao)
        {
            ViewBag.s = tiao;
            return View();
        }

        /// <summary>
        /// 进行显示
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Ck(string tiao)
        {
            IEnumerable<Change> ie = await changeDAO.changesAsync(tiao);
            return Json(ie, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 调动登记查看
        /// </summary>
        /// <returns></returns>
        public ActionResult KanJu(string id)
        {
            ViewBag.s = id;
            return View();
        }

        /// <summary>
        /// 进行查询我需要的信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> Zui(string id)
        {
            Change change = await changeDAO.changesAsync1(id);
            return Json(change, JsonRequestBehavior.AllowGet);
        }
    }
}