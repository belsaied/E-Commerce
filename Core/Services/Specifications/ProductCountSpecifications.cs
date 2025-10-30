using Domain.Entities.ProductModule;
using Shared;

namespace Services.Specifications
{
    public class ProductCountSpecifications:BaseSpecifications<Product,int> 
    {
        public ProductCountSpecifications(ProductSpecificationParameters parameters):base(p => (!parameters.TypeId.HasValue || parameters.TypeId == p.TypeId) &&
                       (!parameters.BrandId.HasValue || parameters.BrandId == p.BrandId) &&
                       //دة كله معمول عشان اتجنب حوار ال null في حالة اني فلترت ب حاجة واحدة بس منهم اضمن ان التانية راجعة ب true فالدنيا متضربش مني 
                       (string.IsNullOrEmpty(parameters.Search) || p.Name.ToLower().Contains(parameters.Search.ToLower())))
        {
            
        }
    }
}
