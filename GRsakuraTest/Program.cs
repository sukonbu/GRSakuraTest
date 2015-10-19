using System;
using Microsoft.SPOT.Hardware;
using System.Threading;

namespace GRsakuraTest
{
    public class Program
    {
        private const Cpu.Pin ButtonPin = (Cpu.Pin)0x57;
        private const Cpu.Pin UserPin = (Cpu.Pin)0x56;

        private static OutputPort _led;


        public static void Main()
        {
            //GR-SAKURA on board LED pin (LED1 0x50, LED2 0x51, LED3 0x52, LED4 0x56)
            Cpu.Pin[] ledPins = { (Cpu.Pin)0x50, (Cpu.Pin)0x51, (Cpu.Pin)0x52, (Cpu.Pin)0x56 };

            OutputPort[] leds = { new OutputPort(ledPins[0], false),
                                  new OutputPort(ledPins[1], false),
                                  new OutputPort(ledPins[2], false),
                                  new OutputPort(ledPins[3], false) };
            var button = new InterruptPort(ButtonPin, true, Port.ResistorMode.Disabled, Port.InterruptMode.InterruptEdgeBoth);
            button.OnInterrupt += button_OnInterruput;
            _led = new OutputPort(UserPin, false);
            (new Thread(() =>
            {
                Thread.Sleep(Timeout.Infinite);
            })).Start();
        }

        static void button_OnInterruput(uint data1,uint data2,DateTime time)
        {
            var isPressed = data2 == 0;
            _led.Write(isPressed);
        }

        static void LedBlink(OutputPort[] leds)
        {
            int SleepTime = 500; //const ‚ÍPascal
            (new Thread(() =>
            {
                while (true)
                {
                    foreach (OutputPort led in leds)
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

        static void inputButton(OutputPort[] leds)
        {
            var button = new InputPort(ButtonPin, true, Port.ResistorMode.Disabled);
            var led = new OutputPort(UserPin, false);
            (new Thread(() =>
            {
                while (true)
                {
                    var isPressed = !button.Read();

                    led.Write(isPressed);
                    Thread.Sleep(100);
                }
            })).Start();
        }
    }
}
