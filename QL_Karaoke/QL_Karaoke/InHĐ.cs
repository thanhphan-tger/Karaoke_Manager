using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QL_Karaoke
{
    public partial class Form_InHĐ : Form
    {
        public Form_InHĐ()
        {
            InitializeComponent();
        }

        private void InHĐ_Load(object sender, EventArgs e)
        {
            CrystalReport2 rpt1 = new CrystalReport2();
            crystalReportViewer1.ReportSource = rpt1;
            crystalReportViewer1.Refresh();
            crystalReportViewer1.DisplayToolbar = false;
            crystalReportViewer1.DisplayStatusBar = false;
        }
    }
}
