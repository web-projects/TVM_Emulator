using TVMEmulator.forms;

namespace TVMEmulator
{
    partial class MainForm
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
            this.mainOutput = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.receiverPort = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.adaModeBtn = new System.Windows.Forms.Button();
            this.custidTB = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.passwordTB = new System.Windows.Forms.RichTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.sessionIdLbl = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.adaMessageTxt = new System.Windows.Forms.RichTextBox();
            this.ucTimings1 = new TVMEmulator.forms.ucTimings();
            this.label7 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // mainOutput
            // 
            this.mainOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mainOutput.Location = new System.Drawing.Point(12, 238);
            this.mainOutput.Name = "mainOutput";
            this.mainOutput.ReadOnly = true;
            this.mainOutput.Size = new System.Drawing.Size(655, 333);
            this.mainOutput.TabIndex = 0;
            this.mainOutput.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Receiver Port";
            // 
            // receiverPort
            // 
            this.receiverPort.Location = new System.Drawing.Point(120, 29);
            this.receiverPort.Name = "receiverPort";
            this.receiverPort.Size = new System.Drawing.Size(54, 22);
            this.receiverPort.TabIndex = 2;
            this.receiverPort.Text = "5112";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "CustID";
            // 
            // adaModeBtn
            // 
            this.adaModeBtn.Location = new System.Drawing.Point(120, 202);
            this.adaModeBtn.Name = "adaModeBtn";
            this.adaModeBtn.Size = new System.Drawing.Size(56, 22);
            this.adaModeBtn.TabIndex = 4;
            this.adaModeBtn.Text = "Start";
            this.adaModeBtn.UseVisualStyleBackColor = true;
            this.adaModeBtn.Click += new System.EventHandler(this.AdaModeExecute);
            // 
            // custidTB
            // 
            this.custidTB.Location = new System.Drawing.Point(120, 65);
            this.custidTB.Name = "custidTB";
            this.custidTB.Size = new System.Drawing.Size(54, 22);
            this.custidTB.TabIndex = 6;
            this.custidTB.Text = "1117600";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 102);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Password";
            // 
            // passwordTB
            // 
            this.passwordTB.Location = new System.Drawing.Point(120, 102);
            this.passwordTB.Name = "passwordTB";
            this.passwordTB.Size = new System.Drawing.Size(54, 22);
            this.passwordTB.TabIndex = 8;
            this.passwordTB.Text = "ipa1234";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(30, 207);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "ADA Mode";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(30, 142);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Session Id";
            // 
            // sessionIdLbl
            // 
            this.sessionIdLbl.AutoSize = true;
            this.sessionIdLbl.Location = new System.Drawing.Point(123, 142);
            this.sessionIdLbl.Name = "sessionIdLbl";
            this.sessionIdLbl.Size = new System.Drawing.Size(56, 13);
            this.sessionIdLbl.TabIndex = 10;
            this.sessionIdLbl.Text = "not-yet-set";
            this.sessionIdLbl.TextChanged += new System.EventHandler(this.OnSessionIdTextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(26, 173);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(87, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Display Message";
            // 
            // adaMessageTxt
            // 
            this.adaMessageTxt.Location = new System.Drawing.Point(120, 169);
            this.adaMessageTxt.Name = "adaMessageTxt";
            this.adaMessageTxt.Size = new System.Drawing.Size(547, 22);
            this.adaMessageTxt.TabIndex = 12;
            this.adaMessageTxt.Text = "SCAN CODE: ";
            // 
            // ucTimings1
            // 
            this.ucTimings1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ucTimings1.Location = new System.Drawing.Point(5, 349);
            this.ucTimings1.Name = "ucTimings1";
            this.ucTimings1.Size = new System.Drawing.Size(342, 59);
            this.ucTimings1.TabIndex = 14;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(551, 38);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(52, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "Clear Log";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(611, 33);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(56, 22);
            this.button1.TabIndex = 13;
            this.button1.Text = "Clear";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.ClearLog);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(682, 583);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.adaMessageTxt);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.sessionIdLbl);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.passwordTB);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.custidTB);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.adaModeBtn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.receiverPort);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.mainOutput);
            this.Name = "MainForm";
            this.Text = "TVM EMULATOR";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnFormClosing);
            this.Load += new System.EventHandler(this.OnFormLoad);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox mainOutput;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox receiverPort;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button adaModeBtn;
        private System.Windows.Forms.RichTextBox custidTB;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox passwordTB;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label sessionIdLbl;
        private ucTimings ucTimings1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RichTextBox adaMessageTxt;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button1;
    }
}

