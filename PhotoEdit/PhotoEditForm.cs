using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhotoEdit
{
    public partial class PhotoEditForm : Form
    {
        private Bitmap selectedPhoto;
        private Bitmap editedPhoto;
        private progressForm progress = new progressForm();
        private CancellationTokenSource cancellationTokenSource;
        public PhotoEditForm(String filePath)
        {
            selectedPhoto = new Bitmap(Image.FromFile(filePath));
            editedPhoto = new Bitmap(Image.FromFile(filePath));

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
            imageView.BackgroundImage = editedPhoto;
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
            cancellationTokenSource = new CancellationTokenSource();
            var token = cancellationTokenSource.Token;

            await Task.Run(() =>
            {

                int totalSize = (editedPhoto.Height) * (editedPhoto.Width);
                int totalProgress = 0;

                for (int y = 0; y < editedPhoto.Height; y++)
                {
                    for (int x = 0; x < editedPhoto.Width; x++)
                    {
                        if (token.IsCancellationRequested)
                        {
                            break;
                        }
                        else
                        {
                            totalProgress = (x * (y+1)) / totalSize;

                            Color color = editedPhoto.GetPixel(x, y);
                            int newRed = Math.Abs(color.R - 255);
                            int newGreen = Math.Abs(color.G - 255);
                            int newBlue = Math.Abs(color.B - 255);
                            Color newColor = Color.FromArgb(newRed, newGreen, newBlue);
                            editedPhoto.SetPixel(x, y, newColor);
                        }
                    }
                }
            });
        }
        private void ColorButton_Click(object sender, EventArgs e)
        {

        }

        private void UpdateTaskBar(int updatePercent)
        {
            progress.ProgressBarValue = updatePercent;
        }
    }
}
