namespace CoreKit.XF.Models
{
    // Product
    public class Product
    {
        public int Id { get; set; } // Id (Primary key)
        public System.Guid? IdKey { get; set; } // IdKey
        public string Code { get; set; } // Code (length: 100)
        public string Sku { get; set; } // SKU (length: 1000)
        public string Name { get; set; } // Name (length: 2000)
        public string Summary { get; set; } // Summary (length: 2000)
        public string Content { get; set; } // Content (length: 4000)
        public string Note { get; set; } // Note (length: 2000)
        public decimal? Msrp { get; set; } // MSRP
        public decimal? Price { get; set; } // Price
        public decimal? OldPrice { get; set; } // OldPrice
        public decimal? SpecialPrice { get; set; } // SpecialPrice
        public System.DateTime? SpecialStartDate { get; set; } // SpecialStartDate
        public System.DateTime? SpecialEndDate { get; set; } // SpecialEndDate
        public bool? IsPriceCalled { get; set; } // IsPriceCalled
        public int? Quantity { get; set; } // Quantity
        public bool? IsAvailable { get; set; } // IsAvailable
        public string Image { get; set; } // Image (length: 4000)
        public int? ViewCount { get; set; } // ViewCount
        public System.DateTime? UpTopNew { get; set; } // UpTopNew
        public System.DateTime? UpTopHot { get; set; } // UpTopHot
        public int? CategoryId { get; set; } // CategoryId
        public int? CatalogId { get; set; } // CatalogId
        public string Type { get; set; } // Type (length: 100)
        public int? Status { get; set; } // Status
        public int? LanguageId { get; set; } // LanguageId
        public string LanguageCode { get; set; } // LanguageCode (length: 100)
        public int? Position { get; set; } // Position
        public int? Order { get; set; } // Order
        public System.DateTime? PublishedDate { get; set; } // PublishedDate
        public bool? IsPublished { get; set; } // IsPublished
        public bool? IsHightlight { get; set; } // IsHightlight
        public bool? IsVisible { get; set; } // IsVisible
        public bool? IsActived { get; set; } // IsActived
        public bool? IsDeleted { get; set; } // IsDeleted
        public System.DateTime? UpdatedDate { get; set; } // UpdatedDate
        public string UpdatedBy { get; set; } // UpdatedBy (length: 500)
        public System.DateTime? CreatedDate { get; set; } // CreatedDate
        public string CreatedBy { get; set; } // CreatedBy (length: 500)

        // Reverse navigation

        /// <summary>
        /// Child OrderLines where [OrderLine].[ProductId] point to this entity (FK_OrderLine_Product)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<OrderLine> OrderLines { get; set; } // OrderLine.FK_OrderLine_Product
        ///// <summary>
        ///// Child ProductCustomAttributes where [ProductCustomAttribute].[ProductId] point to this entity (FK_ProductCustomAttribute_Product)
        ///// </summary>
        //public virtual System.Collections.Generic.ICollection<ProductCustomAttribute> ProductCustomAttributes { get; set; } // ProductCustomAttribute.FK_ProductCustomAttribute_Product
        ///// <summary>
        ///// Child ProductLocations where [ProductLocation].[ProductId] point to this entity (FK_ProductLocation_Product)
        ///// </summary>
        //public virtual System.Collections.Generic.ICollection<ProductLocation> ProductLocations { get; set; } // ProductLocation.FK_ProductLocation_Product
        ///// <summary>
        ///// Child ProductVendors where [ProductVendor].[ProductId] point to this entity (FK_ProductVendor_Product)
        ///// </summary>
        //public virtual System.Collections.Generic.ICollection<ProductVendor> ProductVendors { get; set; } // ProductVendor.FK_ProductVendor_Product

        // Foreign keys

        /// <summary>
        /// Parent Catalog pointed by [Product].([CatalogId]) (FK_Product_Catalog)
        /// </summary>
        public virtual Catalog Catalog { get; set; } // FK_Product_Catalog

        /// <summary>
        /// Parent Category pointed by [Product].([CategoryId]) (FK_Product_Category)
        /// </summary>
        public virtual Category Category { get; set; } // FK_Product_Category

        public Product()
        {
            OrderLines = new System.Collections.Generic.List<OrderLine>();
            //ProductCustomAttributes = new System.Collections.Generic.List<ProductCustomAttribute>();
            //ProductLocations = new System.Collections.Generic.List<ProductLocation>();
            //ProductVendors = new System.Collections.Generic.List<ProductVendor>();
        }
    }

}
// </auto-generated>
