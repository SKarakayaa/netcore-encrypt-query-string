using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;

namespace Filters
{
    public class DecryptQueryStringParameterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var dataProtectionProvider = DataProtectionProvider.Create("WebQuery");
            var protector = dataProtectionProvider.CreateProtector("WebQuery.QueryStrings");

            Dictionary<string, object> decryptedParamaters = new Dictionary<string, object>();

            if (context.HttpContext.Request.Query["q"].ToString() != null)
            {
                string decrptedString = protector.Unprotect(context.HttpContext.Request.Query["q"].ToString());
                string[] getRandom = decrptedString.Split('[');

                var format = new CultureInfo("en-GB");
                var dataCheck = Convert.ToDateTime(getRandom[2], format);

                TimeSpan diff = Convert.ToDateTime(DateTime.Now, format) - dataCheck;

                if (diff.Minutes > 30)
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { action = "Error", controller = "Error" }));

                string[] paramsArrs = getRandom[1].Split(',');

                for (int i = 0; i < paramsArrs.Length; i++)
                {
                    string[] paramArr = paramsArrs[i].Split('=');
                    decryptedParamaters.Add(paramArr[0], Convert.ToString(paramArr[1]));
                }
            }
            for (int i = 0; i < decryptedParamaters.Count; i++)
            {
                context.ActionArguments[decryptedParamaters.Keys.ElementAt(i)] = decryptedParamaters.Values.ElementAt(i);
            }
        }
    }
}