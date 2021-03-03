using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace SitefinityWebApp.Extensions
{
    public static class HtmlHelpers
    {
        public static readonly MethodInfo RegisterResourceMethod = Assembly.Load("Telerik.Sitefinity.Frontend").GetType("Telerik.Sitefinity.Frontend.Mvc.Helpers.ResourceHelper").GetMethod("RegisterResource", BindingFlags.Static | BindingFlags.NonPublic);
        public static readonly MethodInfo TryConfigureScriptManagerMethod = Assembly.Load("Telerik.Sitefinity.Frontend").GetType("Telerik.Sitefinity.Frontend.Mvc.Helpers.ResourceHelper").GetMethod("RegisterResource", BindingFlags.Static | BindingFlags.NonPublic, null, new[] { typeof(string), typeof(IHttpHandler) }, null);

        /// <summary>
        /// This custom helper assists in placing script in the specified section: "top", "bottom"        
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="scriptPath"></param>
        /// <param name="sectionName"></param>
        /// <param name="throwException"></param>
        /// <param name="tryUseScriptManager"></param>
        /// <returns></returns>
        public static MvcHtmlString ScriptCustom(this HtmlHelper helper, string scriptPath, string sectionName, bool throwException, bool tryUseScriptManager = true)
        {
            if (tryUseScriptManager && (bool)TryConfigureScriptManagerMethod.Invoke(null, new object[] { scriptPath, helper.ViewContext.HttpContext.CurrentHandler }))
                return MvcHtmlString.Empty;

            return RegisterResourceMethod.Invoke(null, new object[] { helper.ViewContext.HttpContext, scriptPath, 0, sectionName, throwException }) as MvcHtmlString;
        }
    }
}


