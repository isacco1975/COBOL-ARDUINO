using System;
using System.IO.Ports;

namespace IGP_SimpleSerial
{
    class Program
    {
        static void Main(string[] args)
        {
            string comPortName = args[0];
            int comPortRate = Convert.ToInt32(args[1].Trim());
            int comPortBits = Convert.ToInt32(args[2].Trim());            
            string comPortMessageToSend = args[3];
            string optionalArgument = args[4];

            SerialPort igpSerial = new SerialPort(comPortName, comPortRate, Parity.None, comPortBits, StopBits.One);

            igpSerial.Open();
            igpSerial.Write(comPortMessageToSend); 
            igpSerial.Write(";"); 
            igpSerial.Write(optionalArgument.ToString()); 
            igpSerial.Close();
        }
    }
}
