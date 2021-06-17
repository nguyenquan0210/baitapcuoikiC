using ModelEF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelEF.Dao
{
    public class MenuTypeDao
    {
        private NguyenDinhQuanContext dbcont;

        public MenuTypeDao()
        {

            dbcont = new NguyenDinhQuanContext();
        }
        public string Insert(loai_sp entityUser)
        {
            try
            {
                dbcont.loai_sp.Add(entityUser);
                dbcont.SaveChanges();
                return entityUser.ID;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public bool Update(loai_sp entityUser)
        {
            try
            {
                var Cate = dbcont.loai_sp.Find(entityUser.ID);
                Cate.Name = entityUser.Name;
                Cate.Description = entityUser.Description;
               


                dbcont.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public loai_sp Find(string id)
        {
            return dbcont.loai_sp.Find(id);

        }
        public List<loai_sp> ListAll()
        {

            return dbcont.loai_sp.ToList();
        }
        public List<loai_sp> List(string searchString)
        {

            if (!string.IsNullOrEmpty(searchString))
                return dbcont.loai_sp.Where(x => x.Name.ToString().Contains(searchString) || x.ID.ToString().Contains(searchString)).ToList();
            return dbcont.loai_sp.ToList();
        }
        public bool Delete(string id)
        {
            try
            {
                var mn = dbcont.loai_sp.Find(id);
                dbcont.loai_sp.Remove(mn);
                dbcont.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
