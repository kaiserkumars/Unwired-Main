using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using WinRTXamlToolkit.Controls.DataVisualization.Charting;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace HamburgerHeavenChallenge
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Pie : Page
    {
        public class Records
        {
            public string Device
            {
                get;
                set;
            }
            public int Power
            {
                get;
                set;
            }
        }

        public Pie()
        {
            this.InitializeComponent();
            LoadChartContents();
        }

        private void LoadChartContents()
        {

            List<Records> records1 = new List<Records>();
            records1.Add(new Records() { Device = "ASUS Z00XS", Power = 25 });
            records1.Add(new Records() { Device = "LENOVO PC", Power = 25 });
            records1.Add(new Records() { Device = "LUMIA 730", Power = 35 });
            records1.Add(new Records() { Device = "SAMSUNG J7", Power = 15 });

            (PieChart.Series[0] as PieSeries).ItemsSource = records1;
        }
    }
}
