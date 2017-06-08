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
        public void ValidateRunOutputWith3Lifes()
        {
            string expected = "['up', 'up', 'left']\r\n\r\n";

            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                Run.MaxLives = 3;
                Run.Main(new[] {"input//mazes.txt"});

                Assert.AreEqual<string>(expected, sw.ToString());
            }
        }

        [TestMethod]
        public void ValidateRunOutputWith1Lifes()
        {
            string expected = "['right', 'up', 'up', 'left', 'left']\r\n\r\n";

            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                Run.MaxLives = 1;
                Run.Main(new[] { "input//mazes.txt" });

                Assert.AreEqual<string>(expected, sw.ToString());
            }
        }
    }
}
