using AppConsole.Fakes;
using Microsoft.QualityTools.Testing.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO.Fakes;
using System.Linq;

namespace AppConsole.Tests
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class ProcessoCobrancaTests
    {
        private IDisposable _shimscontext;
        private ProcessoCobranca _processoCobranca;

        [TestInitialize]
        public void Initialize()
        {
            _shimscontext = ShimsContext.Create();
            _processoCobranca = new ProcessoCobranca();
        }

        [TestCleanup]
        public void Cleanup()
        {
            _shimscontext.Dispose();
        }

        [TestMethod]
        public void Executar_Deve_ChamarMetodos()
        {
            // Arrange
            var lerArquivoCobrancaFoiChamado = false;
            var enviarEmailCobrancaFoiChamado = false;
            ShimProcessoCobranca.AllInstances.LerArquivoCobranca = _ =>
            {
                lerArquivoCobrancaFoiChamado = true;
            };
            ShimProcessoCobranca.AllInstances.EnviarEmailCobranca = _ =>
            {
                enviarEmailCobrancaFoiChamado = true;
            };

            // Act
            _processoCobranca.Executar();

            // Assert
            Assert.AreEqual(true, lerArquivoCobrancaFoiChamado);
            Assert.AreEqual(true, enviarEmailCobrancaFoiChamado);
        }

        [TestMethod]
        public void LerArquivoCobranca_ArquivoEncontrado_CarregarDados()
        {
            // Arrange
            ShimFile.ReadAllLinesString = (path) => new string[]
            {
                "fabiano",
                "flaviane",
                "albertt"
            };

            // Act
            _processoCobranca.LerArquivoCobranca();

            // Assert
            Assert.IsNotNull(_processoCobranca.Filiados);
            Assert.AreNotEqual(0, _processoCobranca.Filiados.Count());
            Assert.AreEqual(3, _processoCobranca.Filiados.Count());
        }

        [TestMethod]
        public void CarregarFiliadosDoBanco_ComListaFiliados_DeveCarregarFiliados()
        {
            // Arrange
            ShimProcessoCobranca.AllInstances._obterRepositorio = (_) => new StubIRepositorioFiliado
            {
                ObterPessoas = () => new List<Filiado>
                {
                    new Filiado
                    {
                        Idade = 25,
                        Nome = "fabiano"
                    },
                    new Filiado
                    {
                        Idade = 30,
                        Nome = "rennan"
                    },
                    new Filiado
                    {
                        Idade = 18,
                        Nome = "flaviane"
                    }
                }
            };

            // Act
            _processoCobranca.CarregarFiliadosDoBanco();

            // Assert
            Assert.IsNotNull(_processoCobranca.Filiados);
            Assert.AreNotEqual(0, _processoCobranca.Filiados.Count());
            Assert.AreEqual(3, _processoCobranca.Filiados.Count());
        }

        [TestMethod]
        public void CarregarFiliadosDoBanco_ComListaVazia_DeveCarregarFiliados()
        {
            // Arrange
            ShimProcessoCobranca.AllInstances._obterRepositorio = (_) => new StubIRepositorioFiliado
            {
                ObterPessoas = () => new List<Filiado>()
            };

            // Act
            _processoCobranca.CarregarFiliadosDoBanco();

            // Assert
            Assert.IsNotNull(_processoCobranca.Filiados);
            Assert.AreEqual(0, _processoCobranca.Filiados.Count());
        }

        [TestMethod]
        public void CarregarFiliadosDoBanco_ComValorNulo_DeveCarregarFiliados()
        {
            // Arrange
            ShimProcessoCobranca.AllInstances._obterRepositorio = (_) => new StubIRepositorioFiliado
            {
                ObterPessoas = () => null
            };

            // Act
            _processoCobranca.CarregarFiliadosDoBanco();

            // Assert
            Assert.IsNotNull(_processoCobranca.Filiados);
            Assert.AreEqual(0, _processoCobranca.Filiados.Count());
        }

        [TestMethod]
        public void EnviarEmailCobranca_ComListaDeFiliados_DeveChamarMetodoEmail()
        {
            // Arrange
            var emailEnviado = false;
            ShimServicoEmail.AllInstances.EnviarEmailString = (_, valor) =>
            {
                emailEnviado = true;
            };
            ShimProcessoCobranca.AllInstances.FiliadosGet = _ => new string[] { "fabiano" };

            // Act
            _processoCobranca.EnviarEmailCobranca();

            // Assert
            Assert.AreEqual(true, emailEnviado);
        }

        [TestMethod]
        public void EnviarEmailCobranca_ComListaVazia_NaoDeveChamarMetodoEmail()
        {
            // Arrange
            var emailEnviado = false;
            ShimServicoEmail.AllInstances.EnviarEmailString = (_, valor) =>
            {
                emailEnviado = true;
            };
            ShimProcessoCobranca.AllInstances.FiliadosGet = _ => new string[] { };

            // Act
            _processoCobranca.EnviarEmailCobranca();

            // Assert
            Assert.AreEqual(false, emailEnviado);
        }

    }
}
