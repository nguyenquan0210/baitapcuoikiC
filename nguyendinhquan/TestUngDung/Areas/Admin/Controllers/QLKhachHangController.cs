using ModelEF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using ModelEF.Dao;
using PagedList;
using TestUngDung.common;

namespace TestUngDung.Areas.Admin.Controllers
{
    public class QLKhachHangController : BaseController
    {
        // GET: Admin/QLKhachHang
        public ActionResult Index(string searchString, int page = 1, int pagesize = 5)
        {

            var user = new KhachHangDao();

            var model = user.ListKH(searchString);


            @ViewBag.SearchString = searchString;
            return View(model.ToPagedList(page, pagesize));
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
        public ActionResult Detais(string id)
        {
            var dao = new KhachHangDao();
            var rs = dao.Find(id);
            return View(rs);

        }
        //Delete
        [HttpGet]
        public ActionResult Delete(string id)
        {
            var dao = new KhachHangDao().Delete(id);
            setAlert("Xoá bản ghi thành công!", "success");
            return RedirectToAction("Index", "QLKhachHang");
        }
        [HttpGet]
        public ActionResult Create(string id)
        {
            var dao = new KhachHangDao();

            var rs = dao.Find(id);
            if (rs != null)
                return View(rs);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserAccount id)
        {
            if (ModelState.IsValid)
            {
                var dao = new KhachHangDao();
                var pass = Encryptor.EncryptorMD5(id.Password);
                id.Password = pass;

                string rs = "";

                rs = dao.Insert(id);



                if (!string.IsNullOrEmpty(rs))
                {
                    setAlert("Cập nhập thành công tài khoản có tên: '" + id.UserName.ToUpper() + "' ", "success");
                    return RedirectToAction("Index", "QLKhachHang");

                }
                else
                {
                    setAlert("Cập nhập tài khoản không thành công!!!", "error");
                    //ModelState.AddModelError("", "Tạo tài khoản không thành công");
                }
            }
            return View();
        }
        [HttpGet]
        public ActionResult Update(string id)
        {
            var dao = new KhachHangDao();
            var rs = dao.Find(id);

            return View(rs);

        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(UserAccount Entity)
        {


            if (ModelState.IsValid)
            {
                Entity.ID = SlugName(Entity.ID);
               /* var pass = Encryptor.EncryptorMD5(Entity.Password);
                Entity.Password = pass;*/
                if (new KhachHangDao().Update(Entity))
                {
                    setAlert("Cập nhật dữ liệu thành công!", "success");
                    return RedirectToAction("Index", "QLKhachHang");
                }
                else
                {
                    ModelState.AddModelError("", "Không thể cập nhật thông tin! Vui lòng kiểm tra lại!");
                }
            }
            return View(Entity);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Blocked(UserAccount Entity)
        {


            if (ModelState.IsValid)
            {
                Entity.ID = SlugName(Entity.ID);


                if (new KhachHangDao().Blocked(Entity))
                {
                    setAlert("Chặn tài khoản '" + Entity.UserName.ToUpper() + "' thành công!", "success");
                    return RedirectToAction("Index", "QLKhachHang");
                }
                else
                {
                    ModelState.AddModelError("", "Không thể Chặn tài khoản! Vui lòng kiểm tra lại!");
                }
            }
            return View(Entity);
        }
    }
}