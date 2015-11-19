using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Models
{
    public static class ServerInfo
    {
        public const string Server = "http://localhost:50619";
        public const string JsonContetnType = "application/json";
        public const string StringQueryType = "application/x-www-form-urlencoded";

        private const string LoginInRoute = "/api/Account/Login";
        private const string RegisterRoute = "/api/Account/Register";
        private const string MessegesRoute = "/api/messages/";
        private const string UsersRoute = "/api/users";
        private const string SendMessageRoute = "/api/messages";

        public static string GetLoginRoute()
        {
            return ServerInfo.Server + ServerInfo.LoginInRoute;
        }

        public static string GetRegisterRoute()
        {
            return ServerInfo.Server + ServerInfo.RegisterRoute;
        }

        public static string GetMessegesRoute(string userName)
        {
            return ServerInfo.Server + ServerInfo.MessegesRoute + userName + "/";
        }

        public static string GetUsersRoute()
        {
            return ServerInfo.Server + ServerInfo.UsersRoute;
        }

        public static string GetSendMessageRoute()
        {
            return ServerInfo.Server + ServerInfo.SendMessageRoute;
        }

        public static string GetDeleteMessegesRoute(string messageId)
        {
            return ServerInfo.Server + ServerInfo.MessegesRoute + messageId + "/";
        }

        public static string GetEditMessegesRoute(string messageId)
        {
            return ServerInfo.Server + ServerInfo.MessegesRoute + messageId + "/";
        }
    }
}
