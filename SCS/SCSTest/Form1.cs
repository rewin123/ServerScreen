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
        ScreenStream screen;
        int port = 8996;
        public Form1()
        {
            InitializeComponent();
        }


        private void Button1_Click_1(object sender, EventArgs e)
        {
            TcpListener list = new TcpListener(port);
            list.Start();
            var client = list.AcceptTcpClient();
            screen = new ScreenStreamSend(client.GetStream());

            timer1.Start();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            screen.Tick();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            TcpClient client = new TcpClient(textBox1.Text, port);
            screen = new ScreenStreamGet(client.GetStream(),pictureBox1);

            timer1.Start();
        }
    }
}
