using ModelEF.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestUngDung.common;
using TestUngDung.Models;

namespace TestUngDung.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Login user)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();


                var rs = dao.Login(user.UserName, Encryptor.EncryptorMD5(user.Password));



                if (rs == 0)
                {

                    ModelState.AddModelError("", "Tài Khoản Không Tồn Tại");


                }
                else
                {
                    if (rs == -1)
                    {
                        ModelState.AddModelError("", "Mật Khẩu Không Đúng ");

                    }
                    else
                    {
                        if (rs == 2)
                        {
                            ModelState.AddModelError("", "Tài khoản bị chặn");
                        }
                        else
                        {
                            if (rs == -2)
                            {
                                /*ModelState.AddModelError("", "Chỉ Đăng nhập bằng tài Khoản ADmin?");*/
                                Session.Add(Constants.USER_SESSION, user);
                                return RedirectToAction("Index", "Home");
                            }
                            else
                            {

                                /*ModelState.AddModelError("", "Đăng nhập thành công");*/
                                Session.Add(Constants.USER_SESSION, user);
                                return RedirectToAction("Index", "Home");
                            }
                        }
                    }
                }

            }
            return View("Index");
        }
    }
}