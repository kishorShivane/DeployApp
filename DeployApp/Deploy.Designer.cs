namespace DeployApp
{
    partial class Deploy
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
            this.btnGenDutyDbf = new System.Windows.Forms.Button();
            this.grpSmartCard = new System.Windows.Forms.GroupBox();
            this.btnHotListFile = new System.Windows.Forms.Button();
            this.txtDecimal = new System.Windows.Forms.TextBox();
            this.Enter = new System.Windows.Forms.Label();
            this.btnWCM = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblSmartCard = new System.Windows.Forms.Label();
            this.grpSmartCard.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnGenDutyDbf
            // 
            this.btnGenDutyDbf.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnGenDutyDbf.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenDutyDbf.Location = new System.Drawing.Point(314, 37);
            this.btnGenDutyDbf.Name = "btnGenDutyDbf";
            this.btnGenDutyDbf.Size = new System.Drawing.Size(187, 37);
            this.btnGenDutyDbf.TabIndex = 1;
            this.btnGenDutyDbf.Text = "Generate Duty File";
            this.btnGenDutyDbf.UseVisualStyleBackColor = false;
            this.btnGenDutyDbf.Click += new System.EventHandler(this.btnGenDutyDbf_Click);
            // 
            // grpSmartCard
            // 
            this.grpSmartCard.Controls.Add(this.lblSmartCard);
            this.grpSmartCard.Controls.Add(this.btnHotListFile);
            this.grpSmartCard.Controls.Add(this.txtDecimal);
            this.grpSmartCard.Controls.Add(this.Enter);
            this.grpSmartCard.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpSmartCard.Location = new System.Drawing.Point(33, 173);
            this.grpSmartCard.Name = "grpSmartCard";
            this.grpSmartCard.Size = new System.Drawing.Size(738, 164);
            this.grpSmartCard.TabIndex = 2;
            this.grpSmartCard.TabStop = false;
            this.grpSmartCard.Text = "Smart Card Hotlist";
            // 
            // btnHotListFile
            // 
            this.btnHotListFile.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnHotListFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHotListFile.Location = new System.Drawing.Point(487, 61);
            this.btnHotListFile.Name = "btnHotListFile";
            this.btnHotListFile.Size = new System.Drawing.Size(187, 34);
            this.btnHotListFile.TabIndex = 2;
            this.btnHotListFile.Text = "Generate Hotlist File";
            this.btnHotListFile.UseVisualStyleBackColor = false;
            this.btnHotListFile.Click += new System.EventHandler(this.btnHotListFile_Click);
            // 
            // txtDecimal
            // 
            this.txtDecimal.Location = new System.Drawing.Point(226, 68);
            this.txtDecimal.Name = "txtDecimal";
            this.txtDecimal.Size = new System.Drawing.Size(201, 24);
            this.txtDecimal.TabIndex = 1;
            // 
            // Enter
            // 
            this.Enter.AutoSize = true;
            this.Enter.Location = new System.Drawing.Point(17, 68);
            this.Enter.Name = "Enter";
            this.Enter.Size = new System.Drawing.Size(202, 18);
            this.Enter.TabIndex = 0;
            this.Enter.Text = "Enter 10 Digit Serial Number: ";
            // 
            // btnWCM
            // 
            this.btnWCM.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnWCM.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnWCM.Location = new System.Drawing.Point(337, 383);
            this.btnWCM.Name = "btnWCM";
            this.btnWCM.Size = new System.Drawing.Size(136, 33);
            this.btnWCM.TabIndex = 3;
            this.btnWCM.Text = "WCM";
            this.btnWCM.UseVisualStyleBackColor = false;
            this.btnWCM.Click += new System.EventHandler(this.btnWCM_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(140, 109);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 15);
            this.lblStatus.TabIndex = 4;
            // 
            // lblSmartCard
            // 
            this.lblSmartCard.AutoSize = true;
            this.lblSmartCard.Location = new System.Drawing.Point(119, 120);
            this.lblSmartCard.Name = "lblSmartCard";
            this.lblSmartCard.Size = new System.Drawing.Size(0, 18);
            this.lblSmartCard.TabIndex = 3;
            // 
            // Deploy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnWCM);
            this.Controls.Add(this.grpSmartCard);
            this.Controls.Add(this.btnGenDutyDbf);
            this.Name = "Deploy";
            this.Text = "Deploy";
            this.grpSmartCard.ResumeLayout(false);
            this.grpSmartCard.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnGenDutyDbf;
        private System.Windows.Forms.GroupBox grpSmartCard;
        private System.Windows.Forms.Label Enter;
        private System.Windows.Forms.Button btnWCM;
        private System.Windows.Forms.Button btnHotListFile;
        private System.Windows.Forms.TextBox txtDecimal;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblSmartCard;
    }
}

