using DemoLk.auth;
using DemoLk.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoLk.Context
{
    public class AppDbInitializer: System.Data.Entity.DropCreateDatabaseIfModelChanges<AppDbContext>
    {
        protected override void Seed(AppDbContext context)
        {

            //Проинициализируем базу дефолтнымы пользователями
            List<User> users = new List<User>()
            {
              new User()
            {
            Login="admin",
            PasswordHash=Crypto.Crypto.EncryptStringAES("123456",TokenFactory.secret),
            Role="admin",
            LastName="Лупкин",
            FirstName="Иван",
            MiddleName="Иванович",
            },
        };

            context.Users.AddRange(users);
            context.SaveChanges();
        }
    }
}