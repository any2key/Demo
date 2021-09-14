using DemoLk.auth;
using DemoLk.Models.Identity;
using DemoLk.Models.Request;
using DemoLk.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace DemoLk.Controllers
{
    public class AuthorizeController : ControllerBaseEx
    {
        /// <summary>
        /// Логин
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("Login")]
        public HttpResponseMessage Login([FromBody] LoginRequest req)
        {
            return SafeRun(_ =>
            {
                //Получаем юзера по логину
                var user = db.Users.FirstOrDefault(e => e.Login.ToLower() == req.Login.ToLower());
                if (user == null)
                    throw new Exception($"Пользователь {req.Login} не найден или введен неправильный пароль");

                //проверяем пароль, предварительно дешифруя
                if (Crypto.Crypto.DecryptStringAES(user.PasswordHash, TokenFactory.secret).ToLower() != req.Password.ToLower())
                    throw new Exception($"Пользователь {req.Login} не найден или введен неправильный пароль");

                //генерируем токен acces и refresh токены
                var token = tokenFactory.GenerateToken(new Models.Identity.Token() { Expired = DateTime.Now.AddHours(2), Role = user.Role, User = user.Login });
                var refresh = tokenFactory.GenerateToken(new Models.Identity.Token() { Expired = DateTime.Now.AddDays(30), Role = user.Role, User = user.Login });
                user.RefreshToken = refresh;
                db.SaveChanges();

                return new DataResponse<LoginResponse>() { IsOk = true, Data = new LoginResponse() { Role = user.Role, User = user.Login, Token = token, RefreshToken = refresh } };
            });
        }


        /// <summary>
        /// Обновить токен
        /// </summary>
        /// <param name="refresh"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("Refresh")]
        public HttpResponseMessage Refresh([FromBody] RefreshRequest refresh)
        {
            return SafeRun(_ =>
            {
                //если пришел пустой токен бросаем ощибку
                if (string.IsNullOrEmpty(refresh.Refresh))
                    return Response.BadResponse("");
                Models.Identity.Token refreshToken = tokenFactory.DecodeToken(refresh.Refresh);
                //проверяем срок действия
                if (refreshToken.Expired < DateTime.Now)
                    throw new Exception("Токен просрочен");
                //обновляем access и refresh
                var user = db.Users.FirstOrDefault(e => e.Login.ToLower() == refreshToken.User);
                var token = tokenFactory.GenerateToken(new Models.Identity.Token() { Expired = DateTime.Now.AddHours(2), User = refreshToken.User, Role = refreshToken.Role });
                var newRefresh = tokenFactory.GenerateToken(new Models.Identity.Token() { Expired = DateTime.Now.AddDays(30), User = refreshToken.User, Role = refreshToken.Role });

                user.RefreshToken = newRefresh;
                db.SaveChanges();
                return new DataResponse<LoginResponse>() { IsOk = true, Data = new LoginResponse() { Role = user.Role, User = user.Login, Token = token, RefreshToken = newRefresh } };

            });
        }

        /// <summary>
        /// Проверка, авторизован ли пользователь, действителен ли токен
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage CheckAuth()
        {
            return SafeRun(_ =>
            {
                return Response.OK;
            });
        }

        /// <summary>
        /// Список пользователей
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetUsers()
        {
            return SafeRun(_ =>
            {
                var users = db.Users.ToArray();
                return new DataResponse<IEnumerable<User>>() { IsOk = true, Data = users };
            });
        }

        /// <summary>
        /// Добавить или обновить полдьзователя
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage AddOrUpdateUser(AddOrUpdateReq<User> req)
        {
            return SafeRun(_ =>
            {
                if (req.Sign == AddOrUpdate.Add)
                {
                    req.Data.PasswordHash = req.Data.PassUI.GetHashCode().ToString();
                    db.Users.Add(req.Data);
                }
                else
                {
                    var u = db.Users.First(e => e.Id == req.Data.Id);
                    u.FirstName = req.Data.FirstName;
                    u.MiddleName = req.Data.MiddleName;
                    u.LastName = req.Data.LastName;
                    u.Login = req.Data.Login;
                    u.Role = req.Data.Role;
                    u.PasswordHash = Crypto.Crypto.EncryptStringAES(req.Data.PassUI, TokenFactory.secret);
                }
                db.SaveChanges();
                return Response.OK;
            });
        }

       

        [HttpGet]
        public HttpResponseMessage Remove(int id)
        {
            return SafeRun(_ =>
            {

                var token = tokenFactory.DecodeToken(Request.Headers.Authorization.Parameter);
                var CurrentUser = db.Users.FirstOrDefault(e => e.Login.ToLower() == token.User.ToLower());


                if (CurrentUser.Id == id)
                    throw new Exception("Нельзя удалить пользователя из под которого осуществлен вход в систему. Для удаления этого пользователя авторизуйтесь под другой учетной записью.");

                db.Users.Remove(db.Users.FirstOrDefault(e => e.Id == id));
                db.SaveChanges();
                return Response.OK;
            });
        }


    }
}