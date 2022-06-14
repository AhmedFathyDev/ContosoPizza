using ContosoPizza.Data;
using ContosoPizza.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContosoPizza.Controllers;

[ApiController]
[Route("[controller]")]
public class CouponController : ControllerBase
{
    private readonly PromotionsContext context;

    public CouponController(PromotionsContext context)
    {
        this.context = context;
    }

    [HttpGet]
    public IEnumerable<Coupon> Get()
    {
        return context.Coupons
            .AsNoTracking()
            .ToList();
    }
}