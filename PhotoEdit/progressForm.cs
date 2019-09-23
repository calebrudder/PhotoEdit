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
    public partial class ProgressForm : Form
    {
        public delegate void cancelTask();
        public event cancelTask cancel;
        public ProgressForm()
        {
            InitializeComponent();
        }
        public int ProgressBarValue
        {
            get { return this.transformationProgress.Value; }
            set {
                if (this.transformationProgress.Value <= 95)
                {
                    this.transformationProgress.Value += 5;
                }
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            cancel?.Invoke();
        }
    }
}
