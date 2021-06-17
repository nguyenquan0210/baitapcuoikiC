using ModelEF.Dao;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestUngDung.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index(string id)
        {
            var dao = new SanPhamDao();
            var rs = dao.Find(id);
            return View(rs);

        }
       

       
    }
}