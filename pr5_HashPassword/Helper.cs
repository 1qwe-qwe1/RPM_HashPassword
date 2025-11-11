using pr5_HashPassword.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HashPasswords;

namespace pr5_HashPassword
{
    public class Helper
    {
        private Entities1 _context; 

        public Helper()
        {
            _context = new Entities1();
        }

        
        public static Entities1 GetContext()
        {
            return new Entities1();
        }

        public void CreateUser(Users user, string password)
        {
            user.PasswordHash = Hash.HashPassword(password);
            user.RegistrationDate = DateTime.Now;

            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public bool UserExists(string login)
        {
            return _context.Users.Any(u => u.Login == login);
        }

    }
}
