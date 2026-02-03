using AutoMapper;
using FluentAssertions;
using Moq;
using NexusGym.Application.Dto.Cliente;
using NexusGym.Application.Services.UseCases.Clientes.Commands;
using NexusGym.Application.Validators.Cliente;
using NexusGym.Domain.Abstractions.Clientes;
using NexusGym.Domain.Entities;
using NexusGym.Exceptions.Clientes;

namespace NexusGym.Tests.Clientes.UseCases
{
    [TestClass]
    public class CadastrarClienteUseCaseTests
    {
        private Mock<IClienteRepository> _repositoryMock = null!;
        private IMapper _mapper = null!;
        private CadastrarClienteUseCase _useCase = null!;

        [TestInitialize]
        public void Setup()
        {
            _repositoryMock = new Mock<IClienteRepository>();

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Cliente, ClienteResponseDTO>();
            });

            _mapper = mapperConfig.CreateMapper();

            _useCase = new CadastrarClienteUseCase(
                _repositoryMock.Object,
                _mapper
            );
        }

        [TestMethod]
        public async Task Deve_cadastrar_cliente_quando_dados_validos()
        {
        
            var dto = new ClienteCreateDTO
            {
                Nome = "Pedro Machado",
                Email = "pedro@email.com",
                Cpf = "12345678901",
                Telefone = "21999999999"
            };

            _repositoryMock
                .Setup(r => r.ObterClientePorCpf(dto.Cpf))
                .ReturnsAsync((Cliente?)null);

            _repositoryMock
                .Setup(r => r.AdicionarCliente(It.IsAny<Cliente>()))
                .Returns(Task.CompletedTask);

            
            var result = await _useCase.Execute(dto);

          
            result.Should().NotBeNull();
            result.Nome.Should().Be(dto.Nome);
            result.Email.Should().Be(dto.Email);
            result.Cpf.Should().Be(dto.Cpf);

            _repositoryMock.Verify(
                r => r.ObterClientePorCpf(dto.Cpf),
                Times.Once
            );

            _repositoryMock.Verify(
                r => r.AdicionarCliente(It.IsAny<Cliente>()),
                Times.Once
            );
        }


        [TestMethod]
        public async Task Execute_Deve_Lancar_Excecao_Quando_Cpf_Ja_Existir()
        {
            // Arrange
            var dto = new ClienteCreateDTO
            {
                Nome = "Pedro Machado",
                Email = "pedro@email.com",
                Cpf = "12345678910",
                Telefone = "21999999999"
            };

            _repositoryMock
                .Setup(r => r.ObterClientePorCpf(dto.Cpf))
                .ReturnsAsync(new Cliente(
                    "Cliente Existente",
                    "existente@email.com",
                    dto.Cpf,
                    "21999999999"
                ));

            // Act
            Func<Task> act = () => _useCase.Execute(dto);

            // Assert
            var exception = await act
                .Should()
                .ThrowAsync<Exception>();

            exception.Which.Message
                .Should()
                .Be(ClientesExceptions.Cliente_CpfExistente);

            _repositoryMock.Verify(
                r => r.AdicionarCliente(It.IsAny<Cliente>()),
                Times.Never
            );
        }

    }
}
