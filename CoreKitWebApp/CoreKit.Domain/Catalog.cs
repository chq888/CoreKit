namespace CoreKit.Domain
{

    public class Catalog
    {
        public int Id { get; set; } // Id (Primary key)
        public System.Guid? IdKey { get; set; } // IdKey
        public string Name { get; set; } // Name (length: 128)
        public string Note { get; set; } // Note (length: 1000)
        public int? ParentId { get; set; } // ParentId
        public int? Level { get; set; } // Level
        public string Path { get; set; } // Path (length: 128)
        public string Image { get; set; } // Image (length: 1000)
        public string Link { get; set; } // Link (length: 1000)
        public string Type { get; set; } // Type (length: 1000)
        public int? Order { get; set; } // Order
        public int? LanguageId { get; set; } // LanguageId
        public string LanguageCode { get; set; } // LanguageCode (length: 100)
        public bool? IsActived { get; set; } // IsActived
        public bool? IsDeleted { get; set; } // IsDeleted
        public System.DateTime? UpdatedDate { get; set; } // UpdatedDate
        public string UpdatedBy { get; set; } // UpdatedBy (length: 500)
        public System.DateTime? CreatedDate { get; set; } // CreatedDate
        public string CreatedBy { get; set; } // CreatedBy (length: 500)

        // Reverse navigation

        /// <summary>
        /// Child Catalogs where [Catalog].[ParentId] point to this entity (FK_Catalog_Catalog)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<Catalog> Catalogs { get; set; } // Catalog.FK_Catalog_Catalog
        /// <summary>
        /// Child CategoryCatalogs where [CategoryCatalog].[CatalogId] point to this entity (FK_CategoryCatalog_Catalog)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<CategoryCatalog> CategoryCatalogs { get; set; } // CategoryCatalog.FK_CategoryCatalog_Catalog
        /// <summary>
        /// Child Products where [Product].[CatalogId] point to this entity (FK_Product_Catalog)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<Product> Products { get; set; } // Product.FK_Product_Catalog

        // Foreign keys

        /// <summary>
        /// Parent Catalog pointed by [Catalog].([ParentId]) (FK_Catalog_Catalog)
        /// </summary>
        public virtual Catalog Parent { get; set; } // FK_Catalog_Catalog

        public Catalog()
        {
            Catalogs = new System.Collections.Generic.List<Catalog>();
            CategoryCatalogs = new System.Collections.Generic.List<CategoryCatalog>();
            Products = new System.Collections.Generic.List<Product>();
        }
    }

}
