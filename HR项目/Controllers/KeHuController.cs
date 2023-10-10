using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAO;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.IO;
using System.Net;
using System.Text;

namespace HR项目.Controllers
{
    public class KeHuController : Controller
    {
        public Config_Public_CharDAO chardao = new Config_Public_CharDAO();
        private Config_MajorDAO majorDAO = new Config_MajorDAO();
        private Config_Major_KindDAO configDao = new Config_Major_KindDAO();

        private First_kindDAO kindDAO = new First_kindDAO();

        private FileSecondKindDAO file = new FileSecondKindDAO();

        private ConfigFileThirdKindDAO config = new ConfigFileThirdKindDAO();

        private PublicCharDAO charDAO = new PublicCharDAO();

        private UsersDAO users = new UsersDAO();

        // GET: KeHu

        #region

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 登录页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Dl()
        {
            return View();
        }

        public ActionResult Create(string name, string pwd)
        {
            try
            {
                WebMail.SmtpServer = "smtp.qq.com ";
                WebMail.SmtpPort = 25;
                WebMail.EnableSsl = true;
                WebMail.UserName = "2946068721@qq.com";
                WebMail.Password = "qkonaqkcbjqhdeae";
                WebMail.From = "2946068721@qq.com";
                WebMail.Send("3523966028@qq.com", "欢迎您的入职", "人事部门");
                return Content("成功");
            }
            catch (Exception)
            {
                return Content("失败");
            }
        }

        /// <summary>
        /// 进行登录
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> D(string name, string pwd)
        {
            Users u = await users.ChaYgAsync(name, pwd);
            return Json(u, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 进行显示所有I级机构设置信息
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Syzbw()
        {
            IEnumerable<First_kind> ie = await kindDAO.ChaYiAsync();
            return Json(ie, JsonRequestBehavior.AllowGet);
        }

        public ActionResult XiuYi()
        {
            return View();
        }

        public ActionResult Tian()
        {
            return View();
        }

        /// <summary>
        /// 进行添加
        /// </summary>
        /// <param name="first_Kind"></param>
        /// <returns></returns>
        public async Task<ActionResult> Tia(First_kind first_Kind)
        {
            int cg = await kindDAO.TianAsync(first_Kind);
            if (cg > 0)
            {
                return Content("1");
            }
            else
            {
                return View(first_Kind);
            }
        }

        /// <summary>
        /// 添加成功进入成功视图
        /// </summary>
        /// <returns></returns>
        public ActionResult Tiancg()
        {
            return View();
        }

        /// <summary>
        /// 进行删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> Sc(int id)
        {
            int cg = await kindDAO.ScAsync(id);
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
        /// 删除成功进入的页面
        /// </summary>
        /// <returns></returns>
        public ActionResult ScYe()
        {
            return View();
        }

        /// <summary>
        /// 进行显示修改页面
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> Xiu(int id)
        {
            ViewBag.s = await kindDAO.ChaYiAsync(id);
            return View();
        }

        /// <summary>
        /// 进行更改
        /// </summary>
        /// <param name="first_Kind"></param>
        /// <returns></returns>
        public async Task<ActionResult> Xiuu(First_kind first_Kind)
        {
            int cg = await kindDAO.XiuAsync(first_Kind);
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
        /// 修改成功进行入
        /// </summary>
        /// <returns></returns>
        public ActionResult XiuCg()
        {
            return View();
        }

        /// <summary>
        /// 进行显示II级机构设置
        /// </summary>
        /// <returns></returns>
        public ActionResult Eji()
        {
            return View();
        }

        public async Task<ActionResult> SLian()
        {
            IEnumerable<LianJi> ie = await file.ChaLian();
            return Json(ie, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 进行查询II级机构设置所有信息
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> ChaEji()
        {
            IEnumerable<FileSecondKind> kinds = await file.ChaSy();
            return Json(kinds, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 进行显示II级机构设置添加信息
        /// </summary>
        /// <returns></returns>
        public ActionResult TianEji()
        {
            return View();
        }

        /// <summary>
        /// 进行显示一级下拉框形式
        /// </summary>
        /// <returns></returns>
        private async Task GetYiJi()
        {
            IEnumerable<First_kind> first_s = await kindDAO.ChaLaAsync();
            SelectList s = new SelectList(first_s, "first_kind_id", "first_kind_name");
            ViewBag.s = s;
        }

        /// <summary>
        /// II级机构设置进行添加
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> TianE(FileSecondKind fileSecondKind)
        {
            int cg = await file.TianAsync(fileSecondKind);
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
        ///II级机构设置添加成功
        /// </summary>
        /// <returns></returns>
        public ActionResult EtianCg()
        {
            return View();
        }

        /// <summary>
        /// II级机构设置删除操作
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> ScEr(string id)
        {
            int cg = await file.ScAsync(id);
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
        /// I1级机构设置删除成功页面
        /// </summary>
        /// <returns></returns>
        public ActionResult ErScg()
        {
            return View();
        }

        /// <summary>
        /// II级机构设置的修改
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> ErXiu(string id)
        {
            ViewBag.s = await file.ChaYgAsync(id);
            return View();
        }

        /// <summary>
        ///  II级机构设置进行修改
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> EX(FileSecondKind fil)
        {
            int cg = await file.XiuAsync(fil);
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
        ///  II级机构设置修改成功页面
        /// </summary>
        /// <returns></returns>
        public ActionResult ErXCg()
        {
            return View();
        }

        /// <summary>
        /// 进行显示III级机构设置
        /// </summary>
        /// <returns></returns>
        public ActionResult Esan()
        {
            return View();
        }

        private ConfigFileThirdKindDAO c = new ConfigFileThirdKindDAO();

        /// <summary>
        ///进行查询所有III级机构
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> ChaSan()
        {
            IEnumerable<ConfigFileThirdKind> configs = await c.ChaSyAsync();
            return Json(configs, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// III级机构添加页面
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> SanTian()
        {
            ViewBag.s = await kindDAO.ChaLaAsync();
            return View();
        }

        /// <summary>
        /// 进行III级机构添加
        /// </summary>
        /// <param name="kind"></param>
        /// <returns></returns>
        public async Task<ActionResult> SanTia(ConfigFileThirdKind kind)
        {
            int cg = await config.Tian(kind);
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
        /// III级机构添加成功页面
        /// </summary>
        /// <returns></returns>
        public ActionResult SanCg()
        {
            return View();
        }

        /// <summary>
        /// 进行III级机构删除
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> SanSc(ConfigFileThirdKind kind)
        {
            int cg = await config.ScAsync(kind);
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
        /// 进行查III级机构的具体信息
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> ChaSanYg(string id)
        {
            ViewBag.s = await kindDAO.ChaLaAsync(); //查询一级信息
            ViewBag.v = await config.ChaYiAsync(id);
            return View();
        }

        /// <summary>
        /// 进行III级机构的修改
        /// </summary>
        /// <param name="thirdKind"></param>
        /// <returns></returns>
        public async Task<ActionResult> SanX(ConfigFileThirdKind thirdKind)
        {
            int cg = await config.XIuAsync(thirdKind);
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
        /// III级机构修改成功
        /// </summary>
        /// <returns></returns>
        public ActionResult SanXCg()
        {
            return View();
        }

        /// <summary>
        /// III级机构删除成功
        /// </summary>
        /// <returns></returns>
        public ActionResult SanScCg()
        {
            return View();
        }

        #endregion
        #region  ZZB
        #region  职称设置

        public async Task<int> ZhiChengDEL(int id)
        {
            return await chardao.ZhiChengDEL(id);
        }

        public ActionResult ZhiCeng()
        {
            return View();
        }

        public async Task<ActionResult> ZhiCengCX()
        {
            IEnumerable<Config_Public_Char> config_Public_Chars = await chardao.ZhiChegCX();
            return Json(config_Public_Chars, JsonRequestBehavior.AllowGet);
        }

        #endregion
        #region  职位分类设置

        public ActionResult ZhiWei()
        {
            return View();
        }

        public async Task<ActionResult> ZhiweiCX()
        {
            Console.WriteLine(123);
            IEnumerable<Config_Major_Kind> list = await configDao.ZhiWeiCX();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public async Task<int> ZhiWeiDEL(int id)
        {
            return await configDao.ZhiWeiDel(id);
        }

        /// <summary>
        /// 职称分类添加
        /// </summary>
        /// <param name="configs"></param>
        /// <returns></returns>
        public async Task<int> ZhiWeiINSERT(string configs)
        {
            Console.WriteLine("添加///" + configs);
            return await configDao.ZhiWeiINSERT(configs);
        }

        public ActionResult ZhhiWeiadd()
        {
            return View();
        }

        #endregion

        #region 职位设置

        /// <summary>
        /// 职位设置查询
        /// </summary>
        /// <returns>config_s</returns>
        public async Task<ActionResult> ZhiWeiShe_Show()
        {
            IEnumerable<Config_Major> config_s = await majorDAO.ZhiWeiSheCX();
            return Json(config_s, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 职位设置删除
        /// </summary>
        /// <param name="id">职位id</param>
        /// <returns></returns>
        public async Task<int> ZhiWeiSheDEL(int id)
        {
            return await majorDAO.ZhiWeiSheDEL(id);
        }

        /// <summary>
        /// 职位设置
        /// </summary>
        /// <returns></returns>
        public ActionResult ZhiWeiShe()
        {
            return View();
        }

        /// <summary>
        /// 职位设置添加页面
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> ZhiWeiSheadd()
        {
            await GetLists();
            await GetLists2();

            return View();
        }

        /// <summary>
        /// 职位设置添加
        /// </summary>
        /// <param name="config_"></param>
        /// <returns></returns>
        public async Task<ActionResult> ZhiWeiSheINSERT(Config_Major config_)
        {
            if (ModelState.IsValid)
            {
                int result = await majorDAO.ZhiWeiSheINSERT(config_);
                if (result > 0)
                {
                    return RedirectToAction("ZhiWeiShe");
                }
                else
                {
                    return RedirectToAction("ZhiWeiShe");
                }
            }
            else
            {
                return View(config_);
            }
        }

        public async Task GetLists()
        {
            IEnumerable<Config_Major_Kind> config_Major_Kinds = await configDao.ZhiWeiCX();
            SelectList selectListItems = new SelectList(config_Major_Kinds, "major_kind_id", "major_kind_id");
            ViewBag.s = selectListItems;
        }

        public async Task GetLists2()
        {
            IEnumerable<Config_Major_Kind> config_Major_Kinds = await configDao.ZhiWeiCX();
            SelectList selectListItems = new SelectList(config_Major_Kinds, "major_kind_name", "major_kind_name");
            ViewBag.sls = selectListItems;
        }

        #endregion

        #region 公共属性设置

        /// <summary>
        /// 查看公共属性
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> GongShuXingCX()
        {
            IEnumerable<Config_Public_Char> config_Public_Chars = await chardao.GongShuXingCX();
            return Json(config_Public_Chars, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> GongShuXing()
        {
            return View();
        }

        public async Task<ActionResult> GongShuXingINSERT(Config_Public_Char config)
        {
            if (ModelState.IsValid)
            {
                int result = await chardao.GongShuXingINSERT(config);
                if (result > 0)
                {
                    return RedirectToAction("GongShuXing");
                }
                else
                {
                    return RedirectToAction("GongShuXing");
                }
            }
            else
            {
                return View(config);
            }
        }

        public ActionResult GongShuXingadd()
        {
            return View();
        }

        #endregion

        #endregion

        /// <summary>
        /// 进行进入薪酬项目设置
        /// </summary>
        /// <returns></returns>
        public ActionResult salaryItem()
        {
            return View();
        }

        /// <summary>
        /// 进行查询薪酬项目
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> XinZi()
        {
            IEnumerable<PublicChar> chars = await charDAO.ChaSyAsync("薪酬设置");
            return Json(chars, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 薪酬项目添加页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Salary_item_register()
        {
            return View();
        }

        /// <summary>
        /// 薪酬项目添加
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> XcTian(PublicChar publicChar)
        {
            int cg = await charDAO.TinaAsync(publicChar);
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
        /// 薪酬项目删除
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> XinSc(int id)
        {
            int cg = await charDAO.ScAsync(id);
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