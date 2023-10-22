using PharmaGo.Domain.Entities;
using PharmaGo.Exceptions;
using PharmaGo.IBusinessLogic;
using PharmaGo.IDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmaGo.BusinessLogic
{
    public class ProductManager : IProductManager
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<Pharmacy> _pharmacyRepository;
        private readonly IRepository<Session> _sessionRepository;
        private readonly IRepository<User> _userRepository;

        public ProductManager(IRepository<Product> productRepository, 
                              IRepository<Pharmacy> pharmacyRepository,
                              IRepository<Session> sessionRepository,
                              IRepository<User> userRepository)
        {
            _productRepository = productRepository;
            _pharmacyRepository = pharmacyRepository;
            _sessionRepository = sessionRepository;
            _userRepository = userRepository;
        }

        public IEnumerable<Product> GetAll()
        {
            return _productRepository.GetAllByExpression(t => true);
        }

        public Product GetById(int id)
        {
            Product retrievedProduct = _productRepository.GetOneByExpression(p => p.Id == id);
            if (retrievedProduct == null)
            {
                throw new ResourceNotFoundException("The drug does not exist.");
            }

            return retrievedProduct;
        }

        public Product Create(Product product, string token)
        {
            product.ValidOrFail();

            Guid guidToken = new Guid(token);
            Session session = _sessionRepository.GetOneByExpression(s => s.Token == guidToken);
            int userId = session.UserId;
            User user = _userRepository.GetOneDetailByExpression(u => u.Id == userId);

            Pharmacy pharmacyOfProduct = _pharmacyRepository.GetOneByExpression(p => p.Name == user.Pharmacy.Name);
            if (pharmacyOfProduct == null)
                throw new ResourceNotFoundException("The pharmacy of the product does not exist.");
            
            if (_productRepository.Exists(p => p.Code == product.Code && p.Pharmacy.Name == pharmacyOfProduct.Name))
                throw new InvalidResourceException("The product already exists in that pharmacy.");

            product.Pharmacy.Id = pharmacyOfProduct.Id;
            _productRepository.InsertOne(product);
            _productRepository.Save();
            return product;
        }

        public void Delete(int id)
        {
            var productSaved = _productRepository.GetOneByExpression(d => d.Id == id);
            if (productSaved == null)
            {
                throw new ResourceNotFoundException("The drug to delete does not exist.");
            }
            productSaved.Deleted = true;
            _productRepository.UpdateOne(productSaved);
            _productRepository.Save();
        }

        public IEnumerable<Product> GetAllByUser(string token)
        {
            var guidToken = new Guid(token);
            Session session = _sessionRepository.GetOneByExpression(s => s.Token == guidToken);
            var userId = session.UserId;
            User user = _userRepository.GetOneDetailByExpression(u => u.Id == userId);
            Pharmacy pharmacy = user.Pharmacy;
            return _productRepository.GetAllByExpression(d => d.Deleted == false && d.Pharmacy.Id == pharmacy.Id);
        }
    }
}
