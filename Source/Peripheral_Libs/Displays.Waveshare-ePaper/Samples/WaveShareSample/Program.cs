using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using System.Threading;
using SecretLabs.NETMF.Hardware.Netduino;
using Netduino.Foundation;
using Netduino.Foundation.Displays;
using Netduino.Foundation.Sensors.Buttons;
using Netduino.Foundation.LEDs;

namespace WaveShareEPaper
{
    public class Program
    {
        static EPDColorBase ePaper;
        static GraphicsLibrary display;
        static PushButton button;
        static Led led;

        private static void DrawLogo ()
        {
            var bottom = 265;
            var mHeight = 54;

            //mountain fill
            int lineWidth, x, y;

            for (int i = 1; i < mHeight - 1; i++)
            {
                y = bottom - i;
                x = 4 + 1 + i * 20 / 27;

                //fill bottom of mountain
                if (i < mHeight / 2)
                {
                    lineWidth = 38;
                    display.DrawLine(x, y, x + lineWidth, y, Color.Red);
                }
                //fill top of mountain
                else
                {
                    lineWidth = 38 - (i - mHeight / 2) * 40 / 27;
                    display.DrawLine(x, y, x + lineWidth, y, Color.Red);
                }
            }

            //mountains
            display.DrawLine(4, bottom, 44, bottom - mHeight, true);
            display.DrawLine(4, bottom, 44, bottom, true);
            display.DrawLine(44, bottom - mHeight, 64, bottom - mHeight / 2, true);

            display.DrawLine(44, bottom, 84, bottom - mHeight, true);
            display.DrawLine(84, bottom - mHeight, 124, bottom, true);

            // trees
            int heightTree = 7;
            int yStart, yEnd;
            for (int i = 0; i < 3; i++)
            {
                yStart = bottom - i * heightTree;
                yEnd = bottom - (i + 1) * heightTree;

                display.DrawLine(61, yStart, 68, yEnd, Color.Red);
                display.DrawLine(68, yEnd, 75, yStart, Color.Red);

                display.DrawLine(77, yStart, 84, yEnd, Color.Red);
                display.DrawLine(84, yEnd, 91, yStart, Color.Red);

                display.DrawLine(93, yStart, 100, yEnd, Color.Red);
                display.DrawLine(100, yEnd, 107, yStart, Color.Red);
            }

       //     display.Show();
        }

        public static void Main()
        {
            ePaper = new EPD2i9b(chipSelectPin: Pins.GPIO_PIN_D4,
                dcPin: Pins.GPIO_PIN_D7,
                resetPin: Pins.GPIO_PIN_D6,
                busyPin: Pins.GPIO_PIN_D5,
                spiModule: SPI.SPI_module.SPI1,
                speedKHz: 4000);
            
            ePaper.Clear(false, true);

            display = new GraphicsLibrary(ePaper);

            DrawLogo();

            display.DrawLine(10, 50, 118, 170);
         //   display.Show();
         //   Thread.Sleep(500);

            display.DrawCircle(52, 80, 32);
          //  display.Show();
         //   Thread.Sleep(500);

            display.DrawRectangle(5, 60, 60, 60);
          //  display.Show();
          //  Thread.Sleep(1000);

            display.DrawRectangle(20, 115, 40, 25, Color.Red, true);
         //   display.Show();
        //    Thread.Sleep(500);

            display.CurrentFont = new Font8x12();
            display.DrawText(4, 4, "Wilderness Labs", Color.Red);
          //  display.CurrentFont = new Font8x8();
            display.DrawText(40, 20, "Meadow");

            DrawLogo();

            display.Show();

            // tft.ClearScreen(31);
            //    ePaper.Refresh();

            /*  display = new GraphicsLibrary(tft);

              led = new Led(Pins.ONBOARD_LED);
              button = new PushButton(Pins.ONBOARD_BTN, CircuitTerminationType.CommonGround);
              button.Clicked += Button_Clicked;

              UITest();
              Thread.Sleep(-1);*/
        }

        private static void Button_Clicked(object sender, EventArgs e)
        {
            led.IsOn = !led.IsOn;
            display.DrawText(4, 145, ("LED is: " + (led.IsOn ? "On ":"Off")));
            display.Show();
        }

        static void UITest()
        {
            display.Clear();

            display.DrawLine(10, 10, 118, 150, Color.OrangeRed);
            display.Show();
       //     Thread.Sleep(500);
            display.DrawLine(118, 10, 10, 150, Color.OrangeRed);
            display.Show();
         //   Thread.Sleep(500);

            display.DrawCircle(64, 64, 25, Color.Purple);
            display.Show();
        //    Thread.Sleep(1000);

            display.DrawRectangle(5, 5, 118, 150, Color.Aquamarine);
            display.Show();
       //     Thread.Sleep(1000);

            display.DrawRectangle(10, 125, 108, 25, Color.Yellow, true);
            display.Show();
        //    Thread.Sleep(1000);

            display.CurrentFont = new Font8x8();
            display.DrawText(4, 10, "NETDUINO 3 WiFi", Color.SkyBlue);
            display.Show();
            Thread.Sleep(Timeout.Infinite);
        }
    }
}

namespace System.Diagnostics
{
    public enum DebuggerBrowsableState
    {
        Never = 0,
        Collapsed = 2,
        RootHidden = 3
    }
}