using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VCInstaller
{
    public partial class frm_installer : Form
    {
        string[] installer_files_full_path;
        string[] installer_files;
        string[] selected_files;

        string arguments2005;
        string arguments2008;
        string arguments2010;
        string arguments2012;
        string arguments2013;
        string arguments2015;

        string log;
        string info;
        public frm_installer()
        {
            InitializeComponent();
        }

        private void frm_installer_Load(object sender, EventArgs e)
        {
            if ( WindowsIdentity.GetCurrent().Owner == WindowsIdentity.GetCurrent().User )   // Check for Admin privileges   
            {
                try
                {
                    this.Visible = false;
                    ProcessStartInfo info = new ProcessStartInfo(Application.ExecutablePath); // my own .exe
                    info.UseShellExecute = true;
                    info.Verb = "runas";   // invoke UAC prompt
                    Process.Start(info);
                }
                catch ( Win32Exception ex )
                {
                    if ( ex.NativeErrorCode == 1223 ) //The operation was canceled by the user.
                    {
                        MessageBox.Show("Why did you not selected Yes?", "WHY?", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        Application.Exit();
                    }
                    else
                        throw new Exception("Something went wrong :-(");
                }
                Application.Exit();
            }
            else
            {
                //    MessageBox.Show("I have admin privileges :-)");
            }

            radio_select_all.Enabled = false;
            radio_select_none.Enabled = false;
            btn_install.Enabled = false;

            arguments2005 = "/q";
            arguments2008 = "/qb";
            arguments2010 = "/passive /norestart";
            arguments2012 = "/passive /norestart";
            arguments2013 = "/install /passive /norestart";
            arguments2015 = "/install /passive /norestart";            

            tabControl1.SelectedIndex = 0;           
            notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
            notifyIcon1.BalloonTipTitle = "VC INSTALLER"; 
            notifyIcon1.Icon = this.Icon;

            log = string.Empty;

            info = string.Empty;

            info += "VCINstaller v" + Application.ProductVersion + Environment.NewLine;
            info += "How to use this tool :" + Environment.NewLine;
            info += "1 . Copy all your vcredist packages into a folder, and copy this tool too." + Environment.NewLine;
            info += "2 . Click on the 'Show Files' Button and select your packages" + Environment.NewLine;
            info += "3 . Once you are sure of your selection click the 'Start Install' button" + Environment.NewLine;
            info += "4 . Wait for the packages to finish, a notifycation will be seen when the process is done" + Environment.NewLine;
            info += " " + Environment.NewLine;
            info += "This tool works better if your vcredist are named in a more readable manner such as :" + Environment.NewLine;
            info += "vcredist-2010-x64.exe, vcredist_x86-2008.exe, etc" + Environment.NewLine;
            info += "Just make sure the name vcredist +<year>+<type> is on the filenaame." + Environment.NewLine;
            info += "Tool developer : C. A. Herrera" + Environment.NewLine;

            txt_info.Text = info;

        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btn_search_show_Click(object sender, EventArgs e)
        {

            installer_files_full_path = Directory.GetFiles(Environment.CurrentDirectory, "*.exe", SearchOption.TopDirectoryOnly).Except(Directory.GetFiles(Environment.CurrentDirectory, Application.ProductName + "*" + ".exe") ).ToArray() ;
            installer_files = new string[installer_files_full_path.Length];

            lbl_details.Text = string.Empty;

            chk_file_list.Items.Clear();            

            for ( int i = 0 ; i < installer_files_full_path.Length ; i++ )
            {
                installer_files[i] = Path.GetFileName(installer_files_full_path[i]);
            }

            for (int i = 0 ; i < installer_files_full_path.Length ; i++ )
            {
                chk_file_list.Items.Add(installer_files[i]);
            }

            lbl_details.Text += " " + installer_files_full_path.Length;

            if (chk_file_list.Items.Count > 0)
            {
                btn_install.Enabled = true;
                radio_select_all.Enabled = true;
                radio_select_none.Enabled = true;
            }
            else
            {
                btn_install.Enabled = false;
                radio_select_all.Enabled = false;
                radio_select_none.Enabled = false;

                notifyIcon1.Text = "Warning!";
                notifyIcon1.BalloonTipText = " No Executable files were found!. Check if the tool and the files are in the same folder";
                notifyIcon1.ShowBalloonTip(1000);
            }
   
        }

        private void radio_select_all_CheckedChanged(object sender, EventArgs e)
        {
            for ( int i = 0 ; i < chk_file_list.Items.Count; i++)
            {
                chk_file_list.SetItemCheckState(i,CheckState.Checked);
            }
        }

        private void radio_select_none_CheckedChanged(object sender, EventArgs e)
        {
            for ( int i = 0 ; i < chk_file_list.Items.Count ; i++ )
            {
                chk_file_list.SetItemCheckState(i, CheckState.Unchecked);
            }
        }

        private void btn_install_Click(object sender, EventArgs e)
        {
            int inner = 0;
            selected_files = new string[chk_file_list.Items.Count];

            for ( int i = 0 ; i < chk_file_list.Items.Count ; i++ )
            {
                if ( chk_file_list.GetItemChecked(i) == true )
                {
                    selected_files[inner++] = chk_file_list.Items[i].ToString();
                }
            }

            log += "Process started at " + DateTime.Now.TimeOfDay + Environment.NewLine;
            log += "System version reported is : " + Environment.OSVersion + Environment.NewLine;
            log += "Trying the installation of " + inner + " packages" + Environment.NewLine;


            for ( int i = 0 ; i < chk_file_list.Items.Count ; i++ )
            {

                if ( selected_files[i] == null )
                    break;

                if ( selected_files[i].Contains("2005") )
                {
                    Process vcinstaller = new Process();
                    vcinstaller.StartInfo.FileName = selected_files[i];
                    vcinstaller.StartInfo.Arguments = arguments2005;
                    vcinstaller.Start();
                    vcinstaller.WaitForExit();

                    log += "Installed " + selected_files[i] + " with argument " + arguments2005;
                }

                if ( selected_files[i].Contains("2008") )
                {
                    Process vcinstaller = new Process();
                    vcinstaller.StartInfo.FileName = selected_files[i];
                    vcinstaller.StartInfo.Arguments = arguments2008;
                    vcinstaller.Start();
                    vcinstaller.WaitForExit();

                    log += "Installed " + selected_files[i] + " with argument " + arguments2008;
                }

                if ( selected_files[i].Contains("2010") )
                {
                    Process vcinstaller = new Process();
                    vcinstaller.StartInfo.FileName = selected_files[i];
                    vcinstaller.StartInfo.Arguments = arguments2010;
                    vcinstaller.Start();
                    vcinstaller.WaitForExit();

                    log += "Installed " + selected_files[i] + " with argument " + arguments2010;
                }

                if ( selected_files[i].Contains("2012") )
                {
                    Process vcinstaller = new Process();
                    vcinstaller.StartInfo.FileName = selected_files[i];
                    vcinstaller.StartInfo.Arguments = arguments2012;
                    vcinstaller.Start();
                    vcinstaller.WaitForExit();

                    log += "Installed " + selected_files[i] + " with argument " + arguments2012;
                }

                if ( selected_files[i].Contains("2013") )
                {
                    Process vcinstaller = new Process();
                    vcinstaller.StartInfo.FileName = selected_files[i];
                    vcinstaller.StartInfo.Arguments = arguments2013;
                    vcinstaller.Start();
                    vcinstaller.WaitForExit();

                    log += "Installed " + selected_files[i] + " with argument " + arguments2013;
                }

                if ( selected_files[i].Contains("2015") )
                {
                    Process vcinstaller = new Process();
                    vcinstaller.StartInfo.FileName = selected_files[i];
                    vcinstaller.StartInfo.Arguments = arguments2015;
                    vcinstaller.Start();
                    vcinstaller.WaitForExit();

                    log += "Installed " + selected_files[i] + " with argument " + arguments2015;
                }

            }

            File.WriteAllText("log_" + DateTime.Today.Ticks + ".txt", log);

            notifyIcon1.Text = "Process is finished";
            notifyIcon1.BalloonTipText = "Process is done, check in Control Panel or Setting for all the installed packages";
            notifyIcon1.ShowBalloonTip(1000);
                
        }

        private void notifyIcon1_MouseDown(object sender, MouseEventArgs e)
        {
            notifyIcon1.Visible = false;
        }
    }
}
