using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ClientCore
{
    public abstract class ScreenStream
    {
        public ScreenStream(NetworkStream stream)
        {

        }

        public virtual void Tick()
        {
            throw new NotImplementedException("Вызван Tick абстрактного класса ScreenStream");
        }
    }
}
