using ModelEF.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestUngDung.Models;

namespace TestUngDung.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        private const string CartSession = "CartSession";
        // GET: CartItem
        public ActionResult Index()
        {
            //Create Session
            var Cart = Session[CartSession];
            //Create List 
            var List = new List<Cart>();
            if (Cart != null)
            {
                List = (List<Cart>)Cart;
            }
            return View(List);
        }

        public ActionResult AddItem(string ProductID, int Quantity)
        {
            //Get Product ID
            var Product = new SanPhamDao().Find(ProductID);
            //Create Session
            var Cart = Session[CartSession];
            if (Cart != null)
            {
                var List = (List<Cart>)Cart;
                if (List.Exists(x => x.Product.ID == ProductID))
                {
                    foreach (var item in List)
                    {
                        //Nếu nó đã có trong cart thì cộng thêm số lượng.
                        if (item.Product.ID == ProductID)
                        {
                            item.Quantity += Quantity;
                        }
                    }
                }
                else
                {
                    //Create new cart item
                    var item = new Cart();
                    //Set value
                    item.Product = Product;
                    item.Quantity = Quantity;
                    //Add item
                    List.Add(item);
                }
                //Set Session
                Session[CartSession] = List;
            }
            else
            {
                //Create new cart item
                var item = new Cart();
                //Set value
                item.Product = Product;
                item.Quantity = Quantity;
                //Create List 
                var List = new List<Cart>();
                //Add item
                List.Add(item);
                //Set Session
                Session[CartSession] = List;
            }
            return RedirectToAction("Index");
        }

        public ActionResult minus(string ProductID, int Quantity)
        {
            //Get Product ID
            var Product = new SanPhamDao().Find(ProductID);
            //Create Session
            var Cart = Session[CartSession];
            if (Cart != null)
            {
                var List = (List<Cart>)Cart;
                if (List.Exists(x => x.Product.ID == ProductID))
                {
                    foreach (var item in List)
                    {
                        //Nếu nó đã có trong cart thì cộng thêm số lượng.
                        if (item.Product.ID == ProductID)
                        {
                            item.Quantity += Quantity;
                        }
                    }
                }
                else
                {
                    //Create new cart item
                    var item = new Cart();
                    //Set value
                    item.Product = Product;
                    item.Quantity = Quantity;
                    //Add item
                    List.Add(item);
                }
                //Set Session
                Session[CartSession] = List;
            }
            else
            {
                //Create new cart item
                var item = new Cart();
                //Set value
                item.Product = Product;
                item.Quantity = Quantity;
                //Create List 
                var List = new List<Cart>();
                //Add item
                List.Add(item);
                //Set Session
                Session[CartSession] = List;
            }
            return RedirectToAction("Index");
        }


        public ActionResult DeleteAll()
        {
            Session[CartSession] = null;
            return RedirectToAction("Index");
        }

        public ActionResult Delete(string ID)
        {
            var SessionCart = (List<Cart>)Session[CartSession];
            SessionCart.RemoveAll(x => x.Product.ID == ID);
            Session[CartSession] = SessionCart;
            return RedirectToAction("Index");
        }
    }
}