using ModelEF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ModelEF.Dao
{
    public class UserDao
    {

        private NguyenDinhQuanContext dbcont;
        public UserDao()
        {

            dbcont = new NguyenDinhQuanContext();
        }

       

        public int Login(string user, string pass)
        {
            var rs = dbcont.UserAccounts.SingleOrDefault(x => x.UserName.Trim()  == user.Trim());

            if (rs == null)
            {
                return 0;
            }   
            else //Tồn tại tài khoản, sẽ kiểm tra tiếp xem có trùng pass hay có bị khoá ko?
            {
                if (rs.Password.Trim() != pass.Trim())
                    return -1; //không khớp password 

                else //Khớp username, khớp pass
                {
                    if (rs.Status == 2)
                        return 2; // Tài khoản bị khoá
                    else
                         if (rs.Status == 1)
                            return 1; //tài khoản admin
                        else
                            return -2; //Tài khoản đúng hoạt động bình thường
                }
            }


        }
        public List<UserAccount> List()
        {
            return dbcont.UserAccounts.ToList();

        }
    }
}
