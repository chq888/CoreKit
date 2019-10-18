using CoreKit.XF.Infrastructure;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CoreKit.XF.Helpers
{
    public class TaskHelper
    {

        private readonly ILogService loggingService;

        private Action whenStarting;
        private Action whenFinished;


        private TaskHelper()
        {
            loggingService = new ConsoleLogService();
        }

        public static TaskHelper Create()
        {
            return new TaskHelper();
        }

        public TaskHelper WhenStarting(Action action)
        {
            whenStarting = action;

            return this;
        }

        public TaskHelper WhenFinished(Action action)
        {
            whenFinished = action;

            return this;
        }

        public async Task<bool> TryWithErrorHandlingAsync(
            Task task,
            Func<Exception, Task<bool>> customErrorHandler = null)
        {
            var taskWrapper = new Func<Task<object>>(() => WrapTaskAsync(task));
            var result = await TryWithErrorHandlingAsync(taskWrapper(), customErrorHandler);

            if (result == null)
            {
                return false;
            }

            return true;
        }

        public async Task<T> TryWithErrorHandlingAsync<T>(
            Task<T> task,
            Func<Exception, Task<bool>> customErrorHandler = null, Func<bool> condition = null)
        {
            whenStarting?.Invoke();

            if (condition != null && !condition())
            {
                return default(T);
            }

            try
            {
                T actualResult = await task;
                return actualResult;
            }
            catch (TaskCanceledException exception)
            {
                loggingService?.Debug($"{exception}");
            }
            catch (Exception exception)
            {
                loggingService?.Error(exception);

                if (customErrorHandler != null)
                {
                    await customErrorHandler?.Invoke(exception);
                }
            }
            finally
            {
                whenFinished?.Invoke();
            }

            return default(T);
        }

        private async Task<object> WrapTaskAsync(Task innerTask)
        {
            await innerTask;

            return new object();
        }

    }
}
