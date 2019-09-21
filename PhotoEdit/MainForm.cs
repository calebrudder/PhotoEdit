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

        public MainForm()
        {
            InitializeComponent();
            InitializeTreeView();
        }

        private void InitializeTreeView()
        {
            // https://stackoverflow.com/a/116061
            string picturesPath = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            rootDir = new DirectoryInfo(picturesPath);

            PopulateDirectoryTreeView(rootDir, currentDirectoryTreeView.Nodes);
            currentDirectoryTreeView.ExpandAll();
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
    }
}
