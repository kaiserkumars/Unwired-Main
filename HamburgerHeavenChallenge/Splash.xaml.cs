using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.ApplicationModel.Activation;
using Windows.UI.Core;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace HamburgerHeavenChallenge
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public partial class Splash : Page
    {
       
        public Splash()
        {

            this.InitializeComponent();
            ExtendedSplashScreen();
        }

        private async void ExtendedSplashScreen()
        {
            await Task.Delay(TimeSpan.FromSeconds(4)); // set your desired delay    
            Frame.Navigate(typeof(MainPage)); // call MainPage    
        }
    }
}
