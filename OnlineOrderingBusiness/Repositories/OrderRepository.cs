using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineOrderingBusiness.Repositories
{
     public class OrderRepository 
   { 
    private OnlineOrderingEntities _entities = new OnlineOrderingEntities(); 

 
      public List<Order> GetOrders() 
       { 
           return _entities.Orders.ToList(); 
         } 

 
        public Order GetOrder(Guid orderId) 
      { 
           return _entities.Orders.FirstOrDefault(o => o.OrderId == orderId); 
       }

         public void CreateOrder(Order order)
         {
             var orderCodeNumberSetting = _entities.ApplicationSettings.First();
             var orderCodeNumber = Int32.Parse(orderCodeNumberSetting.ApplicationSettingValue);
             orderCodeNumber++;
             orderCodeNumberSetting.ApplicationSettingValue = orderCodeNumber.ToString();
             order.OrderCode = String.Format("NWT{0}", orderCodeNumber);
             _entities.Orders.Add(order);
             _entities.Entry<ApplicationSetting>(orderCodeNumberSetting).State = EntityState.Modified;
             _entities.SaveChanges();
         }

         public void AddOrderItem(OrderItem orderItem)
         {
             var order = _entities.Orders.FirstOrDefault(o => o.OrderId == orderItem.OrderId);
             order.OrderTotal += orderItem.OrderItemTotal;
             _entities.OrderItems.Add(orderItem);
             _entities.Entry<Order>(order).State = EntityState.Modified;
             _entities.SaveChanges();
         }
   } 

}
