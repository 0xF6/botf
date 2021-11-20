﻿using System.Reflection;

namespace Deployf.Botf;

public static class BotRoutesExtensions
{
    public static string? GetAuthPolicy(this MethodInfo method)
    {
        if (method.GetCustomAttribute<AllowAnonymousAttribute>() != null)
        {
            return null;
        }

        var methodAuth = method.GetCustomAttribute<AuthorizeAttribute>();
        if (methodAuth != null)
        {
            return methodAuth.Policy ?? string.Empty;
        }

        var classAuth = method.DeclaringType!.GetCustomAttribute<AuthorizeAttribute>();
        if (classAuth != null)
        {
            return classAuth.Policy ?? string.Empty;
        }

        return null;
    }

    public static string? GetActionDescription(this MethodInfo method)
    {
        var action = method.GetCustomAttribute<ActionAttribute>();
        if (action == null)
        {
            return null;
        }

        return action.Desc;
    }
}

public static class NumberExtensions
{
    public static long Base64(this string base64)
    {
        if (base64.Length % 4 != 0)
        {
            base64 += "===".Substring(0, 4 - (base64.Length % 4));
        }
        var bytes = Convert.FromBase64String(base64);
        return BitConverter.ToInt64(bytes);
    }

    public static string Base64(this long value)
    {
        var bytes = BitConverter.GetBytes(value);
        return Convert.ToBase64String(bytes).Replace("=", "");
    }
}