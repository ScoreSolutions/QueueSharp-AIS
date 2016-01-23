using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Engine;

namespace CSITWSWindowService
{
    public partial class frmTestWindowsService : Form
    {
        public frmTestWindowsService()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CSITWSurveyENG eng = new CSITWSurveyENG();
            //if (eng.ReadAndSaveDataFromTextFile() > 0)
            //{
            //    eng.FilterData();
            //}

            eng.FilterData();
        }
    }
}
