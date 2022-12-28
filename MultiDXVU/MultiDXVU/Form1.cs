using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NDXVUMeterNET;

namespace MultiDXVU
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DXVUMeterNETGDI dummyDXVUCtrl = new DXVUMeterNETGDI();

            dummyDXVUCtrl.ControlIsReady += () =>
            {
                Point p = Point.Empty;
                Size s = new Size(this.Width - 12, 100);

                foreach (DXVUMeterNETGDI.DevicesCollection.Device device in dummyDXVUCtrl.Devices)
                {
                    if(device.GUID == Guid.Empty) continue;
                    AddDXVU(device, p, s);
                    p.Y += (s.Height + 4);
                }

                this.Controls.Remove(dummyDXVUCtrl);
                dummyDXVUCtrl.Dispose();
            };

            this.Controls.Add(dummyDXVUCtrl);
        }

        private void AddDXVU(DXVUMeterNETGDI.DevicesCollection.Device device, Point location, Size size)
        {
            DXVUMeterNETGDI dxvu = new DXVUMeterNETGDI();
            dxvu.ControlIsReady += () => {
                //dxvu.LicenseControl("Serial Number", "Order Number");

                dxvu.Style = DXVUMeterNETGDI.StyleConstants.DigitalVU;
                dxvu.BackColor = Color.Black;

                dxvu.Frequency = 44100;
                dxvu.Channels = 2;
                dxvu.BitDepth = 16;

                dxvu.Devices.SelectedDevice = device;

                dxvu.StartMonitoring();

                this.Controls.Add(dxvu);
            };

            this.Controls.Add(dxvu);
            dxvu.Location = location;
            dxvu.Size = size;
            dxvu.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;
        }
    }
}
