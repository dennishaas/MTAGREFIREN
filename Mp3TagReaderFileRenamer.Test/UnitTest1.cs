using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mp3TagReaderFileRenamer.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void GetTracknoIncludingZero()
        {
            Program p = new Program();
            string expected = "05";
            string result = Program.GetTrackNoPrefixIncludingLeadingZero(5);
            Assert.AreEqual(expected, result);
        }
    }
}
