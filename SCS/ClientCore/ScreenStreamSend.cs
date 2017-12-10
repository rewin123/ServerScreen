using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace ClientCore
{
    public class ScreenStreamSend : ScreenStream
    {
        UdpClient stream;
        public ScreenStreamSend(UdpClient stream) : base(stream)
        {
            this.stream = stream;
            
        }

        public override void Tick()
        {
            SendTask();
        }
        public void SendTask()
        {
            var map = new Bitmap( Screenshot.MakeScreenshot(),new Size(640,480));
            MemoryStream mem = new MemoryStream();
            map.Save(mem, ImageFormat.Png);

            mem.Position = 0;
            byte[] arr = new byte[mem.Length];
            mem.Read(arr, 0, arr.Length);
            
            stream.Send(arr, arr.Length, new IPEndPoint(IPAddress.Broadcast, 8996));
        }
    }
}
