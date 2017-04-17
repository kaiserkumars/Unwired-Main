using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using WinRTXamlToolkit.Controls.DataVisualization.Charting;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace HamburgerHeavenChallenge
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Food : Page
    {
        public class Records  
        {  
    public string Day
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

    public Food()
        {
            this.InitializeComponent();
            LoadChartContents();


        }

        private void LoadChartContents()
        {

            List<Records> records1 = new List<Records>();
            records1.Add(new Records() { Day = "10 April", Power = 17 });
            records1.Add(new Records() { Day = "11 April", Power = 20 });
            records1.Add(new Records() { Day = "12 April", Power = 23 });
            records1.Add(new Records() { Day = "13 April", Power = 27 });

            (ColumnChart1.Series[0] as ColumnSeries).ItemsSource = records1;

            List<Records> records2 = new List<Records>();
            records2.Add(new Records() { Day = "10 April", Power = 30 });
            records2.Add(new Records() { Day = "11 April", Power = 15 });
            records2.Add(new Records() { Day = "12 April", Power = 5 });
            records2.Add(new Records() { Day = "13 April", Power = 20 });

            (ColumnChart2.Series[0] as ColumnSeries).ItemsSource = records2;

            List<Records> records3 = new List<Records>();
            records3.Add(new Records() { Day = "10 April", Power = 5 });
            records3.Add(new Records() { Day = "11 April", Power = 14 });
            records3.Add(new Records() { Day = "12 April", Power = 31 });
            records3.Add(new Records() { Day = "13 April", Power = 12 });

            (ColumnChart3.Series[0] as ColumnSeries).ItemsSource = records3;

            List<Records> records4 = new List<Records>();
            records4.Add(new Records() { Day = "10 April", Power = 15 });
            records4.Add(new Records() { Day = "11 April", Power = 4 });
            records4.Add(new Records() { Day = "12 April", Power = 2 });
            records4.Add(new Records() { Day = "13 April", Power = 10 });

            (ColumnChart4.Series[0] as ColumnSeries).ItemsSource = records4;

        }
    }

}
