
using System.Reflection.Metadata.Ecma335;

namespace Domain.Entities.ProductModule
{
    public class Product:BaseEntity<int>
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string PictureUrl { get; set; } = null!;
        public decimal Price { get; set; }

        //1-M ProductType
        public ProductType ProductType { get; set; }  //Navigational Property.
        public int TypeId { get; set; }   //fk
        // 1-M ProductBrand
        public ProductBrand ProductBrand { get; set; }
        public int BrandId { get; set; }
        // here it can't be recognized by Convension so i must do Configs and i'll understand him the 1-M relation in the configuration class.
    }
}
