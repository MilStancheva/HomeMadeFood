using System;

namespace HomeMadeFood.Web.Common.Messaging
{
    [Serializable]
    public class ToastMessage
    {
        public string Title { get; set; }

        public string Message { get; set; }

        public ToastType ToastType { get; set; }

        public bool IsSticky { get; set; }
    }
}