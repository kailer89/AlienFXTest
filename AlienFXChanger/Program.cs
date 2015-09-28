using AlienFXTest.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AlienFXChanger
{
    class Program
    {
        static void Main(string[] args)
        {
            var t = DateTime.Now;

            using (var w = new Wardevil()) 
            {
                w.ChangeToSolidColor(Color.Red);
                Thread.Sleep(1000);
            }
        }
    }
}
