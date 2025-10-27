using Domain.Entities.ProductModule;
using Shared.Enums;
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
        public ProductWithBrandAndTypeSpecifications(int? typeId,int? brandId,ProductSortingOptions sort)
            :base(p => (!typeId.HasValue || typeId == p.TypeId)&& 
                       (!brandId.HasValue || brandId == p.BrandId))
            //دة كله معمول عشان اتجنب حوار ال null في حالة اني فلترت ب حاجة واحدة بس منهم اضمن ان التانية راجعة ب true فالدنيا متضربش مني 
        {
            AddIncludes(p => p.ProductBrand);
            AddIncludes(p => p.ProductType);
            switch (sort)
            {
                case ProductSortingOptions.NameAsc:
                    AddOrderBy(p => p.Name);
                    break;

                case ProductSortingOptions.NameDesc:
                    AddOrderByDescending(p => p.Name);
                    break;

                case ProductSortingOptions.PriceAsc:
                    AddOrderBy(p => p.Price);
                    break;

                case ProductSortingOptions.PriceDesc:
                    AddOrderByDescending(p => p.Price);
                    break;

                default:
                    break;
            }


        }

        // for GetProductById (int id) -> (criteria & Include) 
        public ProductWithBrandAndTypeSpecifications(int id):base(p=> p.Id == id)  // id coming from the service.
        {
            AddIncludes(p => p.ProductBrand);
            AddIncludes(p => p.ProductType);
        }

    }
}
