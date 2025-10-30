using Domain.Entities.ProductModule;
using Shared;
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
        public ProductWithBrandAndTypeSpecifications(ProductSpecificationParameters parameters)
            :base(p => (!parameters.TypeId.HasValue || parameters.TypeId == p.TypeId)&& 
                       (!parameters.BrandId.HasValue || parameters.BrandId == p.BrandId)&&
            //دة كله معمول عشان اتجنب حوار ال null في حالة اني فلترت ب حاجة واحدة بس منهم اضمن ان التانية راجعة ب true فالدنيا متضربش مني 
                       (string.IsNullOrEmpty(parameters.Search) || p.Name.ToLower().Contains(parameters.Search.ToLower())))
        {
            AddIncludes(p => p.ProductBrand);
            AddIncludes(p => p.ProductType);
            switch (parameters.sort)
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

            ApplyPagination(parameters.PageSize,parameters.PageIndex);
        }

        // for GetProductById (int id) -> (criteria & Include) 
        public ProductWithBrandAndTypeSpecifications(int id):base(p=> p.Id == id)  // id coming from the service.
        {
            AddIncludes(p => p.ProductBrand);
            AddIncludes(p => p.ProductType);
        }

    }
}
