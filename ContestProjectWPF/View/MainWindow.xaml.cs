using ContestProject.ViewModel;
using System.Windows;

namespace ContestProject.View
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }

        public MainViewModel ViewModel => (MainViewModel)DataContext;

        private void Filename_Drop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            txtFilename.Text = files[0];
        }

        private void Filename_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effects = DragDropEffects.Copy;
        }
    }
}
