using ApiFiliado.Controllers;
using ApiFiliado.Models;
using ApiFiliado.Repositorio;
using FizzWare.NBuilder;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Xunit;

namespace ApiFiliado.Tests
{
    [ExcludeFromCodeCoverage]
    public class FiliadosControllerTests : IDisposable
    {
        private readonly FiliadosController _controller;
        private readonly Mock<IRepositorioFiliado> _mockIRepositorioFiliado;

        public FiliadosControllerTests()
        {
            _mockIRepositorioFiliado = new Mock<IRepositorioFiliado>();

            _controller = new FiliadosController(_mockIRepositorioFiliado.Object);
        }

        public void Dispose()
        {
            
        }

        [Fact]
        public void Get_Sucesso_RetornaListDeFiliados()
        {
            // Arrange
            var listaFiliados = Builder<Filiado>
                .CreateListOfSize(10)
                .Build()
                .AsQueryable();

            _mockIRepositorioFiliado.Setup(p => p.Filiados)
                .Returns(listaFiliados);

            // Act
            var result = _controller.Get();
            var okResult = result as OkObjectResult;
            var lista = okResult?.Value as List<Filiado>;

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(okResult);
            Assert.NotNull(lista);
            Assert.NotEmpty(lista);
        }

        [Fact]
        public void GetById_Sucesso_RetornaFiliado()
        {
            // Arrange
            var id = Guid.NewGuid();
            var listaFiliados = Builder<Filiado>
                .CreateListOfSize(10)
                .Random(1)
                    .Do(p => p.Id = id)
                .Build()
                .AsQueryable();

            _mockIRepositorioFiliado.Setup(p => p.Filiados)
                .Returns(listaFiliados);

            // Act
            var result = _controller.Get(id.ToString());
            var okResult = result as OkObjectResult;
            var filiado = okResult?.Value as Filiado;

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(okResult);
            Assert.NotNull(filiado);
            Assert.Equal(id, filiado.Id);
        }
    }
}
