using Dapper;
using Microsoft.Extensions.Configuration;
using SimpleApiAcceptedTests.Models;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace SimpleApiAcceptedTests.Repository
{
    public interface IClientRepository
    {
        Task AddAsync(Cliente cliente);
        Task<Cliente> GetByIdAsync(int id);
        Task<IEnumerable<Cliente>> GetAllAsync();
    }

    public class ClientRepository : ContextDapper, IClientRepository
    {
        private const string ADD = "INSERT INTO Cliente (Nome, SobreNome) values (@NOME, @SOBRENOME)";
        private const string GET_ALL = "SELECT Id, Nome, SobreNome FROM Cliente (nolock)";
        private const string GET_BY_ID = "SELECT Id, Nome, SobreNome FROM Cliente (nolock) WHERE Id = @ID";

        public ClientRepository(IConfiguration confiruation) : base(confiruation)
        {
        }

        public async Task AddAsync(Cliente cliente)
        {
            using var conn = Connection;
            await conn.ExecuteAsync(ADD,
                                        param: new
                                        {
                                            NOME = cliente.Nome,
                                            SOBRENOME = cliente.SobreNome
                                        },
                                        commandType: CommandType.Text);
        }

        public async Task<IEnumerable<Cliente>> GetAllAsync()
        {
            using var conn = Connection;
            return await conn.QueryAsync<Cliente>(GET_ALL, commandType: CommandType.Text);
        }

        public async Task<Cliente> GetByIdAsync(int id)
        {
            using var conn = Connection;
            return await conn.QueryFirstOrDefaultAsync<Cliente>(GET_BY_ID, param: new { ID = id }, commandType: CommandType.Text);
        }
    }
}
