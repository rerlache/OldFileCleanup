using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OldFileCleanup
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<FileInfo> files = new();
        public List<DirectoryInfo> directories = new();
        public MainWindow()
        {
            InitializeComponent();
            GetDriveInformations();
        }
        public void GetDriveInformations()
        {
            DriveInfo[] allDrives = DriveInfo.GetDrives().Where(x => x.IsReady == true).ToArray();
            drivesComboBox.ItemsSource = allDrives;
            /* -- the following loop can be used in a console application, need to be rebuild for WPF --
            foreach (DriveInfo d in allDrives)
            {
                Console.WriteLine("Drive {0}", d.Name);
                Console.WriteLine("  Drive type: {0}", d.DriveType);
                if (d.IsReady == true)
                {
                    Console.WriteLine("  Volume label: {0}", d.VolumeLabel);
                    Console.WriteLine("  File system: {0}", d.DriveFormat);
                    Console.WriteLine(
                        "  Available space to current user:{0, 15} bytes",
                        d.AvailableFreeSpace);

                    Console.WriteLine(
                        "  Total available space:          {0, 15} bytes",
                        d.TotalFreeSpace);

                    Console.WriteLine(
                        "  Total size of drive:            {0, 15} bytes ",
                        d.TotalSize);
                }
            }
            */
        }
        public void GetDriveContent(string DriveLetter)
        {
            DriveInfo driveInfo = new DriveInfo(DriveLetter);
            if (directories.Count >= 0)
            {
                directories.Clear();
            }
            if (files.Count >= 0)
            {
                files.Clear();
            }
            // TODO: fix access denied error for files and directories!
            // TODO: create treeview for files
            try
            {
                files = driveInfo.RootDirectory.GetFiles("*.*", SearchOption.AllDirectories).ToList();
                directories = driveInfo.RootDirectory.GetDirectories("*", SearchOption.AllDirectories).ToList();
            }
            catch (Exception)
            {
                ;
            }
            ContentHeader.Text = String.Format("found {0} files in {1} directories", files.Count().ToString(), directories.Count().ToString());
            //DriveContent.ItemsSource = files;
        }
        public void GetFoldercontent(string folderPath)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(folderPath);
            DirectoryInfo[] directories = directoryInfo.GetDirectories();
            FileInfo[] files = directoryInfo.GetFiles();
            ;
        }
        private void LoadDrivesButton_Click(object sender, RoutedEventArgs e)
        {
            GetDriveInformations();
        }

        private void DriveSelection(object sender, SelectionChangedEventArgs e)
        {
            GetDriveContent(e.AddedItems[0].ToString());
        }

        private void TreeViewExpand_Click(object sender, RoutedEventArgs e)
        {
            ;
            GetFoldercontent(sender.ToString());
        }
    }
}

