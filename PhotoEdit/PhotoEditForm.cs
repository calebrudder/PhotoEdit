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
    public partial class PhotoEditForm : Form
    {
        public PhotoEditForm(String filePath)
        {
            var selectedPhoto = new Bitmap(Image.FromFile(filePath));
            InitializeComponent();
            imageView.BackgroundImage = selectedPhoto;
        }

        private void Button1_Click(object sender, EventArgs e)
        {

        }

        async private void BrightnessBar_Scroll(object sender, EventArgs e)
        {
            await ChangeImageBrightness(brightnessBar.Value);
        }

        private void InvertButton_Click(object sender, EventArgs e)
        {

        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            
        }

        async Task ChangeImageBrightness(int value)
        {
            await Task.Run(() =>
            {


            });
        }
    }
}
