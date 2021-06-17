using ModelEF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestUngDung.Models
{
    [Serializable]
    public class Cart
    {
        public san_pham Product { set; get; }
        public int Quantity { set; get; }
    }
}