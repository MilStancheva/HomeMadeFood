using System;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace HomeMadeFood.Web.CustomFilters
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class AjaxValidateAntiForgeryTokenAttribute : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            try
            {
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    this.ValidateRequestHeader(filterContext.HttpContext.Request);
                }
                else
                {
                    AntiForgery.Validate();
                }
            }
            catch (HttpAntiForgeryException)
            {
                throw new HttpAntiForgeryException("Anti forgery token not found");
            }
        }

        private void ValidateRequestHeader(HttpRequestBase request)
        {
            string cookieToken = string.Empty;
            string formToken = string.Empty;
            string tokenValue = request.Headers["VerificationToken"];
            if (!string.IsNullOrEmpty(tokenValue))
            {
                string[] tokens = tokenValue.Split(',');
                if (tokens.Length == 2)
                {
                    cookieToken = tokens[0].Trim();
                    formToken = tokens[1].Trim();
                }
            }

            AntiForgery.Validate(cookieToken, formToken);
        }
    }
}