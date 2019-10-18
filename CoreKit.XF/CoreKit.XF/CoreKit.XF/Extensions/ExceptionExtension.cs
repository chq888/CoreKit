using System;
using System.Collections.Generic;
using System.Text;

namespace CoreKit.XF.Extensions
{
    public static class ExceptionExtension
    {
        public static string GetException(this Exception ex)
        {
            string[] error = ex.StackTrace.Split('/');

            while (ex != null && ex.InnerException != null)
            {
                if (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                }
            }

            return $"{ex.Message}::{error[error.Length - 1]}";
            //return new Exception();
        }

        public static Exception GetError(this Exception exception)
        {
            if (exception.InnerException != null)
            {
                return exception.InnerException.GetBaseException();
            }

            return exception;
        }
    }
}
