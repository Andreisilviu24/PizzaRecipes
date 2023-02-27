namespace PizzaRecipe_API.Models
{
    public class Pizza
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Ingredients { get; set; }
        public int Price { get; set; }
    }
}
