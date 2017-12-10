using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using ClientCore;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.IO;

namespace SCSTest
{
    public partial class Form1 : Form
    {
        NetworkScreen screen;
        int port = 8996;
        public Form1()
        {
            InitializeComponent();
        }
        

        private void Button1_Click_1(object sender, EventArgs e)
        {
            ChannelServices.RegisterChannel(new TcpChannel(port),false);
            RemotingConfiguration.RegisterWellKnownServiceType(typeof(NetworkScreen), "NetworkScreen.rem", WellKnownObjectMode.SingleCall);
            screen = new NetworkScreen();

            //timer1.Start();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            MemoryStream mem = new MemoryStream();
            byte[] arr = screen.GetScreen();
            mem.Write(arr, 0, arr.Length);
            mem.Position = 0;
            pictureBox1.Image = new Bitmap(mem);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RemotingConfiguration.RegisterWellKnownClientType(typeof(NetworkScreen), "tcp://localhost:" + port + "/NetworkScreen.rem");
            screen = new NetworkScreen();

            timer1.Start();
        }
    }
}
