namespace StriveOverlay
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.chkWallP1 = new System.Windows.Forms.CheckBox();
            this.chkWallP2 = new System.Windows.Forms.CheckBox();
            this.chkDebug = new System.Windows.Forms.CheckBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // chkWallP1
            // 
            this.chkWallP1.AutoSize = true;
            this.chkWallP1.Checked = true;
            this.chkWallP1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkWallP1.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkWallP1.Location = new System.Drawing.Point(124, 153);
            this.chkWallP1.Name = "chkWallP1";
            this.chkWallP1.Size = new System.Drawing.Size(171, 20);
            this.chkWallP1.TabIndex = 0;
            this.chkWallP1.Text = "Wallbreak Meter P1";
            this.chkWallP1.UseVisualStyleBackColor = true;
            // 
            // chkWallP2
            // 
            this.chkWallP2.AutoSize = true;
            this.chkWallP2.Checked = true;
            this.chkWallP2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkWallP2.Font = new System.Drawing.Font("Courier New", 9.75F);
            this.chkWallP2.Location = new System.Drawing.Point(124, 179);
            this.chkWallP2.Name = "chkWallP2";
            this.chkWallP2.Size = new System.Drawing.Size(171, 20);
            this.chkWallP2.TabIndex = 1;
            this.chkWallP2.Text = "Wallbreak Meter P2";
            this.chkWallP2.UseVisualStyleBackColor = true;
            // 
            // chkDebug
            // 
            this.chkDebug.AutoSize = true;
            this.chkDebug.Font = new System.Drawing.Font("Courier New", 9.75F);
            this.chkDebug.Location = new System.Drawing.Point(124, 205);
            this.chkDebug.Name = "chkDebug";
            this.chkDebug.Size = new System.Drawing.Size(67, 20);
            this.chkDebug.TabIndex = 2;
            this.chkDebug.Text = "Debug";
            this.chkDebug.UseVisualStyleBackColor = true;
            // 
            // linkLabel1
            // 
            this.linkLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel1.Location = new System.Drawing.Point(2, 269);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(308, 15);
            this.linkLabel1.TabIndex = 3;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "https://github.com/carekovisk/StriveOverlay";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(417, 290);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.chkDebug);
            this.Controls.Add(this.chkWallP2);
            this.Controls.Add(this.chkWallP1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "StriveOverlay";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkWallP1;
        private System.Windows.Forms.CheckBox chkWallP2;
        private System.Windows.Forms.CheckBox chkDebug;
        private System.Windows.Forms.LinkLabel linkLabel1;
    }
}

