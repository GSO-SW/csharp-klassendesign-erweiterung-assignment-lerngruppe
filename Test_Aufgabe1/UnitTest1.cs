using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Test_Task1
{
    [TestClass]
    public class UnitTest1
    {

        [TestMethod, TestCategory("Task1")]
        [TestCategory("InOut")]
        [TestProperty("GSO-DevGroup", "Kander")]
        [DataRow("12345", "1", 100.0, "2", 50.0, 50)]
        [DataRow("12345", "2", 100.0, "1", 50.0, -50)]


        public void Test_Deposit(string accountNumber, string option1, double amount1, string option2, double amount2, double result)
        {
            Environment.SetEnvironmentVariable("IsTesting", "true");

            // Arrange
            var writer = new StringWriter();
            Console.SetOut(writer);

            var textReader = new StringReader(@$"{accountNumber}
{option1}
{amount1}
{option2}
{amount2}
3
r\
4
r\");
            Console.SetIn(textReader);

            // Act
            Aufgabe_1.Aufgabe1();

            // Assert
            List<string> lines_list_check = new List<string> { $"Ihr Kontostand:{result}EUR" };
            AssertTest(writer, lines_list_check);

            Environment.SetEnvironmentVariable("IsTesting", null);
        }
        public static void AssertTest(StringWriter writer, List<string> lines_list_check)
        {

            // Assert

            var sb = writer.GetStringBuilder();
            var lines = sb.ToString().Split(new[] { "\r\n", "\n" }, StringSplitOptions.TrimEntries);

            List<string> lines_list = new List<string>();

            //Bedingung nötig da 'Enviroment.NewLine' in Git Actions nicht funktioniert.
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i] != "")
                {
                    lines_list.Add(lines[i]);
                    Debug.WriteLine($"{lines[i]}");
                }
            }





            lines_list = lines_list.Intersect(lines_list_check).ToList();



            for (int i = 0; i < lines_list_check.Count; i++)
            {

                try
                {
                    if (lines_list[i] != lines_list_check[i]) Trace.WriteLine($"\nFehler: '{lines_list_check[i]}' nicht gefunden");
                    Assert.AreEqual(lines_list[i], lines_list_check[i]);
                }
                catch
                {
                    Trace.WriteLine($"\n\n");
                    Trace.WriteLine($"Fehler: Zeile fehlt");
                    Trace.WriteLine($"---------------------");
                    Trace.WriteLine($"{lines_list_check[i]}");
                    Trace.WriteLine($"---------------------");
                    Assert.Fail(); ;

                }

            }
        }
    }
}
