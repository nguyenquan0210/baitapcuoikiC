using ModelEF.Dao;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestUngDung.common;

namespace TestUngDung.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();

        }
        public ActionResult Logout()
        {
            Session[Constants.USER_SESSION] = null;
            return RedirectToAction("Index", "Home");
        }

        [ChildActionOnly]
        public ActionResult Product(string searchString, int page = 1, int pagesize = 10)
        {

            var user = new SanPhamDao();

            var model = user.ListSP(searchString);

            @ViewBag.SearchString = searchString;
            return PartialView(model.ToPagedList(page, pagesize));
        }

        [ChildActionOnly]
        public PartialViewResult MenuHeader(string searchString)
        {
            var user = new KhachHangDao();

            var model = user.ListKH(searchString);

            return PartialView(model);
        }
        [ChildActionOnly]
        public PartialViewResult footer()
        {
            
            return PartialView();
        }
        [ChildActionOnly]
        public PartialViewResult MenuType()
        {
            var model = new MenuTypeDao().ListAll();
            return PartialView(model);
        }
       
    }
}