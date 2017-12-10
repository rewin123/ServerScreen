using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientCore
{
    public class NetworkScreen : MarshalByRefObject
    {
        public byte[] GetScreen()
        {
            MemoryStream mem = new MemoryStream();
            Screenshot.MakeScreenshot().Save(mem,ImageFormat.Png);
            byte[] arr = new byte[mem.Length];
            mem.Position = 0;
            mem.Read(arr, 0, (int)mem.Length);
            return arr;
        }
    }
}
