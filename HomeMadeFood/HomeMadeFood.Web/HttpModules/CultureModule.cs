using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;

namespace HomeMadeFood.Web.HttpModules
{
    public class CultureModule : IHttpModule
    {
        public void Dispose()
        {
            
        }

        public void Init(HttpApplication context)
        {
            context.BeginRequest += this.Context_BeginRequest;
        }

        protected void Context_BeginRequest(object sender, EventArgs e)
        {
            CultureInfo culture = (CultureInfo) CultureInfo.CurrentUICulture.Clone();
            culture.NumberFormat.NumberDecimalSeparator = ".";
            Thread.CurrentThread.CurrentCulture = culture;

            var urlParts = HttpContext.Current.Request.Url.AbsoluteUri.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

            if (urlParts.Count() > 2)
            {
                string lang = urlParts[2];

                if (lang == "en")
                {
                    Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en");
                    Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en");
                }
            }
        }
    }
}