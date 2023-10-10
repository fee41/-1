using DAO;
using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HR项目.Controllers
{
    public class XinCBiaoController : Controller
    {
        private PublicCharDAO publicCharDAO = new PublicCharDAO();

        private SalaryStandardDAO sa = new SalaryStandardDAO();

        private SalaryStandardDetailsDAO sx = new SalaryStandardDetailsDAO();

        private UsersDAO usersDAO = new UsersDAO();

        private SalaryGrantDAO SalaryGrantDAO = new SalaryGrantDAO();

        private SalaryGrantDetailsDAO g = new SalaryGrantDetailsDAO();

        private HumanFileDAO f = new HumanFileDAO();

        // GET: XinCBiao
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 进行查询用户信息
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Yh()
        {
            IEnumerable<Users> users = await usersDAO.ChaSyAsync();
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 进行查询	薪酬项目名称
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> ChaXinMc()
        {
            IEnumerable<PublicChar> chars = await publicCharDAO.ChaSyAsync("薪酬设置");
            return Json(chars, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 进行事务的添加进入salary_standard_details和salary_standard
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> TianShi(string jin, string name, string bh, SalaryStandard salary)
        {
            int cg = await sa.Tian(jin, name, bh, salary);
            if (cg == 0)
            {
                return Content("0");
            }
            else
            {
                return Content("1");
            }
        }

        /// <summary>
        /// 进入薪酬标准登记复核
        /// </summary>
        /// <returns></returns>
        public ActionResult Fu()
        {
            return View();
        }

        /// <summary>
        /// 进行分页显示薪酬标准登记复核
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> FenXin(int pageSize, int currentPage)
        {
            FenYe<SalaryStandard> salaries = await sa.FenYe(pageSize, currentPage);
            return Json(salaries, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 进行显示复核信息
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> FuXin(string id)
        {
            ViewBag.s = await sa.ChaYi(id);
            return View();
        }

        /// <summary>
        /// 进行查询金额
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> FuX(string id)
        {
            IEnumerable<SalaryStandardDetails> ie = await sx.ChaYi(id);
            return Json(ie, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 进行事务的修改进入salary_standard_details和salary_standard
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> XiuShi(string id, string jin, SalaryStandard salary)
        {
            int cg = await sa.Xiu(jin, id, salary);
            if (cg == 0)
            {
                return Content("0");
            }
            else
            {
                return Content("1");
            }
        }

        /// <summary>
        /// 进行查询薪酬标准查询
        /// </summary>
        /// <returns></returns>
        public ActionResult ChaTiao()
        {
            return View();
        }

        /// <summary>
        /// 进入薪酬标准登记查询
        /// </summary>
        /// <param name="tiao"></param>
        /// <returns></returns>
        public ActionResult MoHuYe(string tiao)
        {
            //FenYe<SalaryStandard> e = await sa.MoHuCha(pageSize, currentPage, tiao);
            ViewBag.s = tiao;
            return View();
        }

        /// <summary>
        /// 薪酬标准登记查询分页
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> MoHu(int pageSize, int currentPage, string tiao)
        {
            FenYe<SalaryStandard> ye = await sa.MoHuCha(pageSize, currentPage, tiao);
            return Json(ye, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 进入薪酬标准登记查询具体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> ChaDj(string id)
        {
            ViewBag.s = await sa.ChaYi(id);
            return View();
        }

        /// <summary>
        /// 进行查询具体金额
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> Ca(string id)
        {
            IEnumerable<SalaryStandardDetails> ie = await sx.ChaYi(id);
            return Json(ie, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 进行进入薪酬标准变更
        /// </summary>
        /// <returns></returns>
        public ActionResult Bian()
        {
            return View();
        }

        /// <summary>
        /// 进入薪酬标准登记查询
        /// </summary>
        /// <param name="tiao"></param>
        /// <returns></returns>
        public ActionResult BianGen(string tiao)
        {
            ViewBag.s = tiao;
            return View();
        }

        /// <summary>
        /// 薪酬标准登记查询分页
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> MoHuYe1(int pageSize, int currentPage, string tiao)
        {
            FenYe<SalaryStandard> ye = await sa.MoHuCha(pageSize, currentPage, tiao);
            return Json(ye, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 进行入具体-薪酬标准登记变更
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> BianJu(string id)
        {
            ViewBag.s = await sa.ChaYi(id);
            return View();
        }

        /// <summary>
        /// 薪酬标准登记变更具体金额
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> BianJin(string id)
        {
            IEnumerable<SalaryStandardDetails> ie = await sx.ChaYi(id);
            return Json(ie, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 进行更改
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Gen(string id, string jin, SalaryStandard salary)
        {
            int cg = await sa.Gen(jin, id, salary);
            if (cg == 0)
            {
                return Content("0");
            }
            else
            {
                return Content("1");
            }
        }

        /// <summary>
        /// 薪酬发放登记
        /// </summary>
        /// <returns></returns>
        public ActionResult Fa()
        {
            return View();
        }

        /// <summary>
        /// 进行我是要查级
        /// </summary>
        public ActionResult CFaJi(string tiao)
        {
            ViewBag.i = tiao;
            return View();
        }

        /// <summary>
        /// 机进行查询几级信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> ChaJuTi(string tiao)
        {
            IEnumerable<HumanFile> ie = await f.SXuanAsync(tiao);
            return Json(ie, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 进入薪酬发放登记具体信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult ChaJuDeng(string id, string name)
        {
            ViewBag.s = id;
            ViewBag.i = name;
            return View();
        }

        public async Task<ActionResult> ChaD2(string id)
        {
            IEnumerable<HumanFile> ie = await f.SXuanAsync(id);
            return Json(ie, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> ChaD3(string id)
        {
            IEnumerable<HumanFile> ie = await f.SXuanAsync3(id);
            return Json(ie, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 进行查询登记具体信息
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> ChaD(string id)
        {
            IEnumerable<HumanFile> ie = await f.SXuanAsync1(id);
            return Json(ie, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 进行查询登记具体信息1
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> ChaD1(string id)
        {
            IEnumerable<SalaryStandardDetails> ie = await sx.ChaYi(id);
            return Json(ie, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 进行登记
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Den(List<SalaryGrantDetails> submittedDat, string djr, string time, List<SalaryGrant> l, string id)
        {
            int cg = await g.FindAsync(submittedDat, djr, time, l, id);
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
        /// 进入薪酬发放登记审核页面
        /// </summary>
        /// <returns></returns>
        public ActionResult FDeng()
        {
            return View();
        }

        /// <summary>
        /// 进行分页查询薪酬发放登记审核
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> CHaFu(int pageSize, int currentPage)
        {
            FenYe<SalaryGrant> fenYe = await SalaryGrantDAO.FenYe(pageSize, currentPage);
            return Json(fenYe, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 进行具体复核信息
        /// </summary>
        /// <returns></returns>
        public ActionResult JuFu(string id, string bh)
        {
            ViewBag.s = id;
            ViewBag.i = bh;
            return View();
        }

        // private string THE_UID = "adminq"; //用户名
        //  private string THE_KEY = "8497FA9DEAB7998FF278A74B37A000FB"; //接口秘钥

        /// <summary>返回UTF-8编码发送接口地址</summary>
        /// <param name="receivePhoneNumber">目的手机号码（多个手机号请用半角逗号隔开）</param>
        /// <param name="receiveSms">短信内容，最多支持400个字，普通短信70个字/条，长短信64个字/条计费</param>
        /// <returns></returns>
        //public string GetPostUrl(string smsMob, string smsText)
        //{
        //    string postUrl = "http://utf8.sms.webchinese.cn/?Uid=" + THE_UID + "&key=" + THE_KEY + "&smsMob=" + smsMob + "&smsText=" + smsText;
        //    return postUrl;
        //}

        /// <summary> 发送短信，得到返回值</summary>
        //public string PostSmsInfo(string url)
        //{
        //    //调用时只需要把拼成的URL传给该函数即可。判断返回值即可
        //    string strRet = null;
        //    if (url == null || url.Trim().ToString() == "")
        //    {
        //        return strRet;
        //    }
        //    string targeturl = url.Trim().ToString();
        //    try
        //    {
        //        HttpWebRequest hr = (HttpWebRequest)WebRequest.Create(targeturl);
        //        hr.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1)";
        //        hr.Method = "GET";
        //        hr.Timeout = 30 * 60 * 1000;
        //        WebResponse hs = hr.GetResponse();
        //        Stream sr = hs.GetResponseStream();
        //        StreamReader ser = new StreamReader(sr, Encoding.Default);
        //        strRet = ser.ReadToEnd();
        //    }
        //    catch (Exception ex)
        //    {
        //        strRet = null;
        //    }
        //    return strRet;
        //}

        /// <summary>确认返回信息 </summary>
        //public string GetResult(string strRet)
        //{
        //    int result = 0;
        //    try
        //    {
        //        result = int.Parse(strRet);
        //        switch (result)
        //        {
        //            case -1:
        //                strRet = "没有该用户账户";
        //                break;

        //            case -2:
        //                strRet = "接口密钥不正确,不是账户登陆密码";
        //                break;

        //            case -21:
        //                strRet = "MD5接口密钥加密不正确";
        //                break;

        //            case -3:
        //                strRet = "短信数量不足";
        //                break;

        //            case -11:
        //                strRet = "该用户被禁用";
        //                break;

        //            case -14:
        //                strRet = "短信内容出现非法字符";
        //                break;

        //            case -4:
        //                strRet = "手机号格式不正确";
        //                break;

        //            case -41:
        //                strRet = "手机号码为空";
        //                break;

        //            case -42:
        //                strRet = "短信内容为空";
        //                break;

        //            case -51:
        //                strRet = "短信签名格式不正确,接口签名格式为：【签名内容】";
        //                break;

        //            case -6:
        //                strRet = "IP限制";
        //                break;

        //            default:
        //                strRet = "发送短信数量：" + result;
        //                break;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        strRet = ex.Message;
        //    }
        //    return strRet;
        //}

        /// <summary>
        /// 进行复核
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> FheCg(List<SalaryGrantDetails> submittedDat, string djr, string time, string jin, List<HumanFile> h)
        {
            int cg = await g.FuAsync(submittedDat, djr, time, jin, h);

            //string f = GetPostUrl("15576386183", "您的工资1000");
            //string s = PostSmsInfo(f);
            //string g = GetResult(s);

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
        /// 进入薪酬发放查询
        /// </summary>
        /// <returns></returns>
        public ActionResult ChaFa(string id)
        {
            return View();
        }

        /// <summary>
        /// 进行查询薪酬发放查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult CHAF(string tiao)
        {
            ViewBag.s = tiao;
            return View();
        }

        /// <summary>
        /// 薪酬发放查询查询分页
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> MoHuYe2(int pageSize, int currentPage, string tiao)
        {
            FenYe<SalaryGrant> ye = await SalaryGrantDAO.FenYetiao(pageSize, currentPage, tiao);
            return Json(ye, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 进行查询薪酬发放查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult JFh(string id)
        {
            ViewBag.s = id;
            return View();
        }

        /// <summary>
        /// 进行查询薪酬发放查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> CFJu(string id)
        {
            IEnumerable<SalaryGrantDetails> de = await g.ChaAsync(id);
            return Json(de, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 进行查询薪酬发放查询的资助项
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> CFJu1(string id)
        {
            IEnumerable<SalaryStandardDetails> de = await sx.ChaYi(id);
            return Json(de, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 进行查询薪酬发放查询的资助项
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> CFJu2(string id)
        {
            SalaryGrant de = await SalaryGrantDAO.ChaYi(id);
            return Json(de, JsonRequestBehavior.AllowGet);
        }
    }
}