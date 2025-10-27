using Domain.Entities.ProductModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    public class ProductWithBrandAndTypeSpecifications:BaseSpecifications<Product,int>
    {
        // for GetAll (i just need includes not where -> [Criteria] so i set it as null)
        public ProductWithBrandAndTypeSpecifications():base(null)
        {
            AddIncludes(p => p.ProductBrand);
            AddIncludes(p => p.ProductType);
        }

        // for GetProductById (int id) -> (criteria & Include) 
        public ProductWithBrandAndTypeSpecifications(int id):base(p=> p.Id == id)  // id coming from the service.
        {
            AddIncludes(p => p.ProductBrand);
            AddIncludes(p => p.ProductType);
        }

    }
}
