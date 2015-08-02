using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineOrderingBusiness.Repositories
{
    public class ProductRepository 
     { 
         private OnlineOrderingEntities _entities = new OnlineOrderingEntities();

        public List<Product> GetProducts()
        {
            return _entities.Products.ToList();
        }

        public Product GetProduct(Guid productId)
        {
            return _entities.Products.FirstOrDefault(p => p.ProductId == productId);
        }
     } 

}
