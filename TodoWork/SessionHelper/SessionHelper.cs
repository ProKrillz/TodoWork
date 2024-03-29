﻿using System.Text;

namespace TodoWork.SessionHelper;

public static class SessionHelper
{
    public static void SetSessionString(this ISession session, string value, string key)
         => session.Set(key, Encoding.UTF8.GetBytes(value));
}
