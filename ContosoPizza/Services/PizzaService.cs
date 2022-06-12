using ContosoPizza.Data;
using ContosoPizza.Models;
using Microsoft.EntityFrameworkCore;

namespace ContosoPizza.Services;

public class PizzaService
{
    private readonly PizzaContext context;

    public PizzaService(PizzaContext context)
    {
        this.context = context;
    }

    public IEnumerable<Pizza> GetAll()
    {
        return context.Pizzas.AsNoTracking().ToList();
    }

    public Pizza? GetById(int id)
    {
        return context.Pizzas
            .Include(p => p.Toppings)
            .Include(p => p.Sauce)
            .AsNoTracking().SingleOrDefault(p => p.Id == id);
    }

    public Pizza? Create(Pizza newPizza)
    {
        context.Pizzas.Add(newPizza);
        context.SaveChanges();

        return newPizza;
    }

    public void AddTopping(int pizzaId, int toppingId)
    {
        var pizzaToUpdate = context.Pizzas.Find(pizzaId);
        var toppingToAdd = context.Toppings.Find(toppingId);

        if (pizzaToUpdate is null || toppingToAdd is null)
        {
            throw new InvalidOperationException("Pizza or topping does not exist");
        }

        if (pizzaToUpdate.Toppings is null)
        {
            pizzaToUpdate.Toppings = new List<Topping>();
        }

        pizzaToUpdate.Toppings.Add(toppingToAdd);
        
        context.SaveChanges();
    }

    public void UpdateSauce(int pizzaId, int sauceId)
    {
        var pizzaToUpdate = context.Pizzas.Find(pizzaId);
        var sauceToUpdate = context.Sauces.Find(sauceId);

        if (pizzaToUpdate is null || sauceToUpdate is null)
        {
            throw new InvalidOperationException("Pizza or sauce does not exist");
        }

        pizzaToUpdate.Sauce = sauceToUpdate;

        context.SaveChanges();
    }

    public void DeleteById(int id)
    {
        var pizzaToDelete = context.Pizzas.Find(id);

        if (pizzaToDelete is not null)
        {
            context.Pizzas.Remove(pizzaToDelete);
            context.SaveChanges();
        }
    }
}