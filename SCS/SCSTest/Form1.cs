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
            if(wait_tasks.Count < max_length)
            {
                wait_tasks.Add(Task.Run(new Func<Bitmap>(AsyncGetImage)));
            }

            if (wait_tasks[0].Status == TaskStatus.RanToCompletion)
            {
                pictureBox1.Image = wait_tasks[0].Result;
                wait_tasks.RemoveAt(0);
                wait_tasks.Add(Task.Run(new Func<Bitmap>(AsyncGetImage)));
            }
            else if(wait_tasks[0].Status == TaskStatus.Faulted)
            {
                wait_tasks.RemoveAt(0);
            }
            else if(wait_tasks.Count > 1)
            {
                if(wait_tasks[1].IsCompleted)
                {
                    wait_tasks.RemoveAt(0);
                    pictureBox1.Image = wait_tasks[0].Result;
                    wait_tasks.RemoveAt(0);
                    wait_tasks.Add(Task.Run(new Func<Bitmap>(AsyncGetImage)));
                }
            }

            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RemotingConfiguration.RegisterWellKnownClientType(typeof(NetworkScreen), "tcp://" + textBox1.Text + ":" + port + "/NetworkScreen.rem");
            screen = new NetworkScreen();

            wait_tasks.Add(Task.Run(new Func<Bitmap>(AsyncGetImage)));

            timer1.Start();
        }

        Bitmap AsyncGetImage()
        {
            
            MemoryStream mem = new MemoryStream();
            byte[] arr = screen.GetScreen();
            mem.Write(arr, 0, arr.Length);
            mem.Position = 0;
            return new Bitmap(mem);
        }

        int max_length = 10;
        List< Task<Bitmap>> wait_tasks = new List<Task<Bitmap>>();
    }
}
