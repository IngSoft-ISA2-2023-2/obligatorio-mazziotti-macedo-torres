using Microsoft.EntityFrameworkCore;
using PharmaGo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PharmaGo.DataAccess.Repositories
{
    public class PurchasesProductDetailRepository : BaseRepository<PurchaseProductDetail>
    {
        private readonly PharmacyGoDbContext _context;

        public PurchasesProductDetailRepository(PharmacyGoDbContext context) : base(context)
        {
            _context = context;
        }

        public override bool Exists(PurchaseProductDetail element)
        {
            bool exists = false;
            exists = _context.Set<PurchaseProductDetail>().Any<PurchaseProductDetail>(e => e.Id == element.Id);
            return exists;
        }

        public override PurchaseProductDetail GetOneByExpression(Expression<Func<PurchaseProductDetail, bool>> expression)
        {
            return _context.Set<PurchaseProductDetail>()
                .Include(x => x.Pharmacy)
                .FirstOrDefault(expression);
        }
    }
}
