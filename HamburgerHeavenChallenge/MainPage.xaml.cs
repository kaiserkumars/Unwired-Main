using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Security.ExchangeActiveSyncProvisioning;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace HamburgerHeavenChallenge
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        public string DeviceManufacturer { get; set; }
        public string DeviceModel { get; set; }
        public string device_name { get; set; }
        public MainPage()
        {
            this.InitializeComponent();
            BackButton.Visibility = Visibility.Collapsed;
            MyFrame.Navigate(typeof(Financial));
            TitleTextBlock.Text = "Home";
            Financial.IsSelected = true;
            DeviceInformation();
        }

       
        public void DeviceInformation()
        {
            EasClientDeviceInformation eas = new EasClientDeviceInformation();
            DeviceManufacturer = eas.SystemManufacturer;
            DeviceModel = eas.SystemProductName;
            device_name = DeviceManufacturer + " " + DeviceModel;
        }

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
      {
        MySplitView.IsPaneOpen = !MySplitView.IsPaneOpen;
      }

      private void BackButton_Click(object sender, RoutedEventArgs e)
      {
        if (MyFrame.CanGoBack)
        {
          MyFrame.GoBack();
          Financial.IsSelected = true;
        }
      }

      private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
      {
        if (Financial.IsSelected)
        {
        BackButton.Visibility = Visibility.Collapsed;
          MyFrame.Navigate(typeof(Financial));
          TitleTextBlock.Text = "Home";
        }
        else if (Food.IsSelected)
        {
          BackButton.Visibility = Visibility.Visible;
          MyFrame.Navigate(typeof(Food));
          TitleTextBlock.Text = "Stats";
        }

        else if(Settings.IsSelected)
            {
                BackButton.Visibility = Visibility.Visible;
                MyFrame.Navigate(typeof(Settings));
                TitleTextBlock.Text = "Settings";
            }
        else if(Pie.IsSelected)
            {
                BackButton.Visibility = Visibility.Visible;
                MyFrame.Navigate(typeof(Pie));
                TitleTextBlock.Text = "Pie";
            }

      }
    }
}
