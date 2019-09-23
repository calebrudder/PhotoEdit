using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhotoEdit
{
    public partial class progressForm : Form
    {
        public delegate void cancelTask();
        public event cancelTask cancel;
        public progressForm()
        {
            InitializeComponent();
        }
        public int ProgressBarValue
        {
            set { this.transformationProgress.Value = value; }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            cancel?.Invoke();
        }
    }
}
