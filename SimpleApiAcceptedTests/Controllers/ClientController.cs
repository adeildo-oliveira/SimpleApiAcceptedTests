using Microsoft.AspNetCore.Mvc;
using SimpleApiAcceptedTests.Models;
using SimpleApiAcceptedTests.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleApiAcceptedTests.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientRepository _clientRepository;

        public ClientController(IClientRepository clientRepository) => _clientRepository = clientRepository;

        [HttpGet]
        [Route("GetAsync")]
        public async Task<IEnumerable<Cliente>> Get() => await _clientRepository.GetAllAsync();

        [HttpGet]
        [Route("GetAsync/{id:int}")]
        public async Task<Cliente> Get(int id) => await _clientRepository.GetByIdAsync(id);

        [HttpPost]
        [Route("PostAsync")]
        public async Task Post([FromBody] Cliente cliente) => await _clientRepository.AddAsync(cliente);

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value) => throw new NotImplementedException();

        [HttpDelete("{id}")]
        public void Delete(int id) => throw new NotImplementedException();
    }
}
