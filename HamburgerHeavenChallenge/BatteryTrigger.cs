using System;
using System.Threading.Tasks;
using Windows.System.Power;
using Windows.UI.Core;
using Windows.UI.Xaml;

namespace HamburgerHeavenChallenge
{
    public class BatteryTrigger : StateTriggerBase
    {
        public class Charger
        {
            public string deviceName { get; set; };
            public int batteryPercent { get; set; };
            public int isConencted { get; set; };
            public int serial { get; set; };
        }

        private MySqlConnection connection;
        private MySqlCommand command;
        private MySqlDataReader reader;

        public bool Charging { get; set; }
        
        public BatteryTrigger()
        {
            Windows.Devices.Power.Battery.AggregateBattery.ReportUpdated
                += async (sender, args) => await UpdateStatus();

            UpdateStatus();
        }


        private async Task UpdateStatus()
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                var batteryReport = Windows.Devices.Power.Battery.AggregateBattery.GetReport();
                var percentage = (batteryReport.RemainingCapacityInMilliwattHours.Value /
                                    (double)batteryReport.FullChargeCapacityInMilliwattHours.Value);
               
                /*
                   
                if(batteryReport.Status==BatteryStatus.Charging && percentage<0.90)
                {
                    SetActive(!this.Charging);
                }

                else if(batteryReport.Status==BatteryStatus.Discharging && percentage<0.90)
                {
                    SetActive(this.Charging);
                    
                }

                else if(batteryReport.Status==BatteryStatus.Charging && percentage>=0.90)
                {
                    SetActive(ChargingAbove90);
                }

                else if(batteryReport.Status==BatteryStatus.Discharging && percentage >=0.90)
                {
                    SetActive(!ChargingAbove90);
                } */
                

                
                    switch (batteryReport.Status)
                    {
                        case BatteryStatus.Charging:
                            SetActive(!this.Charging);
                            break;
                        case BatteryStatus.Discharging:
                            SetActive(this.Charging);
                            break;
                    }
               
            });
        }
    }
}