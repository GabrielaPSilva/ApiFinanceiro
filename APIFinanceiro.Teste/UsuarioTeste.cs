using APIFinanceiro.Business.Services;
using APIFinanceiro.Data.Repositories.Interfaces;
using APIFinanceiro.Model.Entities;
using AutoBogus;
using AutoFixture;
using FluentAssertions;
using Moq;

namespace APIFinanceiro.Teste
{
    public class UsuarioTeste
    {
        [Fact(DisplayName = "Listar todos os usuários")]
        [Trait("UsuarioRepository", "ListarUsuario")]
        public async Task ListarUsuario_ShouldReturnListOfUsuarios()
        {
            // Arrange
            var mockUsuarioRepository = new Mock<IUsuarioRepository>();
            var usuarioService = new UsuarioService(mockUsuarioRepository.Object);

            var usuarioFaker = new AutoFaker<UsuarioModel>()
                .RuleFor(fake => fake.Id, fake => fake.Random.Number(1, 1000))
                .RuleFor(fake => fake.IdRisco, fake => fake.Random.Number(1, 100))
                .RuleFor(fake => fake.Nome, fake => fake.Person.FullName)
                .RuleFor(fake => fake.Email, fake => fake.Person.Email)
                .RuleFor(fake => fake.Telefone, fake => fake.Person.Phone)
                .RuleFor(fake => fake.CPF, fake => fake.Random.String())
                .RuleFor(fake => fake.DataNascimento, fake => fake.Person.DateOfBirth.ToString("yyyy-MM-dd"))
                .RuleFor(fake => fake.Ativo, fake => fake.Random.Bool());

            var usuarioGenerate = usuarioFaker.Generate();

            mockUsuarioRepository.Setup(repo => repo.ListarUsuario());

            // Act
            var result = await usuarioService.ListarUsuario();

            // Assert
            mockUsuarioRepository.Should().NotBeNull();
        }

        [Fact(DisplayName = "Retornar usuário CPF")]
        [Trait("UsuarioRepository", "RetornarUsuario")]
        public async Task RetornarUsuarioCPF_ShouldReturnUsuario()
        {
            // Arrange
            var mockUsuarioRepository = new Mock<IUsuarioRepository>();
            var usuarioService = new UsuarioService(mockUsuarioRepository.Object);

            var usuarioFaker = new AutoFaker<UsuarioModel>()
                .RuleFor(fake => fake.Id, fake => fake.Random.Number(1, 1000))
                .RuleFor(fake => fake.IdRisco, fake => fake.Random.Number(1, 100))
                .RuleFor(fake => fake.Nome, fake => fake.Person.FullName)
                .RuleFor(fake => fake.Email, fake => fake.Person.Email)
                .RuleFor(fake => fake.Telefone, fake => fake.Person.Phone)
                .RuleFor(fake => fake.CPF, fake => fake.Random.String())
                .RuleFor(fake => fake.DataNascimento, fake => fake.Person.DateOfBirth.ToString("yyyy-MM-dd"))
                .RuleFor(fake => fake.Ativo, fake => fake.Random.Bool());

            var usuarioGenerate = usuarioFaker.Generate();

            mockUsuarioRepository.Setup(repo => repo.RetornarUsuarioCPF(usuarioGenerate.CPF!)).ReturnsAsync(usuarioGenerate);

            // Act
            var result = await usuarioService.RetornarUsuarioCPF(usuarioGenerate.CPF!);

            // Assert
            result.Should().NotBeNull();
        }

        [Fact(DisplayName = "Cadastrar usuário com sucesso")]
        [Trait("UsuarioRepository", "CadastrarUsuario")]
        public async Task CadastrarUsuario_WithValidUsuario_ShouldReturnUserId()
        {
            // Arrange
            var usuarioRepositoryMock = new Mock<IUsuarioRepository>();
            var usuarioService = new UsuarioService(usuarioRepositoryMock.Object);

            var usuarioFaker = new AutoFaker<UsuarioModel>();
            var usuarioFake = usuarioFaker.Generate();

            usuarioRepositoryMock.Setup(repo => repo.CadastrarUsuario(It.IsAny<UsuarioModel>()))
                                 .ReturnsAsync(2);

            // Act
            var result = await usuarioService.CadastrarUsuario(usuarioFake);

            // Assert
            result.Should().Be(2);
            usuarioRepositoryMock.Verify(repo => repo.CadastrarUsuario(It.IsAny<UsuarioModel>()), Times.Once);
        }

        [Fact(DisplayName = "Alterar usuário com sucesso")]
        [Trait("UsuarioRepository", "AlterarUsuario")]
        public async Task AlterarUsuario_WithValidUsuario_ShouldReturnTrue()
        {
            // Arrange
            var usuarioRepositoryMock = new Mock<IUsuarioRepository>();
            var usuarioService = new UsuarioService(usuarioRepositoryMock.Object);

            var usuarioFaker = new AutoFaker<UsuarioModel>();
            var usuarioFake = usuarioFaker.Generate();

            usuarioRepositoryMock.Setup(repo => repo.AlterarUsuario(It.IsAny<UsuarioModel>()))
                                 .ReturnsAsync(true);

            // Act
            var result = await usuarioService.AlterarUsuario(usuarioFake);

            // Assert
            result.Should().BeTrue();
            usuarioRepositoryMock.Verify(repo => repo.AlterarUsuario(It.IsAny<UsuarioModel>()), Times.Once);
        }

        [Fact(DisplayName = "Alterar usuário com falha")]
        [Trait("UsuarioRepository", "AlterarUsuario")]
        public async Task AlterarUsuario_WithNullUsuario_ShouldReturnFalse()
        {
            // Arrange
            var usuarioRepositoryMock = new Mock<IUsuarioRepository>();
            var usuarioService = new UsuarioService(usuarioRepositoryMock.Object);

            // Act
            var result = await usuarioService.AlterarUsuario(null);

            // Assert
            result.Should().BeFalse();
            usuarioRepositoryMock.Verify(repo => repo.AlterarUsuario(It.IsAny<UsuarioModel>()), Times.Never);
        }
    }
}