using ServerStatusMonitor.Properties;
using System;
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
            ConfigForm configForm = new ConfigForm();                   // window that pops up when clicking config in the tray
            System.Timers.Timer timer = new System.Timers.Timer();      // the timer
            Uri uri;                                                    // uri object used to convert full paths to host names
            Ping ping = new Ping();                                     // ping object used to send and receive messages from the host
            bool isPingable = false;                                    // determine if the server can be pinged (if a response is received)
            bool isMonitoring = false;                                  // determine if the server is currently being monitored
            bool isNotified = false;                                    // set to true on first failure so consecutive failures don't spam the user
            string siteToMonitor;                                       // hostname/URL of server being monitored
            int interval;                                               // time between each status check
            private NotifyIcon trayIcon;                                // icon that appears in the Windows tray

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
                    // while monitoring, load the settings from app.config
                    // set the timer interval based on a user-defined value (max 7 days)
                    case true:
                        LoadSettings();
                        timer.Interval = interval > 0 ? interval * 1000 * 60 : 5 * 1000 * 60;
                        timer.Start();
                        PingURL();
                        trayIcon.ContextMenu.MenuItems[0].Enabled = false;
                        trayIcon.ContextMenu.MenuItems[1].Text = "Stop";
                        break;
                    // when monitoring is toggled to false, stop the timer
                    // reset any variables used to track status
                    case false:
                        timer.Stop();
                        isPingable = false;
                        isNotified = false;
                        trayIcon.ContextMenu.MenuItems[0].Enabled = true;
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
                uri = new Uri(siteToMonitor);                       // convert user-input value to pingable hostname
                try
                {
                    PingReply reply = ping.Send(uri.Host);          // ping the host
                    isPingable = reply.Status == IPStatus.Success;  // check if the ping was successful, update isPingable
                }
                catch (PingException ex)
                {
                    Console.WriteLine(ex.ToString());               // log any exceptions to the console.  exceptions may be caused by invalid host names
                }
                if (isPingable)
                {
                    trayIcon.Text = "Server is up and running.";    // on a successful ping, set the hover text to indicate server is running as expected
                    trayIcon.Icon = Resources.AppIconUp;            // update the tray icon to show a green dot
                    isNotified = false;                             // if an error was presented before, clear it so a new one may be used to alert the user
                }
                else
                {
                    trayIcon.Text = "No response - server down!";   // on a failed ping, set the hover text to indicate teh server is down
                    trayIcon.Icon = Resources.AppIconDown;          // update the tray icon to show a red dot
                    if (!isNotified)                                // if the user wasn't notified, display a notification alerting them of a failure
                    {
                        trayIcon.BalloonTipTitle = "Server down!";
                        trayIcon.BalloonTipText = "No response was received from\n" + siteToMonitor;
                        trayIcon.ShowBalloonTip(15000);
                        isNotified = !isNotified;
                    }
                }
            }
        }
    }
}
