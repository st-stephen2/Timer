using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Security.Permissions;
using System.Text;
using System.Windows.Forms;


namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        TextWriter log = null;
        System.Windows.Forms.Timer t;
        //bool WriteToLog;
        string SoundFile;

        public Form1()
        {
            InitializeComponent();
            numericUpDownTime.Value = Properties.Settings.Default.time;
            cbTimeType.SelectedIndex = Properties.Settings.Default.timeType;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                if (Environment.OSVersion.Version.Major >= 6 && DwmApi.DwmIsCompositionEnabled())
                {
                    // Set the proper margins for the extended glass part
                    DwmApi.MARGINS margins = new DwmApi.MARGINS(-1, -1, -1, -1);
                    DwmApi.DwmExtendFrameIntoClientArea(this.Handle, margins);
                }
                else
                {
                    MessageBox.Show("This program or feature requires Windows Aero.");
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
             
        }
        decimal decTime;
        int[] ar = {36000, 600, 10};

        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnStart.Text == "Stop Timer")
                {
                    t.Stop();
                    progressBar1.Value = 0;
                    numericUpDownTime.ReadOnly = false;
                    btnStart.Text = "Start Timer";
                }
                else if (btnStart.Text == "Start Timer")
                {
                    decTime = numericUpDownTime.Value;
                    t = new System.Windows.Forms.Timer();
                    t.Interval = (int)decTime*ar[cbTimeType.SelectedIndex];
                    t.Tick += new EventHandler(t_Tick);
                    t.Start();
                    numericUpDownTime.ReadOnly = true;
                    btnStart.Text = "Stop Timer";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
        
        private void t_Tick(object sender, EventArgs e)
        {
            try
            {
                progressBar1.Value += 1;

                if (progressBar1.Value >= 100)
                {
                    t.Stop();
                    numericUpDownTime.ReadOnly = false;
                    if (SoundFile == "default")
                    {
                        try
                        {
                            SoundPlayer alert1 = new SoundPlayer(@"C:\Windows\Media\Robotz Critical Stop.wav");
                            SoundPlayer alert2 = new SoundPlayer(@"C:\Windows\Media\Robotz Default.wav");
                            SoundPlayer alert3 = new SoundPlayer(@"C:\Windows\Media\Robotz Error.wav");
                            alert1.PlaySync();
                            alert2.PlaySync();
                            alert3.PlaySync();
                        }
                        catch (Exception)
                        {
                            
                        }
                    }
                    else
                    {
                        try
                        {
                            SoundPlayer alert = new SoundPlayer(SoundFile);
                            alert.PlaySync();
                        }
                        catch (Exception)
                        {
                            
                        }
                    }
                    this.Activate();
                    MessageBox.Show("The allotted time has expired");
                    progressBar1.Value = 0;
                    btnStart.Text = "Start Timer";

                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Properties.Settings.Default.time = numericUpDownTime.Value;
                Properties.Settings.Default.timeType = cbTimeType.SelectedIndex;
                Properties.Settings.Default.Save();
            }
            finally
            {
                
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About a = new About();
            a.ShowDialog();
        }
    }
}

