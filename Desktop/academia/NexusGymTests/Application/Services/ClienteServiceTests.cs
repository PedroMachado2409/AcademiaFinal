using Xunit;
using Moq;
using AutoMapper;
using FluentAssertions;
using System.Threading.Tasks;
using NexusGym.Application.Services;
using NexusGym.Application.Dto;
using NexusGym.Domain;
using NexusGym.Infrastructure.Interface;
using Assert = Xunit.Assert;

namespace NexusGymTests.Application.Services
{
    public class ClienteServiceTests
    {
        private readonly Mock<IClienteRepository> _repoMock;
        private readonly IMapper _mapper;
        private readonly ClienteService _service;

        public ClienteServiceTests()
        {

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Cliente, ClienteDTO>().ReverseMap();
            });
            _mapper = config.CreateMapper();


            _repoMock = new Mock<IClienteRepository>();


            _service = new ClienteService(_repoMock.Object, _mapper);
        }

        [Fact]
        public async Task CadastrarCliente_DeveCadastrar_QuandoCpfNaoExistir()
        {

            var dto = new ClienteDTO
            {
                Nome = "Novo Cliente",
                Cpf = "12345678900"
            };


            _repoMock.Setup(r => r.ObterClientePorCpf(dto.Cpf))
                .ReturnsAsync((Cliente)null);

            // Configurar mock para simular a adição do cliente (retornando o cliente passado)
            _repoMock.Setup(r => r.Adicionar(It.IsAny<Cliente>()))
                .ReturnsAsync((Cliente c) => c);

            // Act (executar o método que será testado)
            var resultado = await _service.CadastrarCliente(dto);

            // Assert (verificar se o resultado é o esperado)
            resultado.Should().NotBeNull();
            resultado.Nome.Should().Be(dto.Nome);

            // Verificar se o método Adicionar foi chamado exatamente uma vez
            _repoMock.Verify(r => r.Adicionar(It.IsAny<Cliente>()), Times.Once);
        }

        [Fact]
        public async Task CadastrarCliente_DeveFalhar_QuandoCpfExistir()
        {
            // Arrange
            var dto = new ClienteDTO
            {
                Nome = "Cliente Existente",
                Cpf = "12345678900"
            };

            // Configura mock para simular que já existe um cliente com o CPF
            _repoMock.Setup(r => r.ObterClientePorCpf(dto.Cpf))
                .ReturnsAsync(new Cliente { Cpf = dto.Cpf });

            // Act & Assert: verifica se a exceção foi lançada
            await Assert.ThrowsAsync<Exception>(() => _service.CadastrarCliente(dto));
        }

        [Fact]
        public async Task ObterClientePorId_DeveRetornarCliente_QuandoExistir()
        {
            // Arrange
            var cliente = new Cliente { Id = 1, Nome = "Teste", Cpf = "12345678900" };
            _repoMock.Setup(r => r.ObterPorId(1)).ReturnsAsync(cliente);

            // Act
            var resultado = await _service.ObterClientePorId(1);

            // Assert
            resultado.Should().NotBeNull();
            resultado.Id.Should().Be(1);
        }

        [Fact]
        public async Task ObterClientePorId_DeveLancarExcecao_QuandoNaoExistir()
        {
            // Arrange
            _repoMock.Setup(r => r.ObterPorId(99)).ReturnsAsync((Cliente)null);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _service.ObterClientePorId(99));
        }

        [Fact]
        public async Task ListarClientes_DeveRetornarLista()
        {
            // Arrange
            var lista = new List<Cliente>
    {
        new Cliente { Id = 1, Nome = "Cliente A", Cpf = "11111111111" },
        new Cliente { Id = 2, Nome = "Cliente B", Cpf = "22222222222" }
    };

            _repoMock.Setup(r => r.Listar()).ReturnsAsync(lista);

            // Act
            var resultado = await _service.ListarClientes();

            // Assert
            resultado.Should().HaveCount(2);
        }

        [Fact]
        public async Task AtualizarCliente_DeveAtualizar_QuandoDadosValidos()
        {
            // Arrange
            var dto = new ClienteDTO { Id = 1, Nome = "Atualizado", Cpf = "12345678900" };
            var clienteExistente = new Cliente { Id = 1, Nome = "Antigo", Cpf = "12345678900" };

            _repoMock.Setup(r => r.ObterPorId(dto.Id)).ReturnsAsync(clienteExistente);
            _repoMock.Setup(r => r.ObterClientePorCpf(dto.Cpf)).ReturnsAsync(clienteExistente);

            // Act
            var resultado = await _service.AtualizarCliente(dto);

            // Assert
            resultado.Nome.Should().Be("Atualizado");
        }

        [Fact]
        public async Task AtualizarCliente_DeveLancarExcecao_QuandoCpfJaExistente()
        {
            // Arrange
            var dto = new ClienteDTO { Id = 1, Nome = "Teste", Cpf = "12345678900" };
            var clienteOriginal = new Cliente { Id = 1, Nome = "Teste Antigo", Cpf = "99999999999" };
            var outroClienteComMesmoCpf = new Cliente { Id = 2, Nome = "Outro", Cpf = "12345678900" };

            _repoMock.Setup(r => r.ObterPorId(dto.Id)).ReturnsAsync(clienteOriginal);
            _repoMock.Setup(r => r.ObterClientePorCpf(dto.Cpf)).ReturnsAsync(outroClienteComMesmoCpf);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _service.AtualizarCliente(dto));
        }

        [Fact]
        public async Task InativarCliente_DeveInativar_QuandoAtivo()
        {
            // Arrange
            var cliente = new Cliente { Id = 1, Ativo = true };
            _repoMock.Setup(r => r.ObterPorId(cliente.Id)).ReturnsAsync(cliente);

            // Act
            await _service.InativarCliente(cliente.Id);

            // Assert
            cliente.Ativo.Should().BeFalse();
            _repoMock.Verify(r => r.Atualizar(cliente), Times.Once);
        }

        [Fact]
        public async Task InativarCliente_DeveLancarExcecao_QuandoJaInativo()
        {
            // Arrange
            var cliente = new Cliente { Id = 1, Ativo = false };
            _repoMock.Setup(r => r.ObterPorId(cliente.Id)).ReturnsAsync(cliente);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _service.InativarCliente(cliente.Id));
        }

        [Fact]
        public async Task AtivarCliente_DeveAtivar_QuandoInativo()
        {
            // Arrange
            var cliente = new Cliente { Id = 1, Ativo = false };
            _repoMock.Setup(r => r.ObterPorId(cliente.Id)).ReturnsAsync(cliente);

            // Act
            await _service.AtivarCliente(cliente.Id);

            // Assert
            cliente.Ativo.Should().BeTrue();
            _repoMock.Verify(r => r.Atualizar(cliente), Times.Once);
        }

        [Fact]
        public async Task AtivarCliente_DeveLancarExcecao_QuandoJaAtivo()
        {
            // Arrange
            var cliente = new Cliente { Id = 1, Ativo = true };
            _repoMock.Setup(r => r.ObterPorId(cliente.Id)).ReturnsAsync(cliente);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _service.AtivarCliente(cliente.Id));
        }
    }
}
