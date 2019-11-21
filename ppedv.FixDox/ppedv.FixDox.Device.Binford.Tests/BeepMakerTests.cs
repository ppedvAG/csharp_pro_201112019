using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ppedv.FixDox.Device.Binford.Tests
{
    [TestClass]
    public class BeepMakerTests
    {
        [TestMethod]
        public void BeepMaker_3000()
        {
            var bm = new BeepMaker8000();
            bm.MakeBeep(3000);
        }
    }
}
