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
    public class DrinksController : ControllerBase
    {
        private readonly DatabaseContexts _context;

        public DrinksController(DatabaseContexts context)
        {
            _context = context;
        }

        // GET: api/Drinks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DrinkDTO>>> GetDrinks()
        {
          if (_context.Drinks == null)
          {
              return NotFound();
          }
            return await _context.Drinks
                .Select(d => DrinkToDTO(d))
                .ToListAsync();
        }

        // GET: api/Drinks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Drink>> GetDrink(int id)
        {
          if (_context.Drinks == null)
          {
              return NotFound();
          }
            var drink = await _context.Drinks
                .Include(x => x.DrinkType)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (drink == null)
                return NotFound();
            

            return drink;
        }

        // PUT: api/Drinks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDrink(int id, DrinkDTO drinkDTO)
        {
            if (id != drinkDTO.Id)
            {
                return BadRequest();
            }

            //validate drink type
            var drinkType = await _context.DrinkTypes.FindAsync(drinkDTO.DrinkTypeId);
            if (drinkType == null)
            {
                return BadRequest();
            }

            //validate price to be greater than 6
            if (drinkDTO.Price < 6)
            {
                return BadRequest();
            }

            var drinkToUpdate = await _context.Drinks.FindAsync(id);
            if (drinkToUpdate == null)
            {
                return NotFound();
            }

            drinkToUpdate.Name = drinkDTO.Name;
            drinkToUpdate.Price = drinkDTO.Price;
            drinkToUpdate.Quantity = drinkDTO.Quantity;
            drinkToUpdate.Abv = drinkDTO.Abv;
            drinkToUpdate.DrinkTypeId = drinkDTO.DrinkTypeId;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!DrinkExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Drinks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DrinkDTO>> PostDrink(DrinkDTO drinkDTO)
        {   
           //validate drink type
           var drinkType = await _context.DrinkTypes.FindAsync(drinkDTO.DrinkTypeId);
            if (drinkType == null)
            {
                return BadRequest();
            }

            //validate price to be greater than 6
            if (drinkDTO.Price < 6)
            {
                return BadRequest();
            }


           var drink = new Drink
           {
                Name = drinkDTO.Name,
                Price = drinkDTO.Price,
                Quantity = drinkDTO.Quantity,
                Abv = drinkDTO.Abv,
                DrinkTypeId = drinkDTO.DrinkTypeId,
            };

            _context.Drinks.Add(drink);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                 nameof(GetDrink),
                 new { id = drink.Id },
                  DrinkToDTO(drink));
        }

        // DELETE: api/Drinks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDrink(int id)
        {
            if (_context.Drinks == null)
            {
                return NotFound();
            }
            var drink = await _context.Drinks.FindAsync(id);
            if (drink == null)
            {
                return NotFound();
            }

            _context.Drinks.Remove(drink);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DrinkExists(int id)
        {
            return (_context.Drinks?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private static DrinkDTO DrinkToDTO(Drink drink) =>
            new DrinkDTO
            {
                Id = drink.Id,
                Name = drink.Name,
                Price = drink.Price,
                Quantity = drink.Quantity,
                Abv = drink.Abv,
                DrinkTypeId = drink.DrinkTypeId,
            };

        //show all drinks ordered by the total number of times they were ordered by clients
        [HttpGet("mostOrdered")]
        public async Task<ActionResult<IEnumerable<DrinkTotalOrderedDTO>>> GetDrinksAvgAge()
        {
            if (_context.Drinks == null)
            {
                return NotFound();
            }
            //get all orders
            var orders = await _context.Orders
                .Include(x => x.Client)
                .Include(x => x.Drink)
                .ToListAsync();

            //get all drinks
            var allDrinks = await _context.Drinks.ToListAsync();

            var drinks = new List<DrinkTotalOrderedDTO>();
            foreach (var order in orders)
            {
                var drink = drinks.FirstOrDefault(x => x.Id == order.DrinkId);
                var actualDrink = allDrinks.Find(d => d.Id == order.DrinkId);
                if (drink == null)
                {
                    drink = new DrinkTotalOrderedDTO
                    {
                        Id = order.DrinkId,
                        Name = actualDrink.Name,
                        Total = order.NrOfDrinks
                    };
                    drinks.Add(drink);
                }
                else
                    drink.Total += order.NrOfDrinks;

            }

            //sort by total
            drinks = drinks.OrderByDescending(x => x.Total).ToList();
            return drinks;

        }

    }
}
