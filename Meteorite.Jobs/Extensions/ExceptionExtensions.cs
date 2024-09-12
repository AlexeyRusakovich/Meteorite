using Serilog;

namespace Meteorite.Jobs.Extensions
{
    public static class ExceptionExtensions
    {
        public static void HandleException(this Exception ex)
        {
           Log.Error("Job exception detected:");

            do
            {
                Log.Error("{0}\n{1}", ex.Message, ex.StackTrace);
            }
            while ((ex = ex.InnerException) != null);
        }
    }
}
