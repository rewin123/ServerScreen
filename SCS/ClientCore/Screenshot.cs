using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientCore
{
    public static class Screenshot
    {
        public static Bitmap MakeScreenshot()
        {
            Graphics graph = null;

            var bmp = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);

            graph = Graphics.FromImage(bmp);

            graph.CopyFromScreen(0, 0, 0, 0, bmp.Size);

            graph.Flush();
            graph.Dispose();

            return bmp;
        }
    }
}
