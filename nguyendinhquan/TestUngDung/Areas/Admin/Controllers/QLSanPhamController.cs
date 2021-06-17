using ModelEF.Dao;
using ModelEF.Model;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Xml.Linq;

namespace TestUngDung.Areas.Admin.Controllers
{
    public class QLSanPhamController : BaseController
    {
        // GET: Admin/QLSanPham
       
            public ActionResult Index(string searchString, int page = 1, int pagesize = 5)
            {
                var user = new SanPhamDao();
                var model = user.ListSP(searchString);
                @ViewBag.SearchString = searchString;
                return View(model.ToPagedList(page, pagesize));
            }
        [HttpGet]
        public ActionResult Detais(string id)
            {
                var dao = new SanPhamDao();
                var rs = dao.Find(id);
                SetViewBag(rs.ID_l);
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
            public ActionResult Create(string id)
            {
            SetViewBag();
            return View();
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult Create(san_pham id)
            {
            SetViewBag();
           
                if (ModelState.IsValid)
                {
                    var dao = new SanPhamDao();




                    string rs = dao.Insert(id);



                    if (!string.IsNullOrEmpty(rs))
                    {
                        setAlert("Thêm thành công sản phẩm có tên: '" + id.Name.ToUpper() + "' ", "success");
                        return RedirectToAction("Index", "QLSanPham");

                    }
                    else
                    {
                        setAlert("Thêm không thành công sản phẩm có tên: '" + id.Name.ToUpper() + "' !!!", "error");
                        //ModelState.AddModelError("", "Tạo tài khoản không thành công");
                    }
                return View();
            }
                else
                {
                    setAlert("Vui Nhập Đầy Đủ Các Trường Bắc Buộc", "error");
                return View();
            }
            
            }

       

        [HttpGet]
            public ActionResult Update(string id)
            {
            
            var dao = new SanPhamDao();
                var rs = dao.Find(id);
                SetViewBag(rs.ID_l);
                return View(rs);

            }
            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult Update(san_pham Entity)
            {


                if (ModelState.IsValid)
                {
                    Entity.ID = SlugName(Entity.ID);

                    if (new SanPhamDao().Update(Entity))
                    {
                        setAlert("Cập nhật dữ liệu thành công!", "success");
                        return RedirectToAction("Index", "QLSanPham");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Không thể cập nhật thông tin! Vui lòng kiểm tra lại!");
                    }
                }
                return View(Entity);
            }
           /* [HttpGet]
            [ChildActionOnly]
            public ActionResult HoaDon(string searchString)
            {
                var user = new SanPhamDao();

                var model = user.ListHD(searchString);


                @ViewBag.SearchString = searchString;
                return View(model);
            }*/
           /* [HttpGet]
            [ChildActionOnly]
            public ActionResult BinhLuan(string searchString)
            {
                var user = new SanPhamDao();

                var model = user.ListComment(searchString);


                @ViewBag.SearchString = searchString;
                return View(model);
            }*/
            //Delete
            [HttpGet]
            public ActionResult Delete(string id)
            {
                var dao = new SanPhamDao().Delete(id);
                setAlert("Xoá bản ghi thành công!", "success");
                return RedirectToAction("Index", "QLKhachHang");
            }
        public void SetViewBag(string selectedid = null)
        {
            var dao = new MenuTypeDao();
            ViewBag.MenuCategoryID = new SelectList(dao.ListAll(), "ID", "Name", selectedid);
        }
        public JsonResult LoadImages(string id)
            {
                SanPhamDao Dao = new SanPhamDao();
                var product = Dao.Find(id);
                var images = product.Image;
                XElement xImages = XElement.Parse(images);
                List<string> listImagesReturn = new List<string>();

                foreach (XElement element in xImages.Elements())
                {
                    listImagesReturn.Add(element.Value);
                }
                return Json(new
                {
                    data = listImagesReturn
                }, JsonRequestBehavior.AllowGet);
            }
            public JsonResult SaveImages(long id, string images)
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                var listImages = serializer.Deserialize<List<string>>(images);

                XElement xElement = new XElement("Images");

                foreach (var item in listImages)
                {
                    var subStringItem = item.Substring(22);
                    xElement.Add(new XElement("Image", subStringItem));
                }
                SanPhamDao Dao = new SanPhamDao();
                try
                {
                    /*Dao.UpdateImages(id, xElement.ToString());*/
                    return Json(new
                    {
                        status = true
                    });
                }
                catch (Exception ex)
                {
                    return Json(new
                    {
                        status = false
                    });
                }

            }
        
    }
}