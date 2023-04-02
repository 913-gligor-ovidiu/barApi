using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using lab3.Models;
using lab3.Repository;
using lab3.DTOs;

namespace lab3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly DatabaseContexts _context;

        public ClientsController(DatabaseContexts context)
        {
            _context = context;
        }

        // GET: api/Clients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClientDTO>>> GetClients()
        {
          if (_context.Clients == null)
          {
              return NotFound();
          }
           return await _context.Clients
                .Select(c => ClientToDTO(c))
                .ToListAsync();
        }

        // GET: api/Clients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClientDTO>> GetClient(int id)
        {
          if (_context.Clients == null)
          {
              return NotFound();
          }
            var client = await _context.Clients.FindAsync(id);

            if (client == null)
            {
                return NotFound();
            }

            return ClientToDTO(client) ;
        }

        // PUT: api/Clients/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClient(int id, ClientDTO clientDTO)
        {
            if (id != clientDTO.Id)
            {
                return BadRequest();
            }
            //validate age to be between 16 and 100
            if (clientDTO.Age < 16 || clientDTO.Age > 100)
            {
                return BadRequest();
            }

            var clientToUpdate = await _context.Clients.FindAsync(id);
            if (clientToUpdate == null)
            {
                return NotFound();
            }

            clientToUpdate.Name = clientDTO.Name;
            clientToUpdate.Age = clientDTO.Age;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!ClientExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Clients
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ClientDTO>> PostClient(ClientDTO clientDTO)
        {   
         //validate age to be between 16 and 100
         if (clientDTO.Age < 16 || clientDTO.Age > 100)
            {
                return BadRequest();
            }
            
          var clientToCreate = new Client
          {
              Name = clientDTO.Name,
              Age = clientDTO.Age
          };
            
            _context.Clients.Add(clientToCreate);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetClient),
                new { id = clientToCreate.Id },
                ClientToDTO(clientToCreate));
        }

        // DELETE: api/Clients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            if (_context.Clients == null)
            {
                return NotFound();
            }
            var client = await _context.Clients.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }

            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClientExists(int id)
        {
            return (_context.Clients?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private static ClientDTO ClientToDTO(Client client) =>
            new ClientDTO
            {
                Id = client.Id,
                Name = client.Name,
                Age = client.Age
            };

        //filter clients that are older than a given age
        [HttpGet("filter/{age}")]
        public async Task<ActionResult<IEnumerable<ClientDTO>>> FilterClients(int age)
        {
          if (_context.Clients == null)
            {
              return NotFound();
          }
            return await _context.Clients
                .Where(c => c.Age > age)
                .Select(c => ClientToDTO(c))
                .ToListAsync();
        }

        //make a new client order a drink
        [HttpPost("{clientId}/order/{drinkId}")]
        public async Task<ActionResult<OrderDTO>> OrderDrink(int clientId, int drinkId, OrderDTO orderDTO)
        {   
            //q:how do i use the fields from the http request to create a new order?


            var client = await _context.Clients.FindAsync(clientId);
            var drink = await _context.Drinks.FindAsync(drinkId);

            if (client == null || drink == null)
            {
                return NotFound();
            }
            //validate the number of drinks to be greater than 0
            if (orderDTO.NrOfDrinks < 1)
            {
                return BadRequest();
            }

            var order = new Order
            {
                Client = client,
                Drink = drink,
                DateTime = DateTime.Now,
                NrOfDrinks = orderDTO.NrOfDrinks
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(OrderDrink), orderDTO);
        }

        //show the clients ordered by their total spending ( multiply number of drinks from the order class
        //with the price of the drink from the drink class and use the ClientTotalDTO)
        [HttpGet("total")]
        public async Task<ActionResult<IEnumerable<ClientTotalDTO>>> GetClientsTotal()
        {
          if (_context.Clients == null)
            {
              return NotFound();
          }
           
          //get all orders
          var orders = await _context.Orders
                .Include(o => o.Client)
                .Include(o => o.Drink)
                .ToListAsync();

        //get all clients
        var allClients = await _context.Clients.ToListAsync();

        //get all drinks
        var allDrinks = await _context.Drinks.ToListAsync();

        var clients = new List<ClientTotalDTO>();

        //loop through the orders and calculate the total for each client
        foreach (var order in orders)
        {
            var client = clients.FirstOrDefault(c => c.Id == order.ClientId);
            var actualClient = allClients.Find(c => c.Id == order.ClientId);
            var actualDrink = allDrinks.Find(d => d.Id == order.DrinkId);
            if(client == null)
            {
                client = new ClientTotalDTO
                {
                    Id = actualClient.Id,
                    Name = actualClient.Name,
                    Age = actualClient.Age,
                    Total = 0
                };
                clients.Add(client);
            }
            int total=(int)(order.NrOfDrinks * actualDrink.Price);
            client.Total += total;  
        }

        //sort the clients by their total spending
        clients = clients.OrderByDescending(c => c.Total).ToList();

        return clients;
        }
    }
}
