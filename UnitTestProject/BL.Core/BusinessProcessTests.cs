using Microsoft.VisualStudio.TestTools.UnitTesting;
using GCM.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GCM.BL.Core;

namespace GCM.Tests.BL
{
    [TestClass()]
    public class BusinessProcessTests
    {
        private IBusinessProcess _BusinessProcess;

        [TestMethod()]
        public void PrintOutputTestMorningValidOrdered()
        {
            string input = "morning, 1, 2, 3";
            string expectedOutput = "eggs, toast, coffee";

            this._BusinessProcess = new BusinessProcess(input);
            string output = this._BusinessProcess.CreateOutput();

            Assert.AreEqual(expectedOutput, output, true, "Testing morning with ordered right data fails.");
        }

        [TestMethod()]
        public void PrintOutputTestMorningValid()
        {
            string input = "morning, 2, 1, 3";
            string expectedOutput = "eggs, toast, coffee";

            this._BusinessProcess = new BusinessProcess(input);
            string output = this._BusinessProcess.CreateOutput();

            Assert.AreEqual(expectedOutput, output, true, "Testing morning with right data fails.");
        }

        [TestMethod()]
        public void PrintOutputTestMorningInvalid()
        {
            string input = "morning, 1, 2, 3, 4";
            string expectedOutput = "eggs, toast, coffee, error";

            this._BusinessProcess = new BusinessProcess(input);
            string output = this._BusinessProcess.CreateOutput();

            Assert.AreEqual(expectedOutput, output, true, "Testing morning with invalid data fails.");
        }

        [TestMethod()]
        public void PrintOutputTestMorningRepeat()
        {
            string input = "morning, 1, 2, 3, 3, 3";
            string expectedOutput = "eggs, toast, coffee(x3)";

            this._BusinessProcess = new BusinessProcess(input);
            string output = this._BusinessProcess.CreateOutput();

            Assert.AreEqual(expectedOutput, output, true, "Testing morning with repeated data fails.");
        }

        [TestMethod()]
        public void PrintOutputTestNightValidOrdered()
        {
            string input = "night, 1, 2, 3, 4";
            this._BusinessProcess = new BusinessProcess(input);
            string output = this._BusinessProcess.CreateOutput();
            string expectedOutput = "steak, potato, wine, cake";

            Assert.AreEqual(expectedOutput, output, true, "Testing night with ordered right data fails.");
        }

        [TestMethod()]
        public void PrintOutputTestNightRepeat()
        {
            string input = "night, 1, 2, 2, 4";
            string expectedOutput = "steak, potato(x2), cake";

            this._BusinessProcess = new BusinessProcess(input);
            string output = this._BusinessProcess.CreateOutput();

            Assert.AreEqual(expectedOutput, output, true, "Testing night with repeat data fails.");
        }

        [TestMethod()]
        public void PrintOutputTestNightInvalid()
        {
            string input = "night, 1, 2, 3, 5";
            string expectedOutput = "steak, potato, wine, error";

            this._BusinessProcess = new BusinessProcess(input);
            string output = this._BusinessProcess.CreateOutput();

            Assert.AreEqual(expectedOutput, output, true, "Testing night with invalid data fails.");
        }

        [TestMethod()]
        public void PrintOutputTestNightRepeatedInvalid()
        {
            string input = "night, 1, 1, 2, 3, 5";
            string expectedOutput = "steak, error";

            this._BusinessProcess = new BusinessProcess(input);
            string output = this._BusinessProcess.CreateOutput();

            Assert.AreEqual(expectedOutput, output, true, "Testing night with invalid repeated data fails.");
        }

    }
}