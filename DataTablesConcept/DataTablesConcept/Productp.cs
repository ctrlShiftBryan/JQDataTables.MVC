using System;
using System.ComponentModel.DataAnnotations;

namespace DataTablesConcept
{
    [MetadataTypeAttribute(typeof(Meta.Product))]
    public partial class Product
    {
        
        public String ProductSubcategoryName
        {
            get
            {
                return 
                    ProductSubcategory == null ? "" :

                    ProductSubcategory.Name
                    ;
            }
            
        }

        public String ProductCategoryName
        {
            get
            {
                return
                    ProductSubcategory == null 
                    || ProductSubcategory.ProductCategory == null ? "" :

                    ProductSubcategory.ProductCategory.Name
                    ;
            }
            
        }
    }
}