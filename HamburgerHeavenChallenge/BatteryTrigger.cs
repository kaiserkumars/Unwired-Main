using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Security.ExchangeActiveSyncProvisioning;
using Windows.System.Power;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;

namespace HamburgerHeavenChallenge
{
    public class BatteryTrigger : StateTriggerBase
    {
        public class Charger
        {
            public string device_name { get; set; }
            public int battery { get; set; }
            public int is_conencted { get; set; }
          //  public int Serial { get; set; }
        }


        public string DeviceManufacturer { get; set; }
        public string DeviceModel { get; set; }
        public string device_name { get; set; }


        private MySqlConnection connection;
        private MySqlCommand command;
        private MySqlDataReader reader;
        List<Charger> devicieData { get; set; }
        Int32 record_counter;
        public bool Charging { get; set; }
        
        public BatteryTrigger()
        {
            Windows.Devices.Power.Battery.AggregateBattery.ReportUpdated
                += async (sender, args) => await UpdateStatus();
            DeviceName();      
            //DataRetrieve();  //calling the data retrieval method      
            UpdateStatus();
        }

        public void DeviceName()
        {
            EasClientDeviceInformation eas = new EasClientDeviceInformation();
            DeviceManufacturer = eas.SystemManufacturer;
            DeviceModel = eas.SystemProductName;
            device_name = DeviceManufacturer + " " + DeviceModel;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////DATABASE///////////////////////////////////////////////////////////////////////////////////////////
        
        private async Task makeConnection(string Query) //function to make connection to database
        {
            try
            {
                connection = new MySqlConnection();
                connection.ConnectionString = @"datasource=sql12.freesqldatabase.com;port=3306;username=sql12169494;password=CcIbXhMBuY;database=sql12169494;";
                command = new MySqlCommand(Query, connection);
                
                connection.Open();
                reader = command.ExecuteReader();

            }
            catch (Exception ex)
            {
                var dialog = new MessageDialog(ex.ToString());
                await dialog.ShowAsync();
            }

        }


        public void DataRetrieve()
        {
            List<Charger> dbData = new List<Charger>();
            string Query = "SELECT * FROM BatteryPercentage WHERE 1";
            makeConnection(Query);
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                Charger newCharger = new Charger();
                newCharger.device_name = reader["device_name"].ToString();
                newCharger.battery = Convert.ToInt32(reader["battery"]);
                newCharger.is_conencted = Convert.ToInt32(reader["is_connected"]);
                //newCharger.Serial = Convert.ToInt32(reader["Serial"]);
                dbData.Add(newCharger);

            }
        }


        public async Task SendData(int percentage,string device_name)
        {
            string Query = "UPDATE sql12169494.BatteryPercentage SET battery='"+percentage+"',is_connected=1 WHERE device_name='"+device_name+"';";
            
            try
            {
                await makeConnection(Query);
                var dialog = new MessageDialog("Success");
                await dialog.ShowAsync();
                connection.Close();
            }

            catch (Exception ex)
            {
               
            }
        }

        //////////////////////////////////////////////////DATABASE///////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private async Task UpdateStatus()
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
            {
                var batteryReport = Windows.Devices.Power.Battery.AggregateBattery.GetReport();
                var percentage = (batteryReport.RemainingCapacityInMilliwattHours.Value /
                                    (double)batteryReport.FullChargeCapacityInMilliwattHours.Value);
                
                await SendData(Convert.ToInt32(percentage*100), device_name);

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