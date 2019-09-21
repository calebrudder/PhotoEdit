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
            InitializeTreeView();
            InitializeImageLists();
            InitializeListView();
        }


        private void InitializeTreeView()
        {
            // https://stackoverflow.com/a/116061
            string picturesPath = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            rootDir = new DirectoryInfo(picturesPath);

            PopulateDirectoryTreeView(rootDir, currentDirectoryTreeView.Nodes);
            currentDirectoryTreeView.Nodes[0].Expand();
        }

        private void InitializeImageLists()
        {
            smallImageList = new ImageList();
            largeImageList = new ImageList();

            smallImageList.ImageSize = new Size(64, 64);
            largeImageList.ImageSize = new Size(120, 120);
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
            string dirPath = (string)currentDirectoryTreeView.SelectedNode.Tag;
            await PopulateImageList(dirPath);
        }

        private async Task PopulateImageList(string dirPath)
        {
            await Task.Run(() =>
            {
                DirectoryInfo rootDir = new DirectoryInfo(dirPath);
                FileInfo[] imageFiles = rootDir.GetFiles("*.jpg");
                foreach (FileInfo file in imageFiles)
                {
                    try
                    {
                        // Get file data
                        byte[] bytes = File.ReadAllBytes(file.FullName);
                        MemoryStream ms = new MemoryStream(bytes);
                        Image img = Image.FromStream(ms);

                        int newImageIndex = -1;

                        // Add image to image list
                        Invoke((Action)delegate
                        {
                            newImageIndex = AddImageToImageLists(img);
                        });

                        // Create ListViewItem from file data
                        ListViewItem item = new ListViewItem(file.Name, newImageIndex);
                        item.SubItems.Add(file.LastAccessTime.ToString());
                        item.SubItems.Add(ConvertBytesToString(file.Length));

                        // Add ListViewItem to ListView
                        Invoke((Action)delegate
                        {
                            currentDirectoryImagesView.Items.Add(item);
                        });
                    }
                    catch
                    {
                        Console.WriteLine("Could not read " + file.FullName);
                    }
                }
            });
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
    }
}
