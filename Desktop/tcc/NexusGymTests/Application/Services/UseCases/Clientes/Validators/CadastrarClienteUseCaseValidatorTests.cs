using AutoMapper;
using FluentAssertions;
using Moq;
using NexusGym.Application.Dto.Cliente;
using NexusGym.Application.Services.UseCases.Clientes.Commands;
using NexusGym.Application.Validators.Cliente;
using NexusGym.Domain.Abstractions.Clientes;
using NexusGym.Domain.Entities;
using NexusGym.Exceptions.Clientes;


namespace NexusGymTests.Application.Services.UseCases.Clientes.Validators
{
    [TestClass]
    public class CadastrarClienteUseCaseValidatorTests
    {

        private Mock<IClienteRepository> _repositoryMock = null;
        private IMapper _mapper = null!;
        private CadastrarClienteUseCase _useCase = null;

        [TestInitialize]
        public void Setup()
        {
            _repositoryMock = new Mock<IClienteRepository>();
            var MapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Cliente, ClienteResponseDTO>();
            });

            _mapper = MapperConfig.CreateMapper();
            _useCase = new CadastrarClienteUseCase(_repositoryMock.Object, _mapper);
        }

        [TestMethod]
        public void Deve_validar_com_sucesso_quando_dados_validos()
        {
            var dto = new ClienteCreateDTO
            {
                Nome = "Pedro Machado",
                Email = "pedro@email.com",
                Cpf = "12345678910",
                Telefone = "21999999999"
            };

            var validator = new ClienteCreateValidator();
            var resultado = validator.Validate(dto);

            resultado.IsValid.Should().BeTrue();
            resultado.Errors.Should().BeEmpty();
        }

        [TestMethod]
        public void Nao_Deve_Cadastrar_Sem_Email()
        {
            var requisicao = new ClienteCreateDTO
            { 
                Nome = "Pedro Machado",
                Email ="",
                Cpf = "12345678910",
                Telefone ="21988405670"
            };

            var validator = new ClienteCreateValidator();
          
            var resultado = validator.Validate(requisicao);
           
            resultado.IsValid.Should().BeFalse();

                resultado.Errors.Should().ContainSingle(e =>
            e.PropertyName == "Email" &&
            e.ErrorMessage == ClientesExceptions.Cliente_EmailObrigatorio
        );

        }

        [TestMethod]
        public void Nao_Deve_Cadastrar_Sem_Nome()
        {
            var requisicao = new ClienteCreateDTO
            {
                Nome = "",
                Email = "teste@gmail.com",
                Cpf = "12345678910",
                Telefone = "21988405670"
            };

            var validator = new ClienteCreateValidator();

            var resultado = validator.Validate(requisicao);

            resultado.IsValid.Should().BeFalse();

            resultado.Errors.Should().ContainSingle(e =>
        e.PropertyName == "Nome" &&
        e.ErrorMessage == ClientesExceptions.Cliente_NomeObrigatorio);
        }

        [TestMethod]
        public void Nao_Deve_Cadastrar_Sem_Cpf()
        {
            var requisicao = new ClienteCreateDTO
            {
                Nome = "Pedro Machado",
                Email = "teste@gmail.com",
                Cpf = "",
                Telefone = "21988405670"
            };

            var validator = new ClienteCreateValidator();

            var resultado = validator.Validate(requisicao);

            resultado.IsValid.Should().BeFalse();

            resultado.Errors.Should().ContainSingle(e =>
        e.PropertyName == "Cpf" &&
        e.ErrorMessage == ClientesExceptions.Cliente_CpfObrigatorio);
        }

        [TestMethod]
        public void Nao_Deve_Cadastrar_Sem_Telefone()
        {
            var requisicao = new ClienteCreateDTO
            {
                Nome = "Pedro Machado",
                Email = "teste@gmail.com",
                Cpf = "12345678910",
                Telefone = ""
            };

            var validator = new ClienteCreateValidator();

            var resultado = validator.Validate(requisicao);

            resultado.IsValid.Should().BeFalse();

            resultado.Errors.Should().ContainSingle(e =>
        e.PropertyName == "Telefone" &&
        e.ErrorMessage == ClientesExceptions.Cliente_TelefoneObrigatório);

        }

        [TestMethod]

        public void Nao_Deve_Cadastrar_Com_Cpf_Invalido()
        {
            var requisicao = new ClienteCreateDTO
            {
                Nome = "Pedro Machado",
                Email = "teste@gmail.com",
                Cpf = "123",
                Telefone = "21988405670"
            };

            var validator = new ClienteCreateValidator();

            var resultado = validator.Validate(requisicao);

            resultado.IsValid.Should().BeFalse();

            resultado.Errors.Should().ContainSingle(e =>
        e.PropertyName == "Cpf" &&
        e.ErrorMessage == ClientesExceptions.Cliente_CpfInvalido);

        }       
        
        [TestMethod]

        public void Nao_Deve_Cadastrar_Com_Email_Invalido()
        {
            var requisicao = new ClienteCreateDTO
            {
                Nome = "Pedro Machado",
                Email = "teste",
                Cpf = "12345678910",
                Telefone = "21988405670"
            };

            var validator = new ClienteCreateValidator();

            var resultado = validator.Validate(requisicao);

            resultado.IsValid.Should().BeFalse();

            resultado.Errors.Should().ContainSingle(e =>
        e.PropertyName == "Email" &&
        e.ErrorMessage == ClientesExceptions.Cliente_EmailInvalido);

        }


    }
}
