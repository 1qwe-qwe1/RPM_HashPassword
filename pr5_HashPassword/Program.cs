using HashPasswords;
using pr5_HashPassword.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pr5_HashPassword
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Система регистрации пользователей");

            Console.Write("Введите логин: ");
            string login = Console.ReadLine();

            Console.Write("Введите пароль: ");
            string password = Console.ReadLine();

            string hashedPassword = Hash.HashPassword(password);

            Users newUser = new Users
            {
                Login = login,
                PasswordHash = hashedPassword,
                RegistrationDate = DateTime.Now
            };

            try
            {
                Helper helper = new Helper();
                helper.CreateUser(newUser, password);

                Console.WriteLine($"Пользователь {newUser.Login} создан!");
                Console.WriteLine($"Хеш пароля: {newUser.PasswordHash}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при сохранении в БД: {ex.Message}");
            }

            Console.ReadKey();

            Console.WriteLine($"Пользователь {newUser.Login} создан!");
            Console.WriteLine($"Хеш пароля: {newUser.PasswordHash}");
        }
    }
}
