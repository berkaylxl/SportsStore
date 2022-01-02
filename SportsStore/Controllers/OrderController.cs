using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepository _repository;
        private readonly Cart _cart;

        public OrderController(IOrderRepository repository,Cart cart)
        {
            _repository = repository;
            _cart = cart;
        }
        public ViewResult CheckOut() => View(new Order());
        [HttpPost]
        public IActionResult CheckOut(Order order)
        {
            if (_cart.Lines.Count()==0)
            {
                ModelState.AddModelError("", "Sorry your cart is empty");

            }
            if (ModelState.IsValid)
            {
                order.Lines = _cart.Lines.ToArray();
                _repository.SaveOrder(order);
                _cart.Clear();
                return RedirectToPage("/Completed", new { orderId = order.OrderID });
            }
            else
            {
                return View();
            }
        }
    }
}
