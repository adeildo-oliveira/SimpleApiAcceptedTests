using AcceptedTestsSimpleApi.Builders;
using AcceptedTestsSimpleApi.Tools;
using FluentAssertions;
using SimpleApiAcceptedTests.Models;
using SimpleApiAcceptedTests.Repository;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace AcceptedTestsSimpleApi
{
    public class ClientRepositoryTests : IntegrationTestFixture
    {
        private readonly IClientRepository _clientRepository;
        private readonly DatabaseFixture _fixture;

        public ClientRepositoryTests(DatabaseFixture fixture) : base(fixture)
        {
            _fixture = fixture;
            _clientRepository = _fixture.GetService<IClientRepository>();
            _fixture.CreateServiceClient();
        }

        [Fact]
        public async Task DeveInserirCliente()
        {
            _fixture.ClearDataBase();
            var clienteBuilder = new ClienteBuilder().ComNome("Cliente 02").Instanciar();
            
            var request = new HttpRequestMessageBuilder()
                .ComMethod(HttpMethod.Post)
                .ComUrl("api/Client/PostAsync")
                .ComBody(clienteBuilder)
                .Instanciar();

            var response = await _fixture.Client.SendAsync(request);
            var resultado = await _clientRepository.GetByIdAsync(1);

            response.IsSuccessStatusCode.Should().BeTrue();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            resultado.Id.Should().Be(1);
            resultado.Nome.Should().Be("Cliente 02");
            resultado.SobreNome.Should().Be("Sobre Nome Cliente");
        }

        [Fact]
        public async Task DeveObterCliente()
        {
            _fixture.ClearDataBase();
            var clienteBuilder = new ClienteBuilder().Instanciar();
            var request = new HttpRequestMessageBuilder()
                .ComMethod(HttpMethod.Get)
                .ComUrl("api/Client/GetAsync/1")
                .ComBody(string.Empty)
                .Instanciar();
            await _clientRepository.AddAsync(clienteBuilder);

            var response = await _fixture.Client.SendAsync(request);
            var resultado = await response.Content.ReadAsAsync<Cliente>();

            response.IsSuccessStatusCode.Should().BeTrue();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            resultado.Id.Should().Be(1);
            resultado.Nome.Should().Be("Nome Cliente");
            resultado.SobreNome.Should().Be("Sobre Nome Cliente");
        }

        [Fact]
        public async Task DeveObterTodosOsClientes()
        {
            _fixture.ClearDataBase();
            await _clientRepository.AddAsync(new ClienteBuilder().Instanciar());
            await _clientRepository.AddAsync(new ClienteBuilder().ComNome("Nome Cliente 2").Instanciar());

            var request = new HttpRequestMessageBuilder()
                .ComMethod(HttpMethod.Get)
                .ComUrl("api/Client/GetAsync")
                .ComBody(string.Empty)
                .Instanciar();
            
            var response = await _fixture.Client.SendAsync(request);
            var resultados = await response.Content.ReadAsAsync<List<Cliente>>();
            
            response.IsSuccessStatusCode.Should().BeTrue();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            resultados.Should().HaveCount(2);
            resultados[0].Id.Should().Be(1);
            resultados[0].Nome.Should().Be("Nome Cliente");
            resultados[0].SobreNome.Should().Be("Sobre Nome Cliente");
            resultados[1].Id.Should().Be(2);
            resultados[1].Nome.Should().Be("Nome Cliente 2");
            resultados[1].SobreNome.Should().Be("Sobre Nome Cliente");
        }
    }
}
