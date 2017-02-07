using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using System.ServiceProcess;
using System.Threading;
using System.Diagnostics;

namespace testregkey
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // every type of device has a hard-coded GUID, this is the one for mice
            Guid mouseGuid = new Guid("{50906cb8-ba12-11d1-bf5d-0000f805f530}");

            // get this from the properties dialog box of this device in Device Manager
            string instancePath = @"ROOT\MULTIPORTSERIAL\0002";

            DeviceHelper.SetDeviceEnabled(mouseGuid, instancePath,false);

            MessageBox.Show("driver uitgeschakeld");

            Thread.Sleep(100);

            RegistryKey mykey = Registry.LocalMachine.OpenSubKey("SYSTEM\\ControlSet001\\Enum\\Root\\MULTIPORTSERIAL\\0002\\Device Parameters", true);
            if (mykey != null)
            {
                mykey.SetValue("IPAddress", "212.166.51.20", RegistryValueKind.String);
            }
            mykey.Close();

            RegistryKey mykey2 = Registry.LocalMachine.OpenSubKey("SYSTEM\\ControlSet001\\Enum\\Root\\MULTIPORTSERIAL\\0002", true);
            if (mykey2 != null)
            {
                mykey2.SetValue("FriendlyName", "MD2400 RABBIT 2000 (212.166.51.20)", RegistryValueKind.String);
            }
            mykey2.Close();

            Thread.Sleep(100);
             

            MessageBox.Show("register aangepast");

            DeviceHelper.SetDeviceEnabled(mouseGuid, instancePath, true);

            MessageBox.Show("driver ingeschakeld");

            Process.Start(@"C:\Program Files\Limotec\MD2400 PC Config\pc_md2400_v2.exe");

            /*            ServiceController service = new ServiceController("COMredirectSrv");
                        try
                        {
                            int timeoutMilliseconds = 500;
                            int millisec1 = Environment.TickCount;
                            TimeSpan timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds);

                            service.Stop();
                            service.WaitForStatus(ServiceControllerStatus.Stopped, timeout);

                            // count the rest of the timeout
                            int millisec2 = Environment.TickCount;
                            timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds - (millisec2 - millisec1));

                            service.Start();
                            service.WaitForStatus(ServiceControllerStatus.Running, timeout);
                        }
                        catch(Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        */
        }
    }
}
