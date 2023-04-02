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
using System.Collections.ObjectModel;

namespace lab3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DrinkTypesController : ControllerBase
    {
        private readonly DatabaseContexts _context;

        public DrinkTypesController(DatabaseContexts context)
        {
            _context = context;
        }

        // GET: api/DrinkTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DrinkTypeDTO>>> GetDrinkTypes()
        {
          if (_context.DrinkTypes == null)
          {
              return NotFound();
          }
            return await _context.DrinkTypes
                .Select(x => DrinkTypeToDTO(x))
                .ToListAsync();
        }

        // GET: api/DrinkTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DrinkTypeWithDrinksDTO>> GetDrinkType(int id)
        {
          if (_context.DrinkTypes == null)
          {
              return NotFound();
          }
          var drinkType = await _context.DrinkTypes.FindAsync(id);

          if (drinkType == null)
           {
               return NotFound();
           }

          //get all drinks
          var drinks = await _context.Drinks
                .Include(x => x.DrinkType)
                .Where(x => x.DrinkType.Id == id)
                .ToListAsync();

          //transform drinks to DTOs
         var drinksDTO = drinks.Select(x => DrinkToDTO(x)).ToList();

        var drinkTypeDTO = new DrinkTypeWithDrinksDTO
        {
            Id = drinkType.Id,
            Name = drinkType.Name,
            NrOfBrands = drinkType.NrOfBrands,
            Stock = drinkType.Stock,
            ProfitMargin = drinkType.ProfitMargin,
            Drinks = drinksDTO
        };

            return drinkTypeDTO;

        }

        // PUT: api/DrinkTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDrinkType(int id, DrinkTypeDTO drinkTypeDTO)
        {
            if (id != drinkTypeDTO.Id)
                return BadRequest();

            var drinkType = await _context.DrinkTypes.FindAsync(id);
            if (drinkType == null)
                return NotFound();

            drinkType.Name = drinkTypeDTO.Name;
            drinkType.NrOfBrands = drinkTypeDTO.NrOfBrands;
            drinkType.Stock = drinkTypeDTO.Stock;
            drinkType.ProfitMargin = drinkTypeDTO.ProfitMargin;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!DrinkTypeExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/DrinkTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DrinkTypeDTO>> PostDrinkType(DrinkTypeDTO  drinkTypeDTO)
        {
           var drinkType = new DrinkType
           {
                Name = drinkTypeDTO.Name,
                NrOfBrands = drinkTypeDTO.NrOfBrands,
                Stock = drinkTypeDTO.Stock,
                ProfitMargin = drinkTypeDTO.ProfitMargin
            };

            _context.DrinkTypes.Add(drinkType);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                   nameof(GetDrinkType),
                   new { id = drinkType.Id },
                   DrinkTypeToDTO(drinkType));
        }

        // DELETE: api/DrinkTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDrinkType(int id)
        {
            if (_context.DrinkTypes == null)
            {
                return NotFound();
            }
            var drinkType = await _context.DrinkTypes.FindAsync(id);
            if (drinkType == null)
            {
                return NotFound();
            }

            _context.DrinkTypes.Remove(drinkType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DrinkTypeExists(int id)
        {
            return (_context.DrinkTypes?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private static DrinkTypeDTO DrinkTypeToDTO(DrinkType drinkType) =>
            new DrinkTypeDTO
            {
                Id = drinkType.Id,
                Name = drinkType.Name,
                NrOfBrands = drinkType.NrOfBrands,
                Stock = drinkType.Stock,
                ProfitMargin = drinkType.ProfitMargin
            };

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

        //add multiple drinks to a drinktype
        [HttpPost("{id}/drinks")]
        public async Task<ActionResult<DrinkTypeDTO>> PostDrinkType(int id, IEnumerable<DrinkDTO> drinksDTO)
        {
            if (_context.DrinkTypes == null)
            {
                return NotFound();
            }
            var drinkType = await _context.DrinkTypes.FindAsync(id);
            if (drinkType == null)
            {
                return NotFound();
            }

            foreach (var drinkDTO in drinksDTO)
            {
                var drink = new Drink
                {
                    Name = drinkDTO.Name,
                    Price = drinkDTO.Price,
                    Quantity = drinkDTO.Quantity,
                    Abv = drinkDTO.Abv,
                    DrinkTypeId = id
                };
                _context.Drinks.Add(drink);
            }
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                                  nameof(GetDrinkType),
                                                    new { id = drinkType.Id },
                                                                      DrinkTypeToDTO(drinkType));
        }
        
    }
}
