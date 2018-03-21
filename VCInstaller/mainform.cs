using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VCInstaller
{
    enum ArgumentsbyVCYear
    {
        args2005, args2008, args_2010, args2012, args2013, args_2015
    };



    public partial class frm_installer : Form
    {
        string[] installer_files_full_path;
        string[] installer_files;
        string[] selected_files;

        string[] installer_args_by_pkg;
        SimpleLogger log; // Will create a fresh new log file
        string info;

        /// <summary>
        /// Construct 
        /// </summary>
        public frm_installer()
        {
            InitializeComponent();
        }

        private void frm_installer_Load(object sender, EventArgs e)
        {
            log = new SimpleLogger();
            log.Info("Application : " + this.ProductName );            
            log.Info("System version reported is : " + Environment.OSVersion);
            log.Info("System is 64 bits ? :" + Environment.Is64BitOperatingSystem);
            log.Info("Current process is 64 bit? : " + Environment.Is64BitProcess);
            log.Info("Current program startup folder is : " + Environment.CurrentDirectory);

            log.Trace("Verifiy if tool can run in Admin mode ...");
            if (WindowsIdentity.GetCurrent().Owner == WindowsIdentity.GetCurrent().User)   // Check for Admin privileges   
            {
                try
                {
                    this.Visible = false;
                    ProcessStartInfo info = new ProcessStartInfo(Application.ExecutablePath); // my own .exe
                    info.UseShellExecute = true;
                    info.Verb = "runas";   // invoke UAC prompt
                    Process.Start(info);
                }
                catch (Win32Exception ex)
                {
                    if (ex.NativeErrorCode == 1223) //The operation was canceled by the user.
                    {
                        MessageBox.Show("Why did you not selected Yes?", "WHY?", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        log.Error("Application was not able to run in admin mode: Load event");
                        Application.Exit();
                    }
                    else
                    {
                        log.Warning("The user cancelled the admin elevation or another unexpected error.");
                        throw new Exception("Something went wrong :-(");
                    }

                }
                log.Trace("Application bail out!");
                Application.Exit();
            }
            else
            {
                //MessageBox.Show("I have admin privileges :-)");
                log.Info("I have admin privileges :-)");
            }

            radio_select_all.Enabled = false;
            radio_select_none.Enabled = false;
            btn_install.Enabled = false;

            installer_args_by_pkg = new string[] 
            {
                "/q",
                "/qb",
                "/passive /norestart",
                "/passive /norestart",
                "/install /passive /norestart",
                "/install /passive /norestart"
            };        

            tabControl1.SelectedIndex = 0;           
            notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
            notifyIcon1.BalloonTipTitle = "VC INSTALLER"; 
            notifyIcon1.Icon = this.Icon;

            info = string.Empty;

            log.Trace("Application var init reached.");

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
            log.Trace("Application reached end of load event");
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            log.Trace("Application Exit!");
            log.Report("Errors    :" + log.TotalErrors);
            log.Report("Warnings  :" + log.TotalWarnings);
            log.Report("Failures  :" + log.TotalFatalErrors);

            Application.Exit();
        }

        private void btn_search_show_Click(object sender, EventArgs e)
        {
            log.Info("Searching for files");
            installer_files_full_path = Directory.GetFiles(Environment.CurrentDirectory, "*.exe", SearchOption.TopDirectoryOnly).Except(Directory.GetFiles(Environment.CurrentDirectory, Application.ProductName + "*" + ".exe") ).ToArray() ;
            installer_files = new string[installer_files_full_path.Length];

            if (installer_files_full_path.Length == 0)
            {
                log.Warning("There are no executable files on this folder. Control : " + this.btn_search_show.ToString());                
                notifyIcon1.Text = "Warning!";
                notifyIcon1.BalloonTipText = " No Executable files were found!. Check if the tool and the files are in the same folder";
                notifyIcon1.ShowBalloonTip(1000);
            }
            else
            {
                lbl_details.Text = string.Empty;
                chk_file_list.Items.Clear();

                for (int i = 0; i < installer_files_full_path.Length; i++)
                {
                    installer_files[i] = Path.GetFileName(installer_files_full_path[i]);
                    log.Trace("Found files : " + installer_files[i]);
                }

                for (int i = 0; i < installer_files_full_path.Length; i++)
                {
                    chk_file_list.Items.Add(installer_files[i]);
                    log.Trace("Adding to checked list :" + installer_files[i]);
                }

                if (chk_file_list.Items.Count > 0)
                {
                    btn_install.Enabled = true;
                    radio_select_all.Enabled = true;
                    radio_select_none.Enabled = true;
                    log.Trace("Check files is " + chk_file_list.Items.Count);
                    log.Trace("Button is enabled: " + this.btn_install.ToString());
                }
                else
                {
                    btn_install.Enabled = false;
                    radio_select_all.Enabled = false;
                    radio_select_none.Enabled = false;
                    log.Trace("Button is disabled: " + this.btn_install.ToString());
                }

                lbl_details.Text += " " + installer_files_full_path.Length;
            }
        }


        private void btn_install_Click(object sender, EventArgs e)
        {
            int inner = 0;
            int selected = 0;
            string exeargs = string.Empty;
            string infolog = string.Empty;
            string exename = string.Empty;
            Process vcinstallers = new Process();            

            selected_files = new string[chk_file_list.Items.Count];

            log.Trace("Starting the install process :");
            for ( int i = 0 ; i < chk_file_list.Items.Count ; i++ )
            {
                if ( chk_file_list.GetItemChecked(i) == true )
                {
                    selected_files[inner++] = chk_file_list.Items[i].ToString();
                    selected++;
                }
            }
            log.Info("Installing " + selected + " selected items");         
            log.Info("Intalling packages.");
                        
            for ( int i = 0 ; i < chk_file_list.Items.Count ; i++ )
            {

                if ( selected_files[i] == null )
                    break;

                if ( selected_files[i].Contains("2005") )
                {
                    exename = selected_files[i];
                    exeargs = installer_args_by_pkg[(int)ArgumentsbyVCYear.args2005];
                    infolog = "Package " + selected_files[i] + " with argument " + installer_args_by_pkg[(int)ArgumentsbyVCYear.args2005];
                }

                if ( selected_files[i].Contains("2008") )
                {
                    exename = selected_files[i];
                    exeargs = installer_args_by_pkg[(int)ArgumentsbyVCYear.args2008];
                    infolog = "Package " + selected_files[i] + " with argument " + installer_args_by_pkg[(int)ArgumentsbyVCYear.args2008];
                }

                if ( selected_files[i].Contains("2010") )
                {
                    exename = selected_files[i];
                    exeargs = installer_args_by_pkg[(int)ArgumentsbyVCYear.args_2010];
                    infolog = "Package " + selected_files[i] + " with argument " + installer_args_by_pkg[(int)ArgumentsbyVCYear.args_2010];
                }

                if ( selected_files[i].Contains("2012") )
                {
                    exename = selected_files[i];
                    exeargs = installer_args_by_pkg[(int)ArgumentsbyVCYear.args2012];
                    infolog = "Package " + selected_files[i] + " with argument " + installer_args_by_pkg[(int)ArgumentsbyVCYear.args2012];
                }

                if ( selected_files[i].Contains("2013") )
                {
                    exename = selected_files[i];
                    exeargs = installer_args_by_pkg[(int)ArgumentsbyVCYear.args2013];
                    infolog = "Package " + selected_files[i] + " with argument " + installer_args_by_pkg[(int)ArgumentsbyVCYear.args2013];
                }

                if ( selected_files[i].Contains("2015") )
                {
                    exename = selected_files[i];
                    exeargs = installer_args_by_pkg[(int)ArgumentsbyVCYear.args_2015];
                    infolog = "Package " + selected_files[i] + " with argument " + installer_args_by_pkg[(int)ArgumentsbyVCYear.args_2015];
                }

                bool executed = false;

                try
                {
                    vcinstallers.StartInfo.FileName = exename;
                    vcinstallers.StartInfo.Arguments = exeargs;
                    executed = vcinstallers.Start();
                    if (executed == false)
                        log.Error("Instaler didnt work on :" + exename + " with arguments : " + exeargs);
                    vcinstallers.WaitForExit();                    
                }
                catch (Exception excp)
                {
                    log.Error("An unexepected error ocurred on installing a package :" + excp.Message);
                    MessageBox.Show("An exception ocurred while installing packages " + excp.Message ,"Error on install",MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                log.Info(infolog);
            }

            log.Info("Installation process is done");
            log.Trace("Disposing of Process instance");
            vcinstallers.Dispose();
            notifyIcon1.Text = "Process is finished";
            notifyIcon1.BalloonTipText = "Process is done, check in Control Panel or Setting for all the installed packages";
            notifyIcon1.ShowBalloonTip(1000);
                
        }

        private void notifyIcon1_MouseDown(object sender, MouseEventArgs e)
        {
            notifyIcon1.Visible = false;
        }

        private void radio_select_all_Click(object sender, EventArgs e)
        {            
            for (int i = 0; i < chk_file_list.Items.Count; i++)
            {
                chk_file_list.SetItemCheckState(i, CheckState.Checked);
            }
            log.Trace("Selected files : " + chk_file_list.Items.Count);
            log.Trace("Event click :" + this.radio_select_all.ToString())
        }

        private void radio_select_none_Click(object sender, EventArgs e)
        {            
            for (int i = 0; i < chk_file_list.Items.Count; i++)
            {
                chk_file_list.SetItemCheckState(i, CheckState.Unchecked);
            }
            log.Trace("Unselected all files");
            log.Trace("Event Click :" + this.radio_select_none.ToString());
        }

        private void btn__view_log_Click(object sender, EventArgs e)
        {
            Process viewtextprog = new Process();
            try
            {
                if (File.Exists(Assembly.GetExecutingAssembly().GetName().Name + ".log"))
                {
                    log.Trace("Event log is being reviewed. Control : " + this.btn__view_log.ToString());
                    viewtextprog.StartInfo.FileName = "notepad";
                    viewtextprog.StartInfo.Arguments = Assembly.GetExecutingAssembly().GetName().Name + ".log";
                    viewtextprog.Start();
                }
                viewtextprog.Dispose();
            }
            catch(Exception excp)
            {
                log.Debug("Exception raised :" + excp.Message);
                log.Error("Error on disposing resources "  + this.btn__view_log.ToString());
            }
        }
    }
}
