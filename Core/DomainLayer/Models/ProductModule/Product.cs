using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models.ProductModule
{
    public class Product : BaseEntity<int>
    {
        public string Name { get; set; }=null!;
        public decimal Price { get; set; }
        public string Description { get; set; }=null!;

        public string PictureUrl { get; set; }=null!;


        #region product brand 
        public int BrandId { get; set; }
        public ProductBrand ProductBrand { get; set; }
        #endregion


        #region product Tybe
        public int TybeId { get; set; }
        public ProductTybe ProductTybe { get; set; }
        #endregion

    }
}
