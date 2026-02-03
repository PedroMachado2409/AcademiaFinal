using FluentAssertions;
using Moq;
using NexusGym.Application.Services.UseCases.Clientes.Commands;
using NexusGym.Domain.Abstractions.Clientes;
using NexusGym.Domain.Entities;
using NexusGym.Exceptions.Clientes;
using System;


namespace NexusGymTests.Application.Services.UseCases.Clientes.UseCases
{

    [TestClass]
    public class AtivarClienteUseCaseTests
    {
        private Mock<IClienteRepository> _repositoryMock = null!;
        private AtivarClienteUseCase _useCase = null!;

        [TestInitialize]
        public void Setup()
        {
            _repositoryMock = new Mock<IClienteRepository>();

            _useCase = new AtivarClienteUseCase(_repositoryMock.Object);
        }

        [TestMethod]
        public async Task Deve_Ativar_Cliente_Quando_Esta_Inativo()
        {
            var cliente = new Cliente(
            "Pedro Machado",
            "pedro@email.com",
            "12345678901",
            "21999999999"
            );

            cliente.Inativar();

            _repositoryMock.Setup(c => c.ObterClientePorId(It.IsAny<int>())).ReturnsAsync(cliente);


            var result = await _useCase.Execute(1);

            result.Should().BeTrue();
            cliente.Ativo.Should().BeTrue();

            _repositoryMock.Verify(c => c.ObterClientePorId(1), Times.Once); 
            _repositoryMock.Verify(c => c.AtualizarCliente(cliente), Times.Once);
            
        }

        [TestMethod]
        public async Task Deve_Lancar_Excecao_Quando_Estiver_Ativo()
        {
            var cliente = new Cliente(
              "Pedro Machado",
              "pedro@email.com",
              "12345678901",
              "21999999999"
             );

          

            _repositoryMock.Setup(c => c.ObterClientePorId(It.IsAny<int>())).ReturnsAsync(cliente);

            Func<Task> act = () => _useCase.Execute(1);

            var exception = await act
                    .Should()
                    .ThrowAsync<Exception>();

            exception.Which.Message
                .Should()
                .Be(ClientesExceptions.Cliente_JaAtivo);


            _repositoryMock.Verify(c => c.ObterClientePorId(1), Times.Once);
            _repositoryMock.Verify(c => c.AtualizarCliente(cliente), Times.Never);

        }
    }
}
