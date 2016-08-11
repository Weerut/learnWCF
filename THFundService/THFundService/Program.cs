using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Web;
using EikonEnvManager.ProcessManagement;
using TR.AppServer.Common.Interfaces;
using TR.AppServer.Logging;
using TR.AppServer.ServiceRouting;

namespace WeerutTestWCFService
{
    internal static class Program
    {
        private static readonly ILogger Logger = TR.AppServer.Logging.Logger.Default;

        private static void Main()
        {
            AppDomain.CurrentDomain.UnhandledException += UnhandledException;
            XmlConfigurator.Configure();
            Logger.LogInfo("THFundService initializing...");

            SetConsoleTitle("THFundService");

            var hostFactory = new RoutedServiceHostFactory();
            using (var host = hostFactory.CreateServiceHost(typeof(THFundService)))
            {
                host.Open();
                foreach (var ep in host.Description.Endpoints)
                {
                    Logger.LogInfo("Name: " + ep.Name + "; Address: " + ep.Address);
                }

                Logger.LogInfo("THFundService started");

                EnvironmentManager.Instance.WaitForShutdown();
            }
        }

        #region Unhandled Exception Handler

        private static void UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var exc = e.ExceptionObject as Exception;
            if (null != exc)
            {
                TR.AppServer.Logging.Logger.Default.LogError("Unhandled exception occurred: {0}", exc);
                TR.AppServer.Logging.Logger.Default.LogException(exc);
            }

            FileSystemUtils.GenerateDump();
        }

        #endregion

        #region Helpers

        private static void SetConsoleTitle(string title)
        {
            if (Process.GetCurrentProcess().MainWindowHandle == IntPtr.Zero)
            {
                return;
            }

            int pid = Process.GetCurrentProcess().Id;
            Console.Title = string.Format("{0} ({1}) [pid: {2}]", title, PlatformInfo.Name, pid);
        }

        /// <summary>
        /// Platform information utility class.
        /// </summary>
        internal static class PlatformInfo
        {
            /// <summary>
            /// Gets the name of the system architecture that the CLR is running under.
            /// </summary>
            internal static string Name
            {
                get
                {
                    if (Marshal.SizeOf(typeof(IntPtr)) == 8)
                    {
                        return "x64";
                    }

                    return "x86";
                }
            }
        }

        #endregion
    }
}