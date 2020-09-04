namespace WorkshopAssistant
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
            this.pnl_Header = new System.Windows.Forms.Panel();
            this.UserName_label = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pnl_Sub = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_Site = new System.Windows.Forms.Button();
            this.btn_Leave = new System.Windows.Forms.Button();
            this.btn_Sick = new System.Windows.Forms.Button();
            this.btn_Exit = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.pnl_WorkList = new System.Windows.Forms.Panel();
            this.pnl_Header.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.pnl_Sub.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl_Header
            // 
            this.pnl_Header.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnl_Header.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(155)))), ((int)(((byte)(66)))));
            this.pnl_Header.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnl_Header.Controls.Add(this.UserName_label);
            this.pnl_Header.Controls.Add(this.pictureBox1);
            this.pnl_Header.Location = new System.Drawing.Point(0, 0);
            this.pnl_Header.Name = "pnl_Header";
            this.pnl_Header.Size = new System.Drawing.Size(1921, 177);
            this.pnl_Header.TabIndex = 0;
            // 
            // UserName_label
            // 
            this.UserName_label.AutoSize = true;
            this.UserName_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UserName_label.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(221)))), ((int)(((byte)(0)))));
            this.UserName_label.Location = new System.Drawing.Point(215, 65);
            this.UserName_label.Name = "UserName_label";
            this.UserName_label.Size = new System.Drawing.Size(207, 46);
            this.UserName_label.TabIndex = 6;
            this.UserName_label.Text = "Welcome ";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::WorkshopAssistant.Properties.Resources.MMD_Logo;
            this.pictureBox1.Location = new System.Drawing.Point(1, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(175, 175);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // pnl_Sub
            // 
            this.pnl_Sub.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnl_Sub.BackColor = System.Drawing.Color.White;
            this.pnl_Sub.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnl_Sub.Controls.Add(this.label1);
            this.pnl_Sub.Controls.Add(this.btn_Site);
            this.pnl_Sub.Controls.Add(this.btn_Leave);
            this.pnl_Sub.Controls.Add(this.btn_Sick);
            this.pnl_Sub.Location = new System.Drawing.Point(-1, 174);
            this.pnl_Sub.Name = "pnl_Sub";
            this.pnl_Sub.Size = new System.Drawing.Size(1922, 88);
            this.pnl_Sub.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(155)))), ((int)(((byte)(66)))));
            this.label1.Location = new System.Drawing.Point(45, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(196, 46);
            this.label1.TabIndex = 5;
            this.label1.Text = "Work List";
            // 
            // btn_Site
            // 
            this.btn_Site.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Site.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(155)))), ((int)(((byte)(66)))));
            this.btn_Site.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Site.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Site.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(221)))), ((int)(((byte)(0)))));
            this.btn_Site.Location = new System.Drawing.Point(1369, 17);
            this.btn_Site.Name = "btn_Site";
            this.btn_Site.Size = new System.Drawing.Size(169, 51);
            this.btn_Site.TabIndex = 4;
            this.btn_Site.Text = "On Site";
            this.btn_Site.UseVisualStyleBackColor = false;
            this.btn_Site.Click += new System.EventHandler(this.btn_Site_Click);
            // 
            // btn_Leave
            // 
            this.btn_Leave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Leave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(155)))), ((int)(((byte)(66)))));
            this.btn_Leave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Leave.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Leave.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(221)))), ((int)(((byte)(0)))));
            this.btn_Leave.Location = new System.Drawing.Point(1544, 17);
            this.btn_Leave.Name = "btn_Leave";
            this.btn_Leave.Size = new System.Drawing.Size(218, 51);
            this.btn_Leave.TabIndex = 3;
            this.btn_Leave.Text = "On Leave";
            this.btn_Leave.UseVisualStyleBackColor = false;
            this.btn_Leave.Click += new System.EventHandler(this.btn_Leave_Click);
            // 
            // btn_Sick
            // 
            this.btn_Sick.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Sick.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(155)))), ((int)(((byte)(66)))));
            this.btn_Sick.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Sick.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Sick.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(221)))), ((int)(((byte)(0)))));
            this.btn_Sick.Location = new System.Drawing.Point(1768, 17);
            this.btn_Sick.Name = "btn_Sick";
            this.btn_Sick.Size = new System.Drawing.Size(140, 51);
            this.btn_Sick.TabIndex = 2;
            this.btn_Sick.Text = "Sick";
            this.btn_Sick.UseVisualStyleBackColor = false;
            this.btn_Sick.Click += new System.EventHandler(this.btn_Sick_Click);
            // 
            // btn_Exit
            // 
            this.btn_Exit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Exit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(155)))), ((int)(((byte)(66)))));
            this.btn_Exit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Exit.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Exit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(221)))), ((int)(((byte)(0)))));
            this.btn_Exit.Location = new System.Drawing.Point(1781, 1012);
            this.btn_Exit.Name = "btn_Exit";
            this.btn_Exit.Size = new System.Drawing.Size(127, 56);
            this.btn_Exit.TabIndex = 1;
            this.btn_Exit.Text = "Exit";
            this.btn_Exit.UseVisualStyleBackColor = false;
            this.btn_Exit.Click += new System.EventHandler(this.btn_Exit_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(155)))), ((int)(((byte)(66)))));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(221)))), ((int)(((byte)(0)))));
            this.button1.Location = new System.Drawing.Point(1603, 1012);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(172, 56);
            this.button1.TabIndex = 3;
            this.button1.Text = "Logout";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.btn_Logout_Click);
            // 
            // pnl_WorkList
            // 
            this.pnl_WorkList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.pnl_WorkList.AutoScroll = true;
            this.pnl_WorkList.BackColor = System.Drawing.Color.White;
            this.pnl_WorkList.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pnl_WorkList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnl_WorkList.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(221)))), ((int)(((byte)(0)))));
            this.pnl_WorkList.Location = new System.Drawing.Point(0, 259);
            this.pnl_WorkList.Name = "pnl_WorkList";
            this.pnl_WorkList.Size = new System.Drawing.Size(309, 826);
            this.pnl_WorkList.TabIndex = 1;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(1920, 1080);
            this.ControlBox = false;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btn_Exit);
            this.Controls.Add(this.pnl_Sub);
            this.Controls.Add(this.pnl_WorkList);
            this.Controls.Add(this.pnl_Header);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.ImeMode = System.Windows.Forms.ImeMode.On;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.Text = "MainForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.pnl_Header.ResumeLayout(false);
            this.pnl_Header.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.pnl_Sub.ResumeLayout(false);
            this.pnl_Sub.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnl_Header;
        private System.Windows.Forms.Panel pnl_Sub;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btn_Exit;
        private System.Windows.Forms.Button btn_Site;
        private System.Windows.Forms.Button btn_Leave;
        private System.Windows.Forms.Button btn_Sick;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label UserName_label;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel pnl_WorkList;
    }
}