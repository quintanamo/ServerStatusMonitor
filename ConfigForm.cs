using System;
using System.Configuration;
using System.Windows.Forms;

namespace ServerStatusMonitor
{
    public partial class ConfigForm : Form
    {
        public ConfigForm()
        {
            int intervalValue;
            InitializeComponent();
            if (!string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["SiteToMonitor"]))
            {
                urlToMonitor.Text = ConfigurationManager.AppSettings["SiteToMonitor"];
            }
            int.TryParse(ConfigurationManager.AppSettings["Interval"], out intervalValue);
            if (intervalValue > 0)
            {
                interval.Value = intervalValue;
            }
        }

        private void saveConfigButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(urlToMonitor.Text.Trim()))
            {
                ConfigurationManager.AppSettings["SiteToMonitor"] = urlToMonitor.Text.Trim();
            }
            if (interval.Value > 0)
            {
                ConfigurationManager.AppSettings["Interval"] = interval.Value.ToString();
            }
            this.Close();
        }
    }
}
