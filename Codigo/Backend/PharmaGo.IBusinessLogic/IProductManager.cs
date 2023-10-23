using PharmaGo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmaGo.IBusinessLogic
{
    public interface IProductManager
    {
        Product Create(Product product, string token);
        Product Update(int id, Product product);
        void Delete(int id);
        IEnumerable<Product> GetAll();
        Product GetById(int id);
        IEnumerable<Product> GetAllByUser(string token);
    }
}
