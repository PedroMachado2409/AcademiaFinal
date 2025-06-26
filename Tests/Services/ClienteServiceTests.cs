using AutoMapper;
using Moq;
using NexusGym.Application.Dto;
using NexusGym.Application.Services;
using NexusGym.Domain;
using NexusGym.Exceptions;
using NexusGym.Infrastructure.Repositories;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Xunit;

namespace NexusGym.Tests.Services
{
    public class ClienteServiceTests
    {
        private readonly Mock<ClienteRepository> _repositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly ClienteService _clienteService;

        public ClienteServiceTests()
        {
            _repositoryMock = new Mock<ClienteRepository>();
            _mapperMock = new Mock<IMapper>();
            _clienteService = new ClienteService(_repositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task CadastrarCliente_DeveLancarExcecao_QuandoCpfJaExiste()
        {
            // Arrange
            var clienteDto = new ClienteDTO { Nome = "Maria", Cpf = "99999999999" };
            var clienteExistente = new Cliente { Id = 2, Nome = "Outro", Cpf = "99999999999" };

            _mapperMock.Setup(m => m.Map<Cliente>(clienteDto)).Returns(new Cliente());
            _repositoryMock.Setup(r => r.ObterClientePorCpf(clienteDto.Cpf)).ReturnsAsync(clienteExistente);

            // Act & Assert
            var excecao = await Assert.ThrowsAsync<Exception>(() => _clienteService.CadastrarCliente(clienteDto));
            Assert.Equal(ExceptionsMessage.CpfExiste, excecao.Message);
        }
    }
}
