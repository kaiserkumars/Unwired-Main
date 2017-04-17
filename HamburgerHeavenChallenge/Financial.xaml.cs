using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Security.ExchangeActiveSyncProvisioning;
using Windows.UI.Core;
using Windows.UI.Notifications;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace HamburgerHeavenChallenge
{
  /// <summary>
  /// An empty page that can be used on its own or navigated to within a Frame.
  /// </summary>
  public sealed partial class Financial : Page
  {
        public string DeviceManufacturer { get; set; }
        public string DeviceModel { get; set; }
        public int DeviceOne=89;
        public int DeviceTwo = 67;
        public int DeviceThree = 35;
        DispatcherTimer dispatchTimerOne = new DispatcherTimer();
        DispatcherTimer dispatchTimerTwo = new DispatcherTimer();
        public Financial()
         {
            this.InitializeComponent();
            NavigationCacheMode = NavigationCacheMode.Enabled;  //This is an important line!!! It keeps the value of variables remain unchanged when navigating to other pages
            DeviceOnePercentage.Text = DeviceOne + "%";
            DeviceTwoPercentage.Text = DeviceTwo + "%";
            DeviceThreePercentage.Text = DeviceThree + "%";
            
            UpdatePercentageAsync();
           // DispatchTimerOneSetup();
         
         }

        private async Task UpdatePercentageAsync()
        {
            while (DeviceOne <= 100 || DeviceTwo <= 100 || DeviceThree <= 100)
            {
                if (DeviceOne <= 100)
                {
                    await Task.Delay(20000);
                    DeviceOne++;
                    if(DeviceOne<=100)
                    DeviceOnePercentage.Text = DeviceOne + "%";
                    
                    

                }

                if(DeviceOne ==91)
                {
                    DeviceOneBG.Background = new SolidColorBrush(Windows.UI.Colors.Orange);
                    RemoveChargeOne.Text = "Disconnect Charger!";
                    OtherDeviceToastNotification("ASUS Z00XS");

                }
                
                if (DeviceTwo <= 100)
                {
                    await Task.Delay(6000);
                    DeviceTwo--;
                    if(DeviceTwo<=100)
                    DeviceTwoPercentage.Text = DeviceTwo + "%";
                    
                    

                }

                if (DeviceTwo == 90)
                {
                    DeviceTwoBG.Background = new SolidColorBrush(Windows.UI.Colors.Orange);
                    
                    RemoveChargeTwo.Text = "Disconnect Charger!";
                    OtherDeviceToastNotification("LUMIA 730");
                }
               
                if (DeviceThree <= 100)
                {
                    await Task.Delay(3000);
                    DeviceThree++;
                    if(DeviceThree<=100)
                        DeviceThreePercentage.Text = DeviceThree + "%";
                   
                   

                }

                if (DeviceThree == 91)
                {
                    DeviceThreeBG.Background = new SolidColorBrush(Windows.UI.Colors.Orange);
                    RemoveChargeThree.Text = "Disconnect Charger!";
                    OtherDeviceToastNotification("LENOVO PC");
                }
            }

            
        }

        /*

        private void DispatchTimerOneSetup()
        {
            dispatchTimerOne.Interval = new TimeSpan(0, 0, 10);
            dispatchTimerOne.Tick += DispatchTimer_Tick;          
            dispatchTimerOne.Start();
                      
        }

        
        private void DispatchTimer_Tick(object sender, object e)
        {
            while (DeviceOne < 100 || DeviceTwo < 100 || DeviceThree < 100)
            {
                if (DeviceOne < 100)
                { DeviceOne++; DeviceOnePercentage.Text = DeviceOne + "%"; }
                if (DeviceTwo < 100)
                { DeviceTwo++; DeviceTwoPercentage.Text = DeviceTwo + "%"; }
                if (DeviceThree < 100)
                { DeviceThree++; DeviceThreePercentage.Text = DeviceThree + "%"; }

                if (DeviceOne == 100 || DeviceTwo == 100 || DeviceThree == 100)
                {
                    DeviceOneBG.Background = new SolidColorBrush(Windows.UI.Colors.Orange);
                    DeviceTwoBG.Background = new SolidColorBrush(Windows.UI.Colors.Orange);
                    DeviceThreeBG.Background = new SolidColorBrush(Windows.UI.Colors.Orange);

                }
            }

            dispatchTimerOne.Stop();
            //throw new NotImplementedException();
        }

    */
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {

            base.OnNavigatedTo(e);
            Windows.Devices.Power.Battery.AggregateBattery.ReportUpdated += AggregateBatteryOnReportUpdated;

            await UpdatePercentage(Windows.Devices.Power.Battery.AggregateBattery);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            Windows.Devices.Power.Battery.AggregateBattery.ReportUpdated -= AggregateBatteryOnReportUpdated;
        }

        private async void AggregateBatteryOnReportUpdated(Windows.Devices.Power.Battery sender, object args)
        {
            await UpdatePercentage(sender);
        }

        public async Task UpdatePercentage(Windows.Devices.Power.Battery sender)
        {
            EasClientDeviceInformation eas = new EasClientDeviceInformation();
            DeviceManufacturer = eas.SystemManufacturer;
            DeviceModel = eas.SystemProductName;
            
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                int intPercent;
                var batteryReport = sender.GetReport();
                var percentage = (batteryReport.RemainingCapacityInMilliwattHours.Value /
                                  (double)batteryReport.FullChargeCapacityInMilliwattHours.Value);
                intPercent = (int)(percentage*100);
                /*
                if (batteryReport.ChargeRateInMilliwatts > 10)
                    ChargeRate.Text = "Charging Rate = " + batteryReport.ChargeRateInMilliwatts.ToString() + " mWh";
                else
                    ChargeRate.Text = "Charging Rate = 0 mWh";*/
                if(batteryReport.FullChargeCapacityInMilliwattHours.Value>0)
                ChargeRate.Text = "Charging Rate = " + batteryReport.ChargeRateInMilliwatts.ToString() + " mWh";
                BatteryCapacity.Text = "Battery Design Capacity = "+batteryReport.DesignCapacityInMilliwattHours.ToString()+" mWh";
                CurrentBatteryCapacity.Text = "Current Battery Capacity = " + batteryReport.FullChargeCapacityInMilliwattHours.ToString()+" mWh";
                Life.Text = "Your battery has " + ((batteryReport.FullChargeCapacityInMilliwattHours.Value/ (double)batteryReport.DesignCapacityInMilliwattHours.Value)).ToString("##%") + " of its Original Capacity!";

                if (batteryReport.RemainingCapacityInMilliwattHours.Value > batteryReport.FullChargeCapacityInMilliwattHours.Value)  //Since Design Capacity is 41440mWh and Full Charge Capacity is 11840mWh therefore at 100% charge thae app will show 350% charged! To prevent it we use this if condition
                {
                    BatteryPercentageTextBlock.Text = "100%";
                    StatusTextBlock.Text = "Disconnect Charger!";
                }
                else
                {
                    
                    if (intPercent >= 90)
                    BatteryPercentageTextBlock.Foreground = new SolidColorBrush(Windows.UI.Colors.Orange);
                    else
                    BatteryPercentageTextBlock.Foreground = new SolidColorBrush(Windows.UI.Colors.White);
                    BatteryPercentageTextBlock.Text = percentage.ToString("##%");
                }

                if (batteryReport.Status == Windows.System.Power.BatteryStatus.Discharging)
                    StatusTextBlock.Text = "Discharging!";
                ManufacturerModel.Text = DeviceManufacturer + " " + DeviceModel;
                

               
                if (intPercent == 90 && batteryReport.ChargeRateInMilliwatts>0)
                {
                    
                    CurrentDeviceShowToastNotification();
                }



            });
        }

        private void CurrentDeviceShowToastNotification()
        {
            ToastNotifier ToastNotifier = ToastNotificationManager.CreateToastNotifier();
            Windows.Data.Xml.Dom.XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastText02);
            Windows.Data.Xml.Dom.XmlNodeList toastNodeList = toastXml.GetElementsByTagName("text");
            toastNodeList.Item(0).AppendChild(toastXml.CreateTextNode(DeviceModel));
            toastNodeList.Item(1).AppendChild(toastXml.CreateTextNode("The charge is greater than 90%. You can remove the charger now!"));
            Windows.Data.Xml.Dom.IXmlNode toastNode = toastXml.SelectSingleNode("/toast");
            Windows.Data.Xml.Dom.XmlElement audio = toastXml.CreateElement("audio");
            audio.SetAttribute("src", "ms-winsoundevent:Notification.SMS");

            ToastNotification toast = new ToastNotification(toastXml);
            toast.ExpirationTime = DateTime.Now.AddSeconds(15);
            ToastNotifier.Show(toast);
        }

        private void OtherDeviceToastNotification(string DeviceName)
        {
            ToastNotifier ToastNotifier = ToastNotificationManager.CreateToastNotifier();
            Windows.Data.Xml.Dom.XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastText02);
            Windows.Data.Xml.Dom.XmlNodeList toastNodeList = toastXml.GetElementsByTagName("text");
            toastNodeList.Item(0).AppendChild(toastXml.CreateTextNode(DeviceName));
            toastNodeList.Item(1).AppendChild(toastXml.CreateTextNode("The charge on this device is greater than 90%. You can remove the charger now!"));
            Windows.Data.Xml.Dom.IXmlNode toastNode = toastXml.SelectSingleNode("/toast");
            Windows.Data.Xml.Dom.XmlElement audio = toastXml.CreateElement("audio");
            audio.SetAttribute("src", "ms-winsoundevent:Notification.SMS");

            ToastNotification toast = new ToastNotification(toastXml);
            toast.ExpirationTime = DateTime.Now.AddSeconds(15);
            ToastNotifier.Show(toast);
        }

    }
}
