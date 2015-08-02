using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineOrdering.Models;
using OnlineOrderingBusiness;
using OnlineOrderingBusiness.Repositories;

namespace OnlineOrdering.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        [HttpGet]
        public ActionResult ViewOrders()
        {
            var repository = new OrderRepository();
            var orderModelList = repository.GetOrders().Select(o => new OrderModel
            {
                OrderId = o.OrderId,
                CustomerName = String.Format("{0} {1}", o.Customer.FirstName, o.Customer.LastName),
                OrderCode = o.OrderCode,
                OrderDateTime = o.OrderDateTime,
                OrderTotal = o.OrderTotal
            });
            return View(orderModelList);
        }


        [HttpGet]
        public ActionResult CreateOrder()
        {
            var repository = new CustomerRepository();
            var customerDictionary =
                repository.GetCustomers()
                    .Select(c => new {CustomerId = c.CustomerId, CustomerName = c.FirstName + " " + c.LastName})
                    .ToDictionary(x => x.CustomerId, x => x.CustomerName);
            var model = new OrderModel
            {
                OrderId = Guid.NewGuid(),
                OrderDateTime = DateTime.Now,
                CustomerDictionary = customerDictionary
            };
            return View(model);
        }


        [HttpPost]
        public ActionResult CreateOrder(OrderModel model)
        {
            var orderRepository = new OrderRepository();
            var order = new Order
            {
                OrderId = model.OrderId,
                OrderDateTime = model.OrderDateTime,
                CustomerId = model.CustomerId

            };
            orderRepository.CreateOrder(order);
            return RedirectToAction("AddItem", new {id = model.OrderId});
        }


    [HttpGet]
        public ActionResult AddItem(Guid id)
        {
            var repository = new ProductRepository();
            var productDictionary = repository.GetProducts().OrderBy(product => product.ProductName)
                .Select(p => new {ProductId = p.ProductId, ProductName = p.ProductName})
                .ToDictionary(x => x.ProductId, x => x.ProductName);
            var quantityDictionary = new Dictionary<int, int>();
            for (int i = 1; i <= 10; i++)
            {
                quantityDictionary.Add(i, i);
            }
            var model = new OrderItemModel
            {
                OrderItemId = Guid.NewGuid(),
                OrderId = id,
                OrderItemTotal = 0.ToString("c"),
                ProductDictionary = productDictionary,
                QuantityDictionary = quantityDictionary
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult AddItem(OrderItemModel model)
        {
            var orderItem = new OrderItem
            {
                OrderId = model.OrderId,
                OrderItemId = Guid.NewGuid(),
                OrderItemTotal = Convert.ToDecimal(model.OrderItemTotal.Replace("$", "")),
                ProductId = model.ProductId,
                Quantity = model.Quantity
            };
            var repository = new OrderRepository();
            repository.AddOrderItem(orderItem);
            return RedirectToAction("ViewOrder", new {id = model.OrderId});
        }


        [HttpGet]
        public ActionResult ViewOrder(Guid id)
        {
            var repository = new OrderRepository();
            var order = repository.GetOrder(id);
            var model = new OrderModel
            {
                OrderId = order.OrderId,
                OrderCode = order.OrderCode,
                OrderDateTime = order.OrderDateTime,
                CustomerName = String.Format("{0} {1}", order.Customer.FirstName, order.Customer.LastName),
                OrderTotal = order.OrderTotal
            };
            model.OrderItems = order.OrderItems.Select(oi => new OrderItemModel
            {
                OrderItemId = oi.OrderItemId,
                OrderId = oi.OrderId,
                ProductName = oi.Product.ProductName,
                Quantity = oi.Quantity,
                OrderItemTotal = oi.OrderItemTotal.ToString("c")
            }).ToList();

            return View(model);
        }


        public JsonResult GetOrderItemTotal(Guid productId, int quantity)
        {
            var repository = new ProductRepository();
            var unitCost = repository.GetProduct(productId).Price;
            var orderItemTotal = unitCost*quantity;
            return Json(new {orderItemTotal = orderItemTotal}, JsonRequestBehavior.AllowGet);
        }

    }
}
