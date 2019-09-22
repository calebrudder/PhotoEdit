using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhotoEdit
{
    public partial class PhotoEditForm : Form
    {
        public Bitmap selectedPhoto;
        public PhotoEditForm(String filePath)
        {
            selectedPhoto = new Bitmap(Image.FromFile(filePath));
            InitializeComponent();
            imageView.BackgroundImage = selectedPhoto;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        async private void BrightnessBar_Scroll(object sender, EventArgs e)
        {
            await ChangeImageBrightness(brightnessBar.Value);
        }

        async private void InvertButton_Click(object sender, EventArgs e)
        {
            await InvertColors();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            selectedPhoto.Save("", ImageFormat.Jpeg);
        }

        async Task ChangeImageBrightness(int value)
        {
            await Task.Run(() =>
            {


            });
        }

        async Task InvertColors()
        {
            await Task.Run(() =>
            {

                for (int y = 0; y < selectedPhoto.Height; y++)
                {
                    for (int x = 0; x < selectedPhoto.Width; x++)
                    {
                        Color color = selectedPhoto.GetPixel(x, y);
                        int newRed = Math.Abs(color.R - 255);
                        int newGreen = Math.Abs(color.G - 255);
                        int newBlue = Math.Abs(color.B - 255);
                        Color newColor = Color.FromArgb(newRed, newGreen, newBlue);
                        selectedPhoto.SetPixel(x, y, newColor);
                    }
                }
            });
        }

        private void ColorButton_Click(object sender, EventArgs e)
        {

        }
    }
}
