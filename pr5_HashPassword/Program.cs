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

            int attempts = 3;
            bool success = false;

            while (attempts > 0 && !success)
            {
                Console.Write("Введите логин: ");
                string login = Console.ReadLine();

                // Проверка логина
                if (string.IsNullOrWhiteSpace(login))
                {
                    Console.WriteLine("Ошибка: Логин не может быть пустым!");
                    continue;
                }

                if (login.Contains(" "))
                {
                    Console.WriteLine("Ошибка: Логин не может содержать пробелы!");
                    continue;
                }

                if (login.Length < 4)
                {
                    Console.WriteLine("Ошибка: Логин должен содержать минимум 4 символа!");
                    continue;
                }

                Console.Write("Введите пароль: ");
                string password = Console.ReadLine();

                // Проверка пароля
                if (string.IsNullOrWhiteSpace(password))
                {
                    Console.WriteLine("Ошибка: Пароль не может быть пустым!");
                    continue;
                }

                if (password.Contains(" "))
                {
                    Console.WriteLine("Ошибка: Пароль не может содержать пробелы!");
                    continue;
                }

                if (password.Length < 6)
                {
                    Console.WriteLine("Ошибка: Пароль должен содержать минимум 6 символов!");
                    continue;
                }

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

                    if (helper.UserExists(login))
                    {
                        attempts--;
                        Console.WriteLine($"Ошибка: Пользователь с логином '{login}' уже существует! Осталось попыток: {attempts}");
                        continue;
                    }

                    helper.CreateUser(newUser, password);
                    success = true;

                    Console.WriteLine($"Пользователь {newUser.Login} создан!");
                    Console.WriteLine($"Хеш пароля: {newUser.PasswordHash}");
                }
                catch (Exception ex)
                {
                    attempts--;
                    Console.WriteLine($"Ошибка при сохранении в БД: {ex.Message} Осталось попыток: {attempts}");
                }
            }

            if (!success)
            {
                Console.WriteLine("Регистрация не удалась. Попробуйте позже.");
            }

            Console.ReadKey();
        }
    }
}
