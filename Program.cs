using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Flow
{
    public delegate void OnKey(ConsoleKeyInfo key);

    class Program
    {
        static void Main(string[] args)
        {
            Flow manager = new Flow();
            manager.Explorer();
        }
    }
}