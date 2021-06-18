using ServerStatusMonitor.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using System.Configuration;
using System.Timers;

namespace ServerStatusMonitor
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ServerStatusMonitorTray());
        }

        public class ServerStatusMonitorTray : ApplicationContext
        {
            ConfigForm configForm = new ConfigForm();
            System.Timers.Timer timer = new System.Timers.Timer();
            Uri uri;
            Ping ping = new Ping();
            bool isPingable = false;
            bool isMonitoring = false;
            bool isNotified = false;
            string siteToMonitor;
            int interval;
            private NotifyIcon trayIcon;

            public ServerStatusMonitorTray()
            {
                timer.Elapsed += PingURL;
                trayIcon = new NotifyIcon()
                {
                    Icon = Resources.AppIcon,
                    ContextMenu = new ContextMenu(new MenuItem[]
                    {
                        new MenuItem("Config", ShowConfig),
                        new MenuItem("Start", ToggleMonitoring),
                        new MenuItem("Exit", Exit)
                    }),
                    Visible = true
                };
            }

            private void ShowConfig(object sender, EventArgs e)
            {
                configForm.ShowDialog();
            }

            private void ToggleMonitoring(object sender, EventArgs e)
            {
                Console.WriteLine("Toggling monitor.");
                isMonitoring = !isMonitoring;
                switch (isMonitoring)
                {
                    case true:
                        LoadSettings();
                        Console.WriteLine("Target: " + siteToMonitor);
                        Console.WriteLine("Interval: " + interval);
                        timer.Interval = interval > 0 ? interval * 1000 * 60 : 5 * 1000 * 60;
                        timer.Start();
                        PingURL();
                        trayIcon.ContextMenu.MenuItems[1].Text = "Stop";
                        break;
                    case false:
                        timer.Stop();
                        trayIcon.ContextMenu.MenuItems[1].Text = "Start";
                        trayIcon.Icon = Resources.AppIcon;
                        break;
                }
                Console.WriteLine("Done toggling monitor.");
                
            }

            private void Exit(object sender, EventArgs e)
            {
                trayIcon.Visible = false;
                Application.Exit();
            }

            private void LoadSettings()
            {
                if (!string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["SiteToMonitor"]))
                {
                    siteToMonitor = ConfigurationManager.AppSettings["SiteToMonitor"];
                }
                int.TryParse(ConfigurationManager.AppSettings["Interval"], out interval);
            }

            private void PingURL(Object source, ElapsedEventArgs e) 
            {
                PingURL();
            }

            private void PingURL()
            {
                uri = new Uri(siteToMonitor);
                Console.WriteLine("Pinging host.");
                try
                {
                    PingReply reply = ping.Send(uri.Host);
                    Console.WriteLine("Reply: " + reply.Status.ToString());
                    isPingable = reply.Status == IPStatus.Success;
                }
                catch (PingException ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                if (isPingable)
                {
                    trayIcon.Text = "Server is up and running.";
                    trayIcon.Icon = Resources.AppIconUp;
                    isNotified = false;
                }
                else
                {
                    trayIcon.Icon = Resources.AppIconDown;
                    trayIcon.Text = "No response - server down!";
                    if (!isNotified)
                    {
                        trayIcon.BalloonTipTitle = "Server down!";
                        trayIcon.BalloonTipText = "No response was received from the server!";
                        isNotified = !isNotified;
                    }
                }
            }
        }
    }
}
