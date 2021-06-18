namespace ServerStatusMonitor
{
    partial class ConfigForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigForm));
            this.urlToMonitor = new System.Windows.Forms.TextBox();
            this.urlLabel = new System.Windows.Forms.Label();
            this.intervalLabel = new System.Windows.Forms.Label();
            this.interval = new System.Windows.Forms.NumericUpDown();
            this.saveConfigButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.interval)).BeginInit();
            this.SuspendLayout();
            // 
            // urlToMonitor
            // 
            this.urlToMonitor.Location = new System.Drawing.Point(15, 32);
            this.urlToMonitor.Name = "urlToMonitor";
            this.urlToMonitor.Size = new System.Drawing.Size(324, 20);
            this.urlToMonitor.TabIndex = 0;
            // 
            // urlLabel
            // 
            this.urlLabel.AutoSize = true;
            this.urlLabel.Location = new System.Drawing.Point(12, 16);
            this.urlLabel.Name = "urlLabel";
            this.urlLabel.Size = new System.Drawing.Size(81, 13);
            this.urlLabel.TabIndex = 1;
            this.urlLabel.Text = "URL to monitor:";
            // 
            // intervalLabel
            // 
            this.intervalLabel.AutoSize = true;
            this.intervalLabel.Location = new System.Drawing.Point(12, 67);
            this.intervalLabel.Name = "intervalLabel";
            this.intervalLabel.Size = new System.Drawing.Size(90, 13);
            this.intervalLabel.TabIndex = 2;
            this.intervalLabel.Text = "Interval (minutes):";
            // 
            // interval
            // 
            this.interval.Location = new System.Drawing.Point(15, 83);
            this.interval.Maximum = new decimal(new int[] {
            10080,
            0,
            0,
            0});
            this.interval.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.interval.Name = "interval";
            this.interval.Size = new System.Drawing.Size(87, 20);
            this.interval.TabIndex = 3;
            this.interval.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // saveConfigButton
            // 
            this.saveConfigButton.Location = new System.Drawing.Point(264, 80);
            this.saveConfigButton.Name = "saveConfigButton";
            this.saveConfigButton.Size = new System.Drawing.Size(75, 23);
            this.saveConfigButton.TabIndex = 4;
            this.saveConfigButton.Text = "Save";
            this.saveConfigButton.UseVisualStyleBackColor = true;
            this.saveConfigButton.Click += new System.EventHandler(this.saveConfigButton_Click);
            // 
            // ConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(351, 115);
            this.Controls.Add(this.saveConfigButton);
            this.Controls.Add(this.interval);
            this.Controls.Add(this.intervalLabel);
            this.Controls.Add(this.urlLabel);
            this.Controls.Add(this.urlToMonitor);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConfigForm";
            this.Text = "Configuration";
            ((System.ComponentModel.ISupportInitialize)(this.interval)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox urlToMonitor;
        private System.Windows.Forms.Label urlLabel;
        private System.Windows.Forms.Label intervalLabel;
        private System.Windows.Forms.NumericUpDown interval;
        private System.Windows.Forms.Button saveConfigButton;
    }
}

