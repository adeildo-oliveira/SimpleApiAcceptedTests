using SimpleApiAcceptedTests.Models;

namespace AcceptedTestsSimpleApi.Builders
{
    public class ClienteBuilder
    {
        private string _nome = "Nome Cliente";
        private string _sobreNome = "Sobre Nome Cliente";

        public ClienteBuilder ComNome(string nome)
        {
            _nome = nome;
            return this;
        }

        public ClienteBuilder ComSobreNome(string SobreNome)
        {
            _sobreNome = SobreNome;
            return this;
        }

        public Cliente Instanciar() => new Cliente
        {
            Nome = _nome,
            SobreNome = _sobreNome
        };
    }
}
