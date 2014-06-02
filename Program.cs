using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Leap;

namespace LeapConsoleMouse
{
    class Program
    {
        static void Main( string[] args )
        {
            var controller = new Controller();
            var listener = new LeapListener();
            controller.AddListener( listener );

            Console.WriteLine( "Press Enter to quit..." );
            Console.ReadLine();

            controller.RemoveListener( listener );
            controller.Dispose();
        }
    }
}
