using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace WindowsFormsApplication1
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();

            Assembly currentAsm = Assembly.GetExecutingAssembly();
            object[] o = currentAsm.GetCustomAttributes(typeof(AssemblyVersionAttribute), false);

            lblTitle.Text = ((AssemblyTitleAttribute)
                ((currentAsm.GetCustomAttributes(typeof(AssemblyTitleAttribute), false))[0])).Title;
            tbDescription.Text = ((AssemblyDescriptionAttribute)
                (currentAsm.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false))[0]).Description;
            lblCopyright.Text = string.Format("{0} {1}",
                ((AssemblyCopyrightAttribute)
                (currentAsm.GetCustomAttributes(typeof(AssemblyCopyrightAttribute),false))[0]).Copyright,
                ((AssemblyCompanyAttribute)
                (currentAsm.GetCustomAttributes(typeof(AssemblyCompanyAttribute),false))[0]).Company);
            if (o != null && o.Length > 0)
                lblVersion.Text = string.Format("version {0}", ((AssemblyVersionAttribute)(o[0])).Version);
            else
                lblVersion.Text = string.Format("version {0}", currentAsm.GetName().Version.ToString());

            lblTitle.Left = (this.Width - lblTitle.Width) / 2;
            lblVersion.Left = (this.Width - lblVersion.Width) / 2;
        }
    }
}
