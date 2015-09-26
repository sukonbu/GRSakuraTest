using System;
using Microsoft.SPOT.Hardware;
using System.Threading;

namespace GRsakuraTest
{
    public class Program
    {
        public static void Main()
        {
            const int SleepTime = 500; //const ‚ÍPascal
            
            //GR-SAKURA on board LED pin (LED1 0x50, LED2 0x51, LED3 0x52, LED4 0x56)
            Cpu.Pin[] ledPins = { (Cpu.Pin)0x50, (Cpu.Pin)0x51, (Cpu.Pin)0x52, (Cpu.Pin)0x56 };

            OutputPort[] leds = { new OutputPort(ledPins[0], false),
                                  new OutputPort(ledPins[1], false),
                                  new OutputPort(ledPins[2], false),
                                  new OutputPort(ledPins[3], false) };

            (new Thread(() =>
            {
                while (true)
                {
                    foreach(OutputPort led in leds)
                    {
                        led.Write(false);
                    }

                    foreach (OutputPort led in leds)
                    {
                        Thread.Sleep(SleepTime);
                        led.Write(true);
                    }
                    Thread.Sleep(SleepTime);
                }
            })).Start();
        }
    }
}
