using System;

namespace ppedv.FixDox.Device.Binford
{
    public class BeepMaker8000
    {
        public void MakeBeep(int power)
        {
            Console.Beep(300, power);
        }
    }
}
