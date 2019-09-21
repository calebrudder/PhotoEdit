using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhotoEdit
{
    public partial class MainForm : Form
    {
        private DirectoryInfo rootDir;
        private ImageList largeImageList;
        private ImageList smallImageList;

        public MainForm()
        {
            InitializeComponent();
            
            // https://stackoverflow.com/a/116061
            InitializeTreeView(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures));
            InitializeImageLists();
            InitializeListView();
        }

        private void InitializeTreeView(string rootPath)
        {
            currentDirectoryTreeView.Nodes.Clear();

            rootDir = new DirectoryInfo(rootPath);

            PopulateDirectoryTreeView(rootDir, currentDirectoryTreeView.Nodes);
            currentDirectoryTreeView.Nodes[0].Expand();

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
            currentDirectoryImagesView.Columns.Add("Size", 100, HorizontalAlignment.Left);

            // Add ImageLists
            currentDirectoryImagesView.SmallImageList = smallImageList;
            currentDirectoryImagesView.LargeImageList = largeImageList;

            // Show default view
            currentDirectoryImagesView.View = View.Details;
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
            ClearImageLists();
            string dirPath = (string)currentDirectoryTreeView.SelectedNode.Tag;
            await PopulateImages(dirPath);
        }

        private async Task PopulateImages(string dirPath)
        {
            await Task.Run(() =>
            {
                DirectoryInfo rootDir = new DirectoryInfo(dirPath);
                FileInfo[] imageFiles = rootDir.GetFiles("*.jpg");
                foreach (FileInfo file in imageFiles)
                {
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
                        newImageIndex = AddImageToImageLists(img);
                    });

                    ListViewItem item = CreateListViewItem(file, newImageIndex);

                    // Add ListViewItem to ListView
                    Invoke((Action)delegate
                    {
                        currentDirectoryImagesView.Items.Add(item);
                    });
                }
            });
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

            PhotoEditForm photoEditForm = new PhotoEditForm();

            photoEditForm.ShowDialog();
        }
    }
}
