using ModelEF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelEF.Dao
{
    public class KhachHangDao
    {
        private NguyenDinhQuanContext dbcont;

        public KhachHangDao()
        {

            dbcont = new NguyenDinhQuanContext();
        }
        public string Insert(UserAccount entityUser)
        {

            try
            {

                dbcont.UserAccounts.Add(entityUser);
                dbcont.SaveChanges();
                return entityUser.UserName;
            }
            catch (Exception)
            {
                return null;
            }

        }
        public UserAccount Find(string id)
        {
            return dbcont.UserAccounts.Find(id);

        }
        public List<UserAccount> ListKH(string searchString)
        {

            if (!string.IsNullOrEmpty(searchString))
                return dbcont.UserAccounts.Where(x => x.ID.ToString().Contains(searchString)
                || x.UserName.ToString().Contains(searchString) 
                || x.Status.ToString().Contains(searchString)).ToList();
            return dbcont.UserAccounts.ToList();
        }

        /*  public hoa_don FindHD(string id)
          {
              return dbcont.hoa_don.Find(id);

          }*/
      
        public bool Delete(string id)
        {
            try
            {
                var accc = dbcont.UserAccounts.Find(id);
                dbcont.UserAccounts.Remove(accc);
                dbcont.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool Update(UserAccount entityUser)
        {
            try
            {
                var Cate = dbcont.UserAccounts.Find(entityUser.ID);
                /* Cate.Name = Entity.Name;
                 Cate.ParentID = Entity.ParentID;
                 Cate.Slug = Entity.Slug;*/
                Cate.UserName = entityUser.UserName;

                Cate.Password = entityUser.Password;

                Cate.Status = entityUser.Status;

               

             



                dbcont.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Blocked(UserAccount entityUser)
        {
            try
            {
                var Cate = dbcont.UserAccounts.Find(entityUser.ID);
                Cate.Status = entityUser.Status;
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
