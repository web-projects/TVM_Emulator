
namespace TVMEmulator.forms
{
    partial class ucTimings
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblMinResponseTime = new System.Windows.Forms.Label();
            this.lblMinResponseTimeValue = new System.Windows.Forms.Label();
            this.lblMaxResponseTime = new System.Windows.Forms.Label();
            this.lblAverageResponseTime = new System.Windows.Forms.Label();
            this.lblLastResponseTimeValue = new System.Windows.Forms.Label();
            this.lblLastResponseTime = new System.Windows.Forms.Label();
            this.lblMaxResponseTimeValue = new System.Windows.Forms.Label();
            this.lblAverageResponseTimeValue = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblMinResponseTime
            // 
            this.lblMinResponseTime.AutoSize = true;
            this.lblMinResponseTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMinResponseTime.Location = new System.Drawing.Point(3, 9);
            this.lblMinResponseTime.Name = "lblMinResponseTime";
            this.lblMinResponseTime.Size = new System.Drawing.Size(104, 13);
            this.lblMinResponseTime.TabIndex = 0;
            this.lblMinResponseTime.Text = "Min Response Time:";
            // 
            // lblMinResponseTimeValue
            // 
            this.lblMinResponseTimeValue.AutoSize = true;
            this.lblMinResponseTimeValue.Location = new System.Drawing.Point(134, 9);
            this.lblMinResponseTimeValue.Name = "lblMinResponseTimeValue";
            this.lblMinResponseTimeValue.Size = new System.Drawing.Size(16, 13);
            this.lblMinResponseTimeValue.TabIndex = 1;
            this.lblMinResponseTimeValue.Text = "...";
            // 
            // lblMaxResponseTime
            // 
            this.lblMaxResponseTime.AutoSize = true;
            this.lblMaxResponseTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaxResponseTime.Location = new System.Drawing.Point(3, 34);
            this.lblMaxResponseTime.Name = "lblMaxResponseTime";
            this.lblMaxResponseTime.Size = new System.Drawing.Size(107, 13);
            this.lblMaxResponseTime.TabIndex = 2;
            this.lblMaxResponseTime.Text = "Max Response Time:";
            // 
            // lblAverageResponseTime
            // 
            this.lblAverageResponseTime.AutoSize = true;
            this.lblAverageResponseTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAverageResponseTime.Location = new System.Drawing.Point(230, 9);
            this.lblAverageResponseTime.Name = "lblAverageResponseTime";
            this.lblAverageResponseTime.Size = new System.Drawing.Size(106, 13);
            this.lblAverageResponseTime.TabIndex = 3;
            this.lblAverageResponseTime.Text = "Avg Response Time:";
            // 
            // lblLastResponseTimeValue
            // 
            this.lblLastResponseTimeValue.AutoSize = true;
            this.lblLastResponseTimeValue.Location = new System.Drawing.Point(385, 34);
            this.lblLastResponseTimeValue.Name = "lblLastResponseTimeValue";
            this.lblLastResponseTimeValue.Size = new System.Drawing.Size(16, 13);
            this.lblLastResponseTimeValue.TabIndex = 4;
            this.lblLastResponseTimeValue.Text = "...";
            // 
            // lblLastResponseTime
            // 
            this.lblLastResponseTime.AutoSize = true;
            this.lblLastResponseTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLastResponseTime.Location = new System.Drawing.Point(230, 34);
            this.lblLastResponseTime.Name = "lblLastResponseTime";
            this.lblLastResponseTime.Size = new System.Drawing.Size(107, 13);
            this.lblLastResponseTime.TabIndex = 6;
            this.lblLastResponseTime.Text = "Last Response Time:";
            // 
            // lblMaxResponseTimeValue
            // 
            this.lblMaxResponseTimeValue.AutoSize = true;
            this.lblMaxResponseTimeValue.Location = new System.Drawing.Point(134, 34);
            this.lblMaxResponseTimeValue.Name = "lblMaxResponseTimeValue";
            this.lblMaxResponseTimeValue.Size = new System.Drawing.Size(16, 13);
            this.lblMaxResponseTimeValue.TabIndex = 7;
            this.lblMaxResponseTimeValue.Text = "...";
            // 
            // lblAverageResponseTimeValue
            // 
            this.lblAverageResponseTimeValue.AutoSize = true;
            this.lblAverageResponseTimeValue.Location = new System.Drawing.Point(385, 9);
            this.lblAverageResponseTimeValue.Name = "lblAverageResponseTimeValue";
            this.lblAverageResponseTimeValue.Size = new System.Drawing.Size(16, 13);
            this.lblAverageResponseTimeValue.TabIndex = 8;
            this.lblAverageResponseTimeValue.Text = "...";
            // 
            // ucTimings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblAverageResponseTimeValue);
            this.Controls.Add(this.lblMaxResponseTimeValue);
            this.Controls.Add(this.lblLastResponseTime);
            this.Controls.Add(this.lblLastResponseTimeValue);
            this.Controls.Add(this.lblAverageResponseTime);
            this.Controls.Add(this.lblMaxResponseTime);
            this.Controls.Add(this.lblMinResponseTimeValue);
            this.Controls.Add(this.lblMinResponseTime);
            this.Name = "ucTimings";
            this.Size = new System.Drawing.Size(503, 59);
            this.Load += new System.EventHandler(this.ucTimings_Load);
            this.RegionChanged += new System.EventHandler(this.ucTimings_RegionChanged);
            this.Resize += new System.EventHandler(this.ucTimings_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblMinResponseTime;
        private System.Windows.Forms.Label lblMinResponseTimeValue;
        private System.Windows.Forms.Label lblMaxResponseTime;
        private System.Windows.Forms.Label lblAverageResponseTime;
        private System.Windows.Forms.Label lblLastResponseTimeValue;
        private System.Windows.Forms.Label lblLastResponseTime;
        private System.Windows.Forms.Label lblMaxResponseTimeValue;
        private System.Windows.Forms.Label lblAverageResponseTimeValue;
    }
}
