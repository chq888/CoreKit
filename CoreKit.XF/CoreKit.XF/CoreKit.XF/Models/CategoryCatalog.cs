namespace CoreKit.XF.Models
{

    // CategoryCatalog
    public class CategoryCatalog
    {
        public int Id { get; set; } // Id (Primary key)
        public int? SourceCategoryId { get; set; } // SourceCategoryId
        public int CategoryId { get; set; } // CategoryId
        public int CatalogId { get; set; } // CatalogId

        // Foreign keys

        /// <summary>
        /// Parent Catalog pointed by [CategoryCatalog].([CatalogId]) (FK_CategoryCatalog_Catalog)
        /// </summary>
        public virtual Catalog Catalog { get; set; } // FK_CategoryCatalog_Catalog

        /// <summary>
        /// Parent Category pointed by [CategoryCatalog].([CategoryId]) (FK_CategoryCatalog_Category)
        /// </summary>
        public virtual Category Category_CategoryId { get; set; } // FK_CategoryCatalog_Category

        /// <summary>
        /// Parent Category pointed by [CategoryCatalog].([SourceCategoryId]) (FK_CategoryCatalog_Category2)
        /// </summary>
        public virtual Category SourceCategory { get; set; } // FK_CategoryCatalog_Category2
    }

}
