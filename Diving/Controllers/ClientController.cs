using Microsoft.AspNetCore.Mvc;
using Diving.Models;
using Diving.Repositories;

namespace Diving.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {

        private readonly IClientRepository _clientRepository;
        private readonly ILogger _logger;

        public ClientController(IClientRepository clientRepository, ILogger<ClientController> logger)
        {
            _clientRepository = clientRepository;
            _logger = logger;

        }

        // GET: api/Client
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Client>>> GetClients()
        {
            _logger.LogInformation("GET: GetAllClients");
            var clients = await _clientRepository.GetAllClients();
            return Ok(clients);
        }

        // GET: api/Client/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Client>> GetClient(long id)
        {
            _logger.LogInformation("GET: GetClientWithId");
            var client = await _clientRepository.GetById(id);//_context.Clients.FindAsync(id);

            if (client == null)
            {
                return NotFound();
            }

            return client;
        }

        // PUT: api/Client/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClient(long id, Client client)
        {
            _logger.LogInformation("PUT: AddOrChangeClient");
            if (id != client.ClientId)
            {
                return BadRequest();
            }

            await _clientRepository.Add(client);
            
            return NoContent();
        }

        // POST: api/Client
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Client>> PostClient(Client client)
        {
            _logger.LogInformation("POST: AddClient");
            await _clientRepository.Add(client);
            await _clientRepository.Save();

            return CreatedAtAction("GetClient", new { id = client.ClientId }, client);
        }

        // DELETE: api/Client/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(long id)
        {
            _logger.LogInformation("DELETE: DeleteClientWithID");
            var client = await _clientRepository.GetById(id);
            if (client == null)
            {
                return NotFound();
            }

            _clientRepository.Remove(client);
            await _clientRepository.Save();

            return NoContent();
        }
    }
}
