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

namespace ClientCore
{
    public class ScreenStreamGet : ScreenStream
    {
        delegate void ImageInvoke(Bitmap map);
        NetworkStream stream;
        PictureBox box;
        public ScreenStreamGet(NetworkStream stream, PictureBox box) : base(stream)
        {
            this.stream = stream;
            this.box = box;
            
        }

        public override void Tick()
        {
            GetImageTask();
        }

        void GetImageTask()
        {
            if (stream.DataAvailable)
            {
                BinaryFormatter form = new BinaryFormatter();
                long l = (long)form.Deserialize(stream);
                int length = (int)l;
                byte[] arr = new byte[length];
                int readed = 0;
                while (readed < length)
                {
                    while (!stream.DataAvailable)
                        ;
                    stream.Read(arr, readed, length - readed);
                }
                MemoryStream mem = new MemoryStream();
                //stream.Read(arr, 0, length);
                mem.Write(arr, 0, length);
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
