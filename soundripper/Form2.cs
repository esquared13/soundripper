using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace soundripper
{
    public partial class frmDownloadProgress : Form
    {
        public frmDownloadProgress()
        {
            InitializeComponent();
        }

        public void updateProgressBar(int percentage) // updates progress bar value based on percentage complete calculated
        {
            prgbDownloadProgress.Value = percentage;
        }
    }
}
