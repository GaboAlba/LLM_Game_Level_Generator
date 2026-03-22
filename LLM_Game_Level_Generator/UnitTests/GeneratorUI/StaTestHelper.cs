namespace UnitTests
{
    using System.IO;
    using System.Runtime.ExceptionServices;

    /// <summary>
    /// Helper for running WPF-dependent code on an STA thread in xUnit tests.
    /// </summary>
    internal static class StaTestHelper
    {
        private static readonly object AppLock = new();
        private const string jsonName = "api_keys.json";

        private static void EnsureApplication()
        {
            if (!File.Exists(jsonName))
            {
                var jsonString = """[{"Provider":"OpenAI","Key":"mockKey"}]""";
                File.WriteAllText(jsonName, jsonString);
            }

            lock (AppLock)
            {
                if (System.Windows.Application.Current == null)
                {
                    new System.Windows.Application { ShutdownMode = System.Windows.ShutdownMode.OnExplicitShutdown };
                }
            }
        }

        public static void RunOnSta(Action action)
        {
            Exception? exception = null;
            var thread = new Thread(() =>
            {
                EnsureApplication();

                try
                {
                    action();
                }
                catch (Exception ex)
                {
                    exception = ex;
                }
            });
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            thread.Join();

            if (exception != null)
            {
                ExceptionDispatchInfo.Capture(exception).Throw();
            }

            File.Delete(jsonName);
        }

        public static T RunOnSta<T>(Func<T> func)
        {
            T result = default!;
            Exception? exception = null;
            var thread = new Thread(() =>
            {
                EnsureApplication();

                try
                {
                    result = func();
                }
                catch (Exception ex)
                {
                    exception = ex;
                }
            });
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            thread.Join();

            if (exception != null)
            {
                ExceptionDispatchInfo.Capture(exception).Throw();
            }

            return result;
        }
    }
}
