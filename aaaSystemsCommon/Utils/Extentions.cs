﻿using aaaSystemsCommon.Models;
using System.Reflection;

namespace aaaSystemsCommon.Utils
{
    public static class Extentions
    {
        public static string GetRoot(this Type type) => type?
            .GetCustomAttribute<EntityRootAttribute>()?.Root ?? nameof(type);

        public static string GetInfo(this User user)
        {
            return GetFormatString("Имя", user.Name) +
                   GetFormatString("Номер", user.Phone) +
                   GetFormatString("Роль", user.Role.ToString());
        }

        public static string GetFormatString(string tittle, string? arg, string emoji = null) => ($"\t{tittle + ":"!,-11} {arg,8} {emoji}\n");
    }
}
