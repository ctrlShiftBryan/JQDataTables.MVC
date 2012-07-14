using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using DataTablesHelper;

namespace DataTablesConcept
{
    [MetadataTypeAttribute(typeof(Meta.Product))]
    public partial class Product
    {
        
        public string Name2 { get; set; }

        public String ProductSubcategoryName
        {
            get
            {

                return 
                    ProductSubcategory == null ? "" :
                    this.ProductSubcategory.Name
                    ;
            }
            set
            {
                this.ProductSubcategory.Name = value;
            }
        }
    }
}