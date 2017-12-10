using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;

namespace ClientCore
{
    public class ScreenStreamGet : ScreenStream
    {
        delegate void ImageInvoke(Bitmap map);
        UdpClient stream;
        PictureBox box;
        IPEndPoint point;
        public ScreenStreamGet(UdpClient stream, PictureBox box, ref IPEndPoint point) : base(stream)
        {
            this.stream = stream;
            this.box = box;
            this.point = point;
        }

        public override void Tick()
        {
            GetImageTask();
        }

        void GetImageTask()
        {
            if (stream.Available > 0)
            {
                BinaryFormatter form = new BinaryFormatter();
                //long l = (long)form.Deserialize(stream);
                //int length = (int)l;
                byte[] arr = stream.Receive(ref point);
                //int readed = 0;
                //while (readed < length)
                //{
                //    while (!stream.DataAvailable)
                //        ;
                //    readed += stream.Read(arr, readed, length - readed);
                //}
                MemoryStream mem = new MemoryStream();
                //stream.Read(arr, 0, length);
                mem.Write(arr, 0, arr.Length);
                //Bitmap map = (Bitmap)form.Deserialize(stream);
                var map = new Bitmap(mem);
                box.Invoke(new ImageInvoke(SetImg), map);
            }
        }

        void SetImg(Bitmap map)
        {
            box.Image = map;
        }
    }
}
