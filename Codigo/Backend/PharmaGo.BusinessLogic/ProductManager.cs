﻿using PharmaGo.Domain.Entities;
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

        public Product Update(int id, Product product)
        {
            if (product == null)
                throw new InvalidResourceException("Mandatory information is missing.");

            product.ValidOrFail();

            Product productSaved = _productRepository.GetOneByExpression(p => p.Id == id);

            if (productSaved == null)
                throw new ResourceNotFoundException("The product does not exist.");

            if (_productRepository.GetOneByExpression(p => p.Code == product.Code && p.Code != productSaved.Code && p.Pharmacy.Id == productSaved.Pharmacy.Id) != null)
                throw new ResourceNotFoundException("The new product code already exists in that pharmacy.");

            productSaved.Code = product.Code;
            productSaved.Name = product.Name;
            productSaved.Description = product.Description;
            productSaved.Price = product.Price;

            _productRepository.UpdateOne(productSaved);
            _productRepository.Save();

            return productSaved;
        }
    }
}
