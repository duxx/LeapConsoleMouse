using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows;

namespace LeapConsoleMouse
{
    class MouseCursor
    {
        [DllImport( "user32.dll" )]
        private static extern bool SetCursorPos( int x, int y );

        public static void MoveCursor(int x, int y)
        {
            SetCursorPos( x, y );
        }
    }
}
