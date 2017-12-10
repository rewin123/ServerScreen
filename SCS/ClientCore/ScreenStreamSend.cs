using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace ClientCore
{
    public class ScreenStreamSend : ScreenStream
    {
        NetworkStream stream;
        public ScreenStreamSend(NetworkStream stream) : base(stream)
        {
            this.stream = stream;
        }

        public override void Tick()
        {
            SendTask();
        }

        public void SendTask()
        {
            var map = Screenshot.MakeScreenshot();
            MemoryStream mem = new MemoryStream();
            map.Save(mem, ImageFormat.Png);

            BinaryFormatter form = new BinaryFormatter();
            mem.Position = 0;
            byte[] arr = new byte[mem.Length];
            mem.Read(arr, 0, arr.Length);
            
            form.Serialize(stream, mem.Length);
            stream.Write(arr, 0, arr.Length);
        }
    }
}
