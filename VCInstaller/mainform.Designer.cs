namespace VCInstaller
{
    partial class frm_installer
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
            if ( disposing && ( components != null ) )
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_installer));
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.chk_file_list = new System.Windows.Forms.CheckedListBox();
            this.btn_install = new System.Windows.Forms.Button();
            this.grp_options = new System.Windows.Forms.GroupBox();
            this.radio_select_none = new System.Windows.Forms.RadioButton();
            this.radio_select_all = new System.Windows.Forms.RadioButton();
            this.lbl_details = new System.Windows.Forms.Label();
            this.btn_search_show = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.txt_info = new System.Windows.Forms.TextBox();
            this.btn_exit = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.grp_options.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDown);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(1, 2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(498, 297);
            this.tabControl1.TabIndex = 5;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.chk_file_list);
            this.tabPage1.Controls.Add(this.btn_install);
            this.tabPage1.Controls.Add(this.grp_options);
            this.tabPage1.Controls.Add(this.lbl_details);
            this.tabPage1.Controls.Add(this.btn_search_show);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(490, 271);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Main";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // chk_file_list
            // 
            this.chk_file_list.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.chk_file_list.CheckOnClick = true;
            this.chk_file_list.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chk_file_list.Font = new System.Drawing.Font("Intel Clear", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_file_list.FormattingEnabled = true;
            this.chk_file_list.Location = new System.Drawing.Point(8, 6);
            this.chk_file_list.Name = "chk_file_list";
            this.chk_file_list.Size = new System.Drawing.Size(318, 206);
            this.chk_file_list.TabIndex = 10;
            // 
            // btn_install
            // 
            this.btn_install.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_install.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_install.Image = ((System.Drawing.Image)(resources.GetObject("btn_install.Image")));
            this.btn_install.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_install.Location = new System.Drawing.Point(198, 230);
            this.btn_install.Name = "btn_install";
            this.btn_install.Size = new System.Drawing.Size(128, 23);
            this.btn_install.TabIndex = 9;
            this.btn_install.Text = "Start Install";
            this.btn_install.UseVisualStyleBackColor = true;
            this.btn_install.Click += new System.EventHandler(this.btn_install_Click);
            // 
            // grp_options
            // 
            this.grp_options.Controls.Add(this.radio_select_none);
            this.grp_options.Controls.Add(this.radio_select_all);
            this.grp_options.Font = new System.Drawing.Font("Intel Clear", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grp_options.Location = new System.Drawing.Point(351, 82);
            this.grp_options.Name = "grp_options";
            this.grp_options.Size = new System.Drawing.Size(130, 90);
            this.grp_options.TabIndex = 8;
            this.grp_options.TabStop = false;
            this.grp_options.Text = "Options";
            // 
            // radio_select_none
            // 
            this.radio_select_none.AutoSize = true;
            this.radio_select_none.Location = new System.Drawing.Point(21, 56);
            this.radio_select_none.Name = "radio_select_none";
            this.radio_select_none.Size = new System.Drawing.Size(87, 23);
            this.radio_select_none.TabIndex = 1;
            this.radio_select_none.TabStop = true;
            this.radio_select_none.Text = "Select None";
            this.radio_select_none.UseVisualStyleBackColor = true;
            this.radio_select_none.Click += new System.EventHandler(this.radio_select_none_CheckedChanged);
            // 
            // radio_select_all
            // 
            this.radio_select_all.AutoSize = true;
            this.radio_select_all.Location = new System.Drawing.Point(21, 33);
            this.radio_select_all.Name = "radio_select_all";
            this.radio_select_all.Size = new System.Drawing.Size(74, 23);
            this.radio_select_all.TabIndex = 0;
            this.radio_select_all.TabStop = true;
            this.radio_select_all.Text = "Select All";
            this.radio_select_all.UseVisualStyleBackColor = true;
            this.radio_select_all.Click += new System.EventHandler(this.radio_select_all_CheckedChanged);
            // 
            // lbl_details
            // 
            this.lbl_details.AutoSize = true;
            this.lbl_details.Font = new System.Drawing.Font("Intel Clear", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_details.Location = new System.Drawing.Point(348, 54);
            this.lbl_details.Name = "lbl_details";
            this.lbl_details.Size = new System.Drawing.Size(45, 19);
            this.lbl_details.TabIndex = 6;
            this.lbl_details.Text = "Found :";
            // 
            // btn_search_show
            // 
            this.btn_search_show.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_search_show.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_search_show.Image = ((System.Drawing.Image)(resources.GetObject("btn_search_show.Image")));
            this.btn_search_show.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_search_show.Location = new System.Drawing.Point(351, 6);
            this.btn_search_show.Name = "btn_search_show";
            this.btn_search_show.Size = new System.Drawing.Size(130, 23);
            this.btn_search_show.TabIndex = 5;
            this.btn_search_show.Text = "Show Files";
            this.btn_search_show.UseVisualStyleBackColor = true;
            this.btn_search_show.Click += new System.EventHandler(this.btn_search_show_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.txt_info);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(490, 271);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "About";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // txt_info
            // 
            this.txt_info.BackColor = System.Drawing.Color.LightYellow;
            this.txt_info.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_info.Location = new System.Drawing.Point(61, 25);
            this.txt_info.Multiline = true;
            this.txt_info.Name = "txt_info";
            this.txt_info.ReadOnly = true;
            this.txt_info.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txt_info.Size = new System.Drawing.Size(423, 231);
            this.txt_info.TabIndex = 0;
            // 
            // btn_exit
            // 
            this.btn_exit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_exit.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_exit.Image = ((System.Drawing.Image)(resources.GetObject("btn_exit.Image")));
            this.btn_exit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_exit.Location = new System.Drawing.Point(369, 305);
            this.btn_exit.Name = "btn_exit";
            this.btn_exit.Size = new System.Drawing.Size(130, 23);
            this.btn_exit.TabIndex = 7;
            this.btn_exit.Text = "Exit";
            this.btn_exit.UseVisualStyleBackColor = true;
            this.btn_exit.Click += new System.EventHandler(this.btn_exit_Click);
            // 
            // frm_installer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(509, 341);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btn_exit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frm_installer";
            this.Text = "VC Installer";
            this.Load += new System.EventHandler(this.frm_installer_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.grp_options.ResumeLayout(false);
            this.grp_options.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.CheckedListBox chk_file_list;
        private System.Windows.Forms.Button btn_install;
        private System.Windows.Forms.Button btn_exit;
        private System.Windows.Forms.GroupBox grp_options;
        private System.Windows.Forms.RadioButton radio_select_none;
        private System.Windows.Forms.RadioButton radio_select_all;
        private System.Windows.Forms.Label lbl_details;
        private System.Windows.Forms.Button btn_search_show;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox txt_info;
    }
}

