using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzaRecipe_API.Data;
using PizzaRecipe_API.Models;

namespace PizzaRecipe_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PizzasController : Controller
    {
        private readonly PizzaRecipeDbContext _pizzaRecipeDbContext;
        public PizzasController(PizzaRecipeDbContext pizzaRecipeDbContext)
        {
            _pizzaRecipeDbContext = pizzaRecipeDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPizzas()
        {
            var pizzas = await _pizzaRecipeDbContext.Pizzas.ToListAsync();

            return Ok(pizzas);
        }

        [HttpPost]
        public async Task<IActionResult> AddPizza([FromBody] Pizza pizzaRequest)
        {
            pizzaRequest.Id = Guid.NewGuid();
            await _pizzaRecipeDbContext.Pizzas.AddAsync(pizzaRequest);
            await _pizzaRecipeDbContext.SaveChangesAsync();

            return Ok(pizzaRequest);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetPizza([FromRoute] Guid id)
        {
            var pizza = await _pizzaRecipeDbContext.Pizzas.FirstOrDefaultAsync(x => x.Id == id);

            if(pizza == null)
            {
                return NotFound();
            }

            return Ok(pizza);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdatePizza([FromRoute] Guid id, Pizza updatePizzaRequest)
        {
            var pizza = await _pizzaRecipeDbContext.Pizzas.FindAsync(id);

            if(pizza == null)
            {
                return NotFound();
            }

            pizza.Name = updatePizzaRequest.Name;
            pizza.Ingredients = updatePizzaRequest.Ingredients;
            pizza.Price = updatePizzaRequest.Price;

            await _pizzaRecipeDbContext.SaveChangesAsync();

            return Ok(pizza);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeletePizza([FromRoute] Guid id)
        {
            var pizza = await _pizzaRecipeDbContext.Pizzas.FindAsync(id);

            if(pizza == null)
            {
                return NotFound();
            }

            _pizzaRecipeDbContext.Pizzas.Remove(pizza);
            await _pizzaRecipeDbContext.SaveChangesAsync();

            return Ok(pizza);
        }
    }
}
