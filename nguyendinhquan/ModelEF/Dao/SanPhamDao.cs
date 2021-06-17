using ModelEF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelEF.Dao
{
    public class SanPhamDao
    {
        private NguyenDinhQuanContext dbcont;

        public SanPhamDao()
        {

            dbcont = new NguyenDinhQuanContext();
        }
        public string Insert(san_pham entityUser)
        {
            try
            {
                dbcont.san_pham.Add(entityUser);

                dbcont.SaveChanges();
                return entityUser.Name;
            }
            catch (Exception)
            {
                return null;
            }

        }
      
        public bool Delete(string id)
        {
            try
            {
                var Product = dbcont.san_pham.Find(id);
                dbcont.san_pham.Remove(Product);
                dbcont.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool Update(san_pham entityUser)
        {
            try
            {
                var Cate = dbcont.san_pham.Find(entityUser.ID);
                /* Cate.Name = Entity.Name;
                 Cate.ParentID = Entity.ParentID;
                 Cate.Slug = Entity.Slug;*/
                Cate.Name = entityUser.Name;
                Cate.UnitCost = entityUser.UnitCost;

                Cate.Image = entityUser.Image;

                Cate.ID_l = entityUser.ID_l;

                Cate.Description = entityUser.Description;

                Cate.Status = entityUser.Status;

               

                dbcont.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public san_pham Find(string id)
        {
            return dbcont.san_pham.Find(id);

        }

        public List<san_pham> ListAll()
        {
            return dbcont.san_pham.ToList();
        }
        public List<san_pham> ListSP(string searchString)
        {

            if (!string.IsNullOrEmpty(searchString))
                return dbcont.san_pham.Where(x => x.ID.Contains(searchString) || x.Name.Contains(searchString)
                || x.UnitCost.ToString().Contains(searchString)
                || x.ID_l.ToString().Contains(searchString)).OrderByDescending(x => x.UnitCost).ThenBy(x => x.ID_l).ToList();
            return dbcont.san_pham.OrderByDescending(x => x.UnitCost).ThenBy(x => x.ID_l).ToList();
        }
      /*  public List<CThoa_don> ListHD(string searchString)
        {

            return dbcont.CThoa_don.Where(x => x.id_SP.ToString().Contains(searchString)).ToList();

        }
        public List<comment> ListComment(string searchString)
        {

            return dbcont.comments.Where(x => x.id_sp.ToString().Contains(searchString)).ToList();

        }*/
        public List<san_pham> ListViewDetail(string id)
        {
            return dbcont.san_pham.Where(x => x.ID.Contains(id)).ToList();

        }
    }
}
