using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
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
            //https://www.tutorialsteacher.com/csharp/csharp-stream-io
            path = filePath;
            byte[] bytes = System.IO.File.ReadAllBytes(filePath);
            MemoryStream ms = new MemoryStream(bytes);
            selectedPhoto = new Bitmap(Image.FromStream(ms));

            InitializeComponent();
            imageView.BackgroundImage = selectedPhoto;
        }

        //event handlers

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void SaveButton_Click_1(object sender, EventArgs e)
        {
            editedPhoto = new Bitmap(imageView.BackgroundImage);
            // https://sites.harding.edu/fmccown/classes/comp4450-f19/Photo%20Editor.pdf
            editedPhoto.Save(path, ImageFormat.Jpeg);
        }

        async private void BrightnessBar_Scroll(object sender, EventArgs e)
        {
            editedPhoto = new Bitmap(imageView.BackgroundImage);
            int amount = Convert.ToInt32(2 * (50 - brightnessBar.Value) * 0.01 * 255);
            progress = new progressForm();
            progress.cancel += cancelTask;
            progress.Show();
            progress.Left = this.Left + 75;
            progress.Top = this.Top + 80;

            await ChangeImageBrightness(amount);

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
                progress.cancel += cancelTask;
                progress.Show();
                progress.Left = this.Left + 75;
                progress.Top = this.Top + 80;

                await TintColors(selectedColor);

                progress.Close();
                this.BringToFront();
                imageView.BackgroundImage = editedPhoto;
            }

        }

        async private void InvertButton_Click(object sender, EventArgs e)
        {
            cancellationTokenSource = new CancellationTokenSource();
            var token = cancellationTokenSource.Token;

            editedPhoto = new Bitmap(imageView.BackgroundImage);
            progress = new progressForm();
            progress.cancel += cancelTask;
            progress.Show();
            progress.Left = this.Left + 75;
            progress.Top = this.Top + 80;

            await InvertColors(token);
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
                    double partial = .05 * editedPhoto.Height;

                    if (y % (int)partial == 0)
                    {
                        Invoke((Action)delegate ()
                        {
                            progress.ProgressBarValue++;
                        });
                    }
                    if (token.IsCancellationRequested)
                    {
                        editedPhoto = new Bitmap(imageView.BackgroundImage);
                        break;
                    }
                    else
                    {
                        for (int x = 0; x < editedPhoto.Width; x++)
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
        async Task InvertColors(CancellationToken token)
        {


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
                    double partial = .05 * editedPhoto.Height;
          
                    if(y%(int)partial == 0)
                    {
                        Invoke((Action)delegate ()
                        {
                            progress.ProgressBarValue++;
                        });
                    }
                    if (token.IsCancellationRequested)
                    {
                        editedPhoto = new Bitmap(imageView.BackgroundImage);
                        break;
                    }
                    else
                    {
                        for (int x = 0; x < editedPhoto.Width; x++)
                        {
                            
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

        async Task ChangeImageBrightness(int value)
        {
            int totalSize = (editedPhoto.Height) * (editedPhoto.Width);

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
                    double partial = .05 * editedPhoto.Height;

                    if (y % (int)partial == 0)
                    {
                        Invoke((Action)delegate ()
                        {
                            progress.ProgressBarValue++;
                        });
                    }
                    if (token.IsCancellationRequested)
                    {
                        Invoke((Action)delegate ()
                        {
                            this.brightnessBar.Value = 50;
                        });
                        editedPhoto = new Bitmap(imageView.BackgroundImage);
                        break;
                    }
                    else
                    {
                        for (int x = 0; x < editedPhoto.Width; x++)
                        {

                            Color color = editedPhoto.GetPixel(x, y);
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

        private void cancelTask()
        {
            cancellationTokenSource.Cancel();
        }

        private void BrightnessBar_Scroll(object sender, MouseEventArgs e)
        {

        }
    }
}
