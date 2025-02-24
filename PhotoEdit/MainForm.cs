﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhotoEdit
{
    public partial class MainForm : Form
    {
        private DirectoryInfo rootDir;
        private ImageList largeImageList;
        private ImageList smallImageList;
        private ImageList treeViewImageList;
        private CancellationTokenSource cancellationTokenSource;

        public MainForm()
        {
            InitializeComponent();
            
            // https://stackoverflow.com/a/116061
            InitializeTreeView(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures));
            InitializeImageLists();
            InitializeListView();

            readImagesProgressBar.Visible = false;
        }

        private void InitializeTreeView(string rootPath)
        {
            currentDirectoryTreeView.Nodes.Clear();

            rootDir = new DirectoryInfo(rootPath);

            PopulateDirectoryTreeView(rootDir, currentDirectoryTreeView.Nodes);
            currentDirectoryTreeView.Nodes[0].Expand();

            // Set up the ImageList
            treeViewImageList = new ImageList();
            treeViewImageList.Images.Add(new Bitmap(GetType(), "Images.folderIcon.png"));
            treeViewImageList.ColorDepth = ColorDepth.Depth32Bit;

            currentDirectoryTreeView.ImageList = treeViewImageList;

            // "Select" the first node so the first directory is populated with images
            currentDirectoryTreeView.SelectedNode = currentDirectoryTreeView.Nodes[0];
        }
        
        private void InitializeImageLists()
        {
            smallImageList = new ImageList();
            largeImageList = new ImageList();

            smallImageList.ImageSize = new Size(64, 64);
            largeImageList.ImageSize = new Size(120, 120);

            // https://social.msdn.microsoft.com/Forums/en-US/19c116bf-6261-4ba3-84a9-71d2fda4e030/help-regarding-thumbnail-images-in-listview?forum=winforms
            smallImageList.ColorDepth = ColorDepth.Depth32Bit;
            largeImageList.ColorDepth = ColorDepth.Depth32Bit;
        }

        private void InitializeListView()
        {
            // Add columns
            currentDirectoryImagesView.Columns.Add("Name", 300, HorizontalAlignment.Left);
            currentDirectoryImagesView.Columns.Add("Date", 150, HorizontalAlignment.Left);
            currentDirectoryImagesView.Columns.Add("Size", 75, HorizontalAlignment.Left);

            // Add ImageLists
            currentDirectoryImagesView.SmallImageList = smallImageList;
            currentDirectoryImagesView.LargeImageList = largeImageList;

            // Show default view
            currentDirectoryImagesView.View = View.Details;
            detailToolStripMenuItem.Checked = true;
        }

        // Recursively discovers all the subdirectories from one root directory
        private void PopulateDirectoryTreeView(DirectoryInfo rootDir, TreeNodeCollection nodes)
        {
            string dirName = rootDir.Name;
            nodes.Add(dirName, dirName);
            nodes[dirName].Tag = rootDir.FullName;

            foreach (DirectoryInfo subDir in rootDir.GetDirectories())
            {
                PopulateDirectoryTreeView(subDir, nodes[dirName].Nodes);
            }
        }

        private async void CurrentDirectoryTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            // Cancel the task (assume the token is not null when task is running)
            if (cancellationTokenSource != null)
            {
                cancellationTokenSource.Cancel();
            }

            // Make a new cancellationTokenSource
            cancellationTokenSource = new CancellationTokenSource();
            var token = cancellationTokenSource.Token;

            // Clear lists that possibly have old directory data
            currentDirectoryImagesView.Items.Clear();
            ClearImageLists();

            // Get the new directory path
            string dirPath = (string)currentDirectoryTreeView.SelectedNode.Tag;

            await PopulateImages(dirPath, token);
        }

        private async Task PopulateImages(string dirPath, CancellationToken token)
        {
            if (!token.IsCancellationRequested)
                readImagesProgressBar.Visible = true;

            await Task.Run(() =>
            {
                DirectoryInfo rootDir = new DirectoryInfo(dirPath);
                FileInfo[] imageFiles = rootDir.GetFiles("*.jpg");
                foreach (FileInfo file in imageFiles)
                {
                    if (token.IsCancellationRequested) break;

                    Image img = GetImageFromFile(file.FullName);

                    if (img == null)
                    {
                        // Image could not be read from file
                        continue;
                    }

                    int newImageIndex = -1;

                    // Add image to image list
                    Invoke((Action)delegate
                    {
                        if (!token.IsCancellationRequested)
                            newImageIndex = AddImageToImageLists(img);
                    });

                    ListViewItem item = CreateListViewItem(file, newImageIndex);

                    // Add ListViewItem to ListView
                    Invoke((Action)delegate
                    {
                        if (!token.IsCancellationRequested)
                            currentDirectoryImagesView.Items.Add(item);
                    });
                }
            }, token);

            if (!token.IsCancellationRequested)
                readImagesProgressBar.Visible = false;
        }

        private Image GetImageFromFile(string filePath)
        {
            try
            {
                // Get file data
                byte[] bytes = File.ReadAllBytes(filePath);
                MemoryStream ms = new MemoryStream(bytes);
                Image img = Image.FromStream(ms);
                return img;
            }
            catch
            {
                Console.WriteLine("Could not read " + filePath);
                return null;
            }
        }

        // Creates a ListViewItem with a Name, LastAccessTime, and size of file
        private ListViewItem CreateListViewItem(FileInfo file, int imageIndex)
        {
            // Create ListViewItem from file data
            ListViewItem item = new ListViewItem(file.Name, imageIndex);
            item.SubItems.Add(file.LastAccessTime.ToString());
            item.SubItems.Add(ConvertBytesToString(file.Length));
            item.Tag = file.FullName;

            return item;
        }

        // First converts the number of bytes to either 
        // kilobytes or megabytes, then converts that number 
        // to a string suffixed with the correct abbreviation.
        private string ConvertBytesToString(long bytes)
        {
            string kilobytes = "KB";
            string megabytes = "MB";

            long numKiloBytes = bytes / 1024;

            // Return number in kilobytes
            if (numKiloBytes < 1024)
            {
                return numKiloBytes.ToString() + kilobytes;
            }

            long numMegaBytes = numKiloBytes / 1024;

            // Return number in megabytes
            return numMegaBytes.ToString() + megabytes;
        }

        private int AddImageToImageLists(Image img)
        {
            smallImageList.Images.Add(img);
            largeImageList.Images.Add(img);
            return smallImageList.Images.Count - 1;
        }

        private void ClearImageLists()
        {
            largeImageList.Images.Clear();
            smallImageList.Images.Clear();
        }

        private void SelectRootFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Open a folder picker set root dir to that file. 
            FolderBrowserDialog browser = new FolderBrowserDialog();
            browser.RootFolder = Environment.SpecialFolder.MyComputer;
            DialogResult result = browser.ShowDialog();

            if (result == DialogResult.OK)
            {
                ClearImageLists();
                InitializeTreeView(browser.SelectedPath);
            }
        }

        private void CurrentDirectoryImagesView_ItemActivate(object sender, EventArgs e)
        {
            ListViewItem selectedImage = currentDirectoryImagesView.SelectedItems[0];
            string filePath = (string)selectedImage.Tag;

            PhotoEditForm photoEditForm = new PhotoEditForm(filePath);

            photoEditForm.ShowDialog();
        }

        private void LocateOnDiskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Open Windows File Explorer and select the image that is selected in
            // the ListView
            try
            {
                // Get selected file
                string imagePath = (string)currentDirectoryImagesView.SelectedItems[0].Tag;

                // https://stackoverflow.com/a/47182791
                Process.Start("explorer.exe", "/select,\"" + imagePath + "\"");
            }
            catch
            {
                MessageBox.Show("First select an image, then choose this option to see its location on disk.", 
                    "No Image Selected", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Information);
            }
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ViewSubMenuItem_Click(object sender, EventArgs e)
        {
            // Uncheck the already checked menu item
            foreach (ToolStripMenuItem item in viewToolStripMenuItem.DropDownItems)
            {
                if (item.Checked) item.Checked = false;
            }

            // Check the menu item that triggered the event
            ToolStripMenuItem selectedItem = (ToolStripMenuItem)sender;
            selectedItem.Checked = true;

            // Change the ListView.View to the corresponding menu item
            if (selectedItem.Text == "Details")
            {
                currentDirectoryImagesView.View = View.Details;
            } 
            else if(selectedItem.Text == "Large")
            {
                currentDirectoryImagesView.View = View.LargeIcon;
            }
            else if(selectedItem.Text == "Small")
            {
                currentDirectoryImagesView.View = View.SmallIcon;
            }
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox about = new AboutBox();
            about.ShowDialog();
        }
    }
}
