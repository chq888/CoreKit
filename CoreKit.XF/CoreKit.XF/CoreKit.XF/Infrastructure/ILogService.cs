using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace CoreKit.XF.Infrastructure
{

    public interface ILogService
    {
        void Debug(string message);

        void Warning(string message);

        void Error(Exception exception);
    }


    public class ConsoleLogService : ILogService
    {
        public void Debug(string message)
        {
            System.Diagnostics.Debug.WriteLine(message);
        }

        public void Warning(string message)
        {
            Debug($"# {nameof(Warning)}");
            Debug(message);
        }

        public void Error(Exception exception)
        {
            Debug($"# {nameof(Error)}");
            Debug(exception.ToString());
        }
    }

}
