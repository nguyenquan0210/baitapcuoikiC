using ModelEF.Dao;
using ModelEF.Model;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace TestUngDung.Areas.Admin.Controllers
{
    public class LoaiSPController : BaseController
    {
        // GET: Admin/MenuCategory
        public ActionResult Index(string searchString, int page = 1, int pagesize = 5)
        {
            var user = new MenuTypeDao();
            var model = user.List(searchString);
            @ViewBag.SearchString = searchString;
            return View(model.ToPagedList(page, pagesize));
        }

        public ActionResult Detais(string id)
        {
            var dao = new MenuTypeDao();
            var rs = dao.Find(id);
            return View(rs);

        }
        public string SlugName(string text)
        {
            for (int i = 32; i < 48; i++)
            {
                text = text.Replace(((char)i).ToString(), " ");
            }
            text = text.ToLower();
            text = text.Replace(".", "-");
            text = text.Replace(" ", "-");
            text = text.Replace(",", "-");
            text = text.Replace(";", "-");
            text = text.Replace(":", "-");
            Regex regex = new Regex(@"\p{IsCombiningDiacriticalMarks}+");
            string SlugName = text.Normalize(System.Text.NormalizationForm.FormD);
            return regex.Replace(SlugName, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }
        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(loai_sp id)
        {
            if (ModelState.IsValid)
            {
                var dao = new MenuTypeDao();




                string rs = dao.Insert(id);



                if (!string.IsNullOrEmpty(rs))
                {
                    setAlert("Thêm thành công menu có tên: '" + id.Name.ToUpper() + "' ", "success");
                    return RedirectToAction("Index", "LoaiSP");

                }
                else
                {
                    setAlert("Thêm menu không thành công!!!", "error");
                    //ModelState.AddModelError("", "Tạo tài khoản không thành công");
                }
            }
            else
            {
                setAlert("Thêm menu không thành công!!!", "error");
                return RedirectToAction("Index", "LoaiSP");
            }
            return View();
        }
        [HttpGet]
        public ActionResult Update(string id)
        {
            var dao = new MenuTypeDao();
            var rs = dao.Find(id);

            return View(rs);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(loai_sp Entity)
        {


            if (ModelState.IsValid)
            {
                Entity.ID = SlugName(Entity.ID);

                if (new MenuTypeDao().Update(Entity))
                {
                    setAlert("Cập nhật dữ liệu thành công!", "success");
                    return RedirectToAction("Index", "LoaiSP");
                }
                else
                {
                    ModelState.AddModelError("", "Không thể cập nhật thông tin! Vui lòng kiểm tra lại!");
                }
            }
            return View(Entity);
        }
        [HttpGet]
        public ActionResult Delete(string id)
        {
            var dao = new MenuTypeDao().Delete(id);
            setAlert("Xoá bản ghi thành công!", "success");
            return RedirectToAction("Index", "QLKhachHang");
        }
    }
}