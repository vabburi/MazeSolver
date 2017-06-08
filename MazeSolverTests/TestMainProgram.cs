using System;
using System.IO;
using MazeSolver;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MazeSolverTests
{
    [TestClass]
    public class TestMainProgram
    {
        [TestMethod]
        public void ValidateRunOutput()
        {
            string expected = "['up', 'up', 'left']\r\n\r\n";

            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                Run.Main(new[] {"input//mazes.txt"});

                Assert.AreEqual<string>(expected, sw.ToString());
            }
        }
    }
}
