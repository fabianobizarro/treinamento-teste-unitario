using System;
using Microsoft.QualityTools.Testing.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AppConsole.Tests
{
    [TestClass]
    public class ProgramTests
    {
        private IDisposable _shimsContext;

        [TestInitialize]
        public void Initialize()
        {
            _shimsContext = ShimsContext.Create();
        }

        [TestCleanup]
        public void Cleanup()
        {
            _shimsContext.Dispose();
        }

        [TestMethod]
        public void Main_ComSucesso_ExecutarComoExperado()
        {
            var args = new string[] { };

            Program.Main(args);
        }
    }
}
