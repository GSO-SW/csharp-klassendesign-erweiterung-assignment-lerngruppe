using System.Diagnostics;


namespace Test_Aufgabe5
{
    [TestClass]
    public class BankkontoTests
    {
        [TestMethod, TestCategory("Task5")]
        [TestCategory("InOut")]
        [TestProperty("GSO-DevGroup", "Kander")]
        public void TestEinzahlung()
        {
            var konto = new Bankkonto();
            konto.Einzahlung(100);
            Assert.AreEqual(100, konto.kontostand);
        }

        [TestMethod]
        public void TestAuszahlung()
        {
            var konto = new Bankkonto();
            konto.Einzahlung(200);
            konto.Auszahlung(50);
            Assert.AreEqual(150, konto.kontostand);
        }

        [TestMethod]
        public void TestKontonummerGültig()
        {
            var konto = new Bankkonto();
            konto.kontonummer = 12345;
            Assert.AreEqual(12345, konto.kontonummer);
        }

        [TestMethod]
        public void TestKontonummerUngültig()
        {
            var konto = new Bankkonto();

            // Um den Konsolenausdruck in einer Zeichenfolge zu speichern
            using (var consoleOutput = new System.IO.StringWriter())
            {
                Console.SetOut(consoleOutput);
                konto.kontonummer = -1;
                Assert.AreEqual("-----------------------\r\nFehler: Negativer Wert!\r\n", consoleOutput.ToString());
            }

            // �berpr�fen, ob die Kontonummer unver�ndert bleibt
            Assert.AreEqual(0, konto.kontonummer);
        }
    }
}