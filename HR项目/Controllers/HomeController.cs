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
    public class HomeController : Controller
    {
        private RolesDAO rolesDAO = new RolesDAO();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public async Task<ActionResult> Ll(int rid)
        {
            List<Tree> trees = await rolesDAO.GetList(rid);
            return Json(trees, JsonRequestBehavior.AllowGet);
        }
    }
}