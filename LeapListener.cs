using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Leap;
using SharpOSC;

namespace LeapConsoleMouse
{
    class LeapListener : Listener
    {
        public override void OnInit( Controller controller )
        {
            Console.WriteLine( "Initialized" );
        }

        public override void OnConnect( Controller controller )
        {
            Console.WriteLine( "Connected" );
        }

        public override void OnDisconnect( Controller controller )
        {
            Console.WriteLine( "Disconnected" );
        }

        public override void OnExit( Controller controller )
        {
            Console.WriteLine( "Exited" );
        }

        private long currentTime;
        private long previousTime;
        private long timeChanged;

        public override void OnFrame( Controller controller )
        {
            Frame currentFrame = controller.Frame();

            currentTime = currentFrame.Timestamp;
            timeChanged = currentTime - previousTime;

            if( timeChanged > 1000 )
            {
                if ( !currentFrame.Hands.IsEmpty )
                {
                    Finger finger = currentFrame.Fingers[0];

                    Screen screen = controller.LocatedScreens.ClosestScreenHit( finger );

                    if ( screen != null && screen.IsValid )
                    {
                        var tipVelocity = ( int ) finger.TipVelocity.Magnitude;

                        //if ( tipVelocity > 25 )
                        //{
                            var xScreenIntersect = screen.Intersect( finger, true ).x;
                            var yScreenIntersect = screen.Intersect( finger, true ).y;

                            if ( xScreenIntersect.ToString() != "NaN" )
                            {
                                var x = ( int ) ( xScreenIntersect * screen.WidthPixels );
                                var y = ( int ) ( screen.HeightPixels - ( yScreenIntersect * screen.HeightPixels ) );

                                Console.WriteLine( "Screen intersect X: " + xScreenIntersect.ToString() );
                                Console.WriteLine( "Screen intersect Y: " + yScreenIntersect.ToString() );
                                Console.WriteLine( "Width pixels: " + screen.WidthPixels.ToString() );
                                Console.WriteLine( "Height pixels: " + screen.HeightPixels.ToString() );

                                Console.WriteLine( "\n" );

                                Console.WriteLine( "x: " + x.ToString() );
                                Console.WriteLine( "y: " + y.ToString() );

                                Console.WriteLine( "\n" );

                                Console.WriteLine( "Tip velocity: " + tipVelocity.ToString() );

                                var message = new SharpOSC.OscMessage( "/mouse", x, y, tipVelocity );
                                var sender = new SharpOSC.UDPSender( "127.0.0.1", 55555 );
                                sender.Send( message );
                                //MouseCursor.MoveCursor( x, y );
                                Console.WriteLine( "\n" + new String( '=', 40 ) + "\n" );
                            }
                        //}
                    }
                }
            }

            previousTime = currentTime;
        }
    }
}
