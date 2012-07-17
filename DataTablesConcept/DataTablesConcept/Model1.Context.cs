//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Data.Objects;
using System.Data.EntityClient;

namespace DataTablesConcept
{
    public partial class DB : ObjectContext
    {
        public const string ConnectionString = "name=DB";
        public const string ContainerName = "DB";
    
        #region Constructors
    
        public DB()
            : base(ConnectionString, ContainerName)
        {
            this.ContextOptions.LazyLoadingEnabled = true;
        }
    
        public DB(string connectionString)
            : base(connectionString, ContainerName)
        {
            this.ContextOptions.LazyLoadingEnabled = true;
        }
    
        public DB(EntityConnection connection)
            : base(connection, ContainerName)
        {
            this.ContextOptions.LazyLoadingEnabled = true;
        }
    
        #endregion
    
        #region ObjectSet Properties
    
        public ObjectSet<Product> Products
        {
            get { return _products  ?? (_products = CreateObjectSet<Product>("Products")); }
        }
        private ObjectSet<Product> _products;
    
        public ObjectSet<ProductCategory> ProductCategories
        {
            get { return _productCategories  ?? (_productCategories = CreateObjectSet<ProductCategory>("ProductCategories")); }
        }
        private ObjectSet<ProductCategory> _productCategories;
    
        public ObjectSet<ProductSubcategory> ProductSubcategories
        {
            get { return _productSubcategories  ?? (_productSubcategories = CreateObjectSet<ProductSubcategory>("ProductSubcategories")); }
        }
        private ObjectSet<ProductSubcategory> _productSubcategories;

        #endregion
    }
}