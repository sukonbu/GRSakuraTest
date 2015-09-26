using Microsoft.SPOT.Hardware;
using System.Threading;

namespace GRsakuraTest
{
    public class Program
    {
        //GR-SAKURA (LED1 0x50, LED2 0x51, LED3 0x52, LED4 0x56)
        private const Cpu.Pin LED1Pin = (Cpu.Pin)0x50; //LED1
        private const Cpu.Pin LED2Pin = (Cpu.Pin)0x51; //LED2
        private const Cpu.Pin LED3Pin = (Cpu.Pin)0x52; //LED2
        private const Cpu.Pin LED4Pin = (Cpu.Pin)0x56; //LED4
        public static void Main()
        {
            var led1 = new OutputPort(LED1Pin, false);
            var led2 = new OutputPort(LED2Pin, false);
            var led3 = new OutputPort(LED3Pin, false);
            var led4 = new OutputPort(LED4Pin, false);
            (new Thread(() =>
            {
                while (true)
                {
                    led1.Write(false);
                    led2.Write(false);
                    led3.Write(false);
                    led4.Write(false);
                    Thread.Sleep(1000);
                    led1.Write(true);
                    Thread.Sleep(1000);
                    led2.Write(true);
                    Thread.Sleep(1000);
                    led3.Write(true);
                    Thread.Sleep(1000);
                    led4.Write(true);
                    Thread.Sleep(1000);
                }
            })).Start();
        }
    }
}
