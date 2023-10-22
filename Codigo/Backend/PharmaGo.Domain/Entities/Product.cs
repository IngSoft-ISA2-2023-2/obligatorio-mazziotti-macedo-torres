using PharmaGo.Domain.Entities;
using PharmaGo.Exceptions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PharmaGo.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public Pharmacy? Pharmacy { get; set; }
        public bool Deleted { get; set; }

        public void ValidOrFail()
        {
            if (string.IsNullOrEmpty(Code) || string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Description)
                    || Price <= 0 || Pharmacy == null)
                throw new InvalidResourceException("Mandatory information is missing.");
            if (!Regex.IsMatch(Code, @"^\d{5}$"))
                throw new InvalidResourceException("The product code is invalid.");
            if (Name.Length > 30)
                throw new InvalidResourceException("The product name is too long.");
            if (Description.Length > 70)
                throw new InvalidResourceException("The product description is too long.");
        }
    }
}