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
        private progressForm progress;
        private CancellationTokenSource cancellationTokenSource;
        private String path;
        public PhotoEditForm(String filePath)
        {
            path = filePath;
            selectedPhoto = new Bitmap(Image.FromFile(filePath));

            InitializeComponent();
            imageView.BackgroundImage = selectedPhoto;
        }

        //event handlers

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void SaveButton_Click(object sender, EventArgs e)
        {
            editedPhoto = new Bitmap(imageView.BackgroundImage);
            editedPhoto.Save(path, ImageFormat.Jpeg);
        }

        async private void BrightnessBar_Scroll(object sender, EventArgs e)
        {
            editedPhoto = new Bitmap(imageView.BackgroundImage);
            int amount = Convert.ToInt32(2 * (50 - brightnessBar.Value) * 0.01 * 255);
            progress = new progressForm();
            progress.Show();

            var progressTrack = new Progress<int>(percent =>
            {
                progress.ProgressBarValue = percent;
            });

            await ChangeImageBrightness(amount, progressTrack);

            progress.Close();
            this.BringToFront();
            imageView.BackgroundImage = editedPhoto;
        }

        async private void ColorButton_Click(object sender, EventArgs e)
        {
            Color selectedColor;

            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                selectedColor = colorDialog1.Color;

                editedPhoto = new Bitmap(imageView.BackgroundImage);
                progress = new progressForm();
                progress.Show();

                await TintColors(selectedColor);

                progress.Close();
                this.BringToFront();
                imageView.BackgroundImage = editedPhoto;
            }

        }

        async private void InvertButton_Click(object sender, EventArgs e)
        {

            var progressTrack = new Progress<int>(percent =>
            {
                progress.ProgressBarValue = percent;
            });

            editedPhoto = new Bitmap(imageView.BackgroundImage);
            progress = new progressForm();
            progress.Show();
            await InvertColors(progressTrack);
            progress.Close();
            this.BringToFront();
            imageView.BackgroundImage = editedPhoto;
        }



        //Async tasks for doing work

        async Task TintColors(Color tint)
        {
            cancellationTokenSource = new CancellationTokenSource();
            var token = cancellationTokenSource.Token;

            await Task.Run(() =>
            {
                Invoke((Action)delegate ()
                {
                    this.cancelButton.Enabled = false;
                    this.saveButton.Enabled = false;
                    this.invertButton.Enabled = false;
                    this.colorButton.Enabled = false;
                    this.brightnessBar.Enabled = false;
                });
                UseWaitCursor = true;

                for (int y = 0; y < editedPhoto.Height; y++)
                {
                    for (int x = 0; x < editedPhoto.Width; x++)
                    {
                        if (token.IsCancellationRequested)
                        {
                            editedPhoto = new Bitmap(imageView.BackgroundImage);
                            break;
                        }
                        else
                        {
                            Color color = editedPhoto.GetPixel(x, y);
                            double avgRGB = ((color.R + color.G + color.B) / 3);
                            double percent = (avgRGB / 255) * 100;
                            int newRed = (tint.R * (int)percent) / 100;
                            int newGreen = (tint.G * (int)percent) / 100;
                            int newBlue = (tint.B * (int)percent) / 100;
                            Color newColor = Color.FromArgb(newRed, newGreen, newBlue);
                            editedPhoto.SetPixel(x, y, newColor);
                        }
                    }
                }

                Invoke((Action)delegate ()
                {
                    this.cancelButton.Enabled = true;
                    this.saveButton.Enabled = true;
                    this.invertButton.Enabled = true;
                    this.colorButton.Enabled = true;
                    this.brightnessBar.Enabled = true;
                });
                UseWaitCursor = false;
            });


        }
        async Task InvertColors(IProgress<int> progressTrack)
        {
            cancellationTokenSource = new CancellationTokenSource();
            var token = cancellationTokenSource.Token;

            await Task.Run(() =>
            {
                Invoke((Action)delegate ()
                {
                    this.cancelButton.Enabled = false;
                    this.saveButton.Enabled = false;
                    this.invertButton.Enabled = false;
                    this.colorButton.Enabled = false;
                    this.brightnessBar.Enabled = false;
                });
                UseWaitCursor = true;

                int totalSize = (editedPhoto.Height) * (editedPhoto.Width);
                int totalProgress;

                for (int y = 0; y < editedPhoto.Height; y++)
                {
                    for (int x = 0; x < editedPhoto.Width; x++)
                    {
                        if (token.IsCancellationRequested)
                        {
                            editedPhoto = new Bitmap(imageView.BackgroundImage);
                            break;
                        }
                        else
                        {
                            totalProgress = ((y + x) / totalSize) * 100;
                            if (progressTrack != null && totalProgress >= 1)
                            {
                                progressTrack.Report(totalProgress);
                            }
                            Color color = editedPhoto.GetPixel(x, y);
                            int newRed = Math.Abs(color.R - 255);
                            int newGreen = Math.Abs(color.G - 255);
                            int newBlue = Math.Abs(color.B - 255);
                            Color newColor = Color.FromArgb(newRed, newGreen, newBlue);
                            editedPhoto.SetPixel(x, y, newColor);


                        }
                    }
                }
                Invoke((Action)delegate ()
                {
                    this.cancelButton.Enabled = true;
                    this.saveButton.Enabled = true;
                    this.invertButton.Enabled = true;
                    this.colorButton.Enabled = true;
                    this.brightnessBar.Enabled = true;
                });
                UseWaitCursor = false;
            });
        }

        async Task ChangeImageBrightness(int value, IProgress<int> progressTrack)
        {
            int totalSize = (editedPhoto.Height) * (editedPhoto.Width);
            int totalProgress;

            await Task.Run(() =>
            {
                Invoke((Action)delegate ()
                {
                    this.cancelButton.Enabled = false;
                    this.saveButton.Enabled = false;
                    this.invertButton.Enabled = false;
                    this.colorButton.Enabled = false;
                    this.brightnessBar.Enabled = false;
                });
                UseWaitCursor = true;

                cancellationTokenSource = new CancellationTokenSource();
                var token = cancellationTokenSource.Token;

                for (int y = 0; y < editedPhoto.Height; y++)
                {
                    for (int x = 0; x < editedPhoto.Width; x++)
                    {
                        if (token.IsCancellationRequested)
                        {
                            editedPhoto = new Bitmap(imageView.BackgroundImage);
                            break;
                        }
                        else
                        {

                            Color color = selectedPhoto.GetPixel(x, y);
                            int newRed = (color.R - value);
                            if (newRed > 255)
                            {
                                newRed = 255;
                            }
                            if (newRed < 0)
                            {
                                newRed = 0;
                            }

                            int newGreen = (color.G - value);
                            if (newGreen > 255)
                            {
                                newGreen = 255;
                            }
                            if (newGreen < 0)
                            {
                                newGreen = 0;
                            }

                            int newBlue = (color.B - value);
                            if (newBlue > 255)
                            {
                                newBlue = 255;
                            }
                            if (newBlue < 0)
                            {
                                newBlue = 0;
                            }

                            totalProgress = ((y + x) / totalSize) * 100;
                            if (progressTrack != null && totalProgress >= 1)
                            {
                                progressTrack.Report(totalProgress);
                            }

                            Color newColor = Color.FromArgb(newRed, newGreen, newBlue);
                            editedPhoto.SetPixel(x, y, newColor);
                        }
                    }

                }

                Invoke((Action)delegate ()
                {
                    this.cancelButton.Enabled = true;
                    this.saveButton.Enabled = true;
                    this.invertButton.Enabled = true;
                    this.colorButton.Enabled = true;
                    this.brightnessBar.Enabled = true;
                });
                UseWaitCursor = false;
            });
        }

    }
}
