using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace gameCenter.Projects
{
    /// <summary>
    /// Interaction logic for ProjectPresentationPage.xaml
    /// </summary>
    public partial class ProjectPresentationPage : Window
    {

        private Window currentProject;
        public ProjectPresentationPage()
        {
            InitializeComponent();
            DispatcherTimer clock = new()
            {
                Interval = TimeSpan.FromSeconds(1)
            };

            clock.Tick += ShowCurrentDate!;
            clock.Start();
        }

        private void ShowCurrentDate(object sender, EventArgs e)
        {
            DateLabel.Content = DateTime.UtcNow.ToString("dddd dd MMMM yyyy HH:mm:ss");
        }

        public void OnStart(string title, string ProjectDescription, ImageSource imageSource, Window project)
        {
            TitleLabel.Content = title;
            ProjectText.Text = ProjectDescription;
            ProjectImage.Source = imageSource;
            currentProject = project;
        }

        private void ProjectImage_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            currentProject.Show();
            Close();
        }
        private void Image_MouseEnter(object sender, MouseEventArgs e)
        {
            Image image = (sender as Image)!;
            image.Opacity = 0.7;
           
        }
        private void Image_MouseLeave(object sender, MouseEventArgs e)
        {
            (sender as Image)!.Opacity = 1;
        }

        private void Avatar_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

    }
}
