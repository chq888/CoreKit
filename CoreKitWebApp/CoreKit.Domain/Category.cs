namespace CoreKit.Domain
{
    // Category
    public class Category
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
        /// Child Addresses where [Address].[CategoryId] point to this entity (FK_Address_Category)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<Address> Addresses { get; set; } // Address.FK_Address_Category
        /// <summary>
        /// Child Categories where [Category].[ParentId] point to this entity (FK_trCategories_ToTable)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<Category> Categories { get; set; } // Category.FK_trCategories_ToTable
        /// <summary>
        /// Child CategoryCatalogs where [CategoryCatalog].[CategoryId] point to this entity (FK_CategoryCatalog_Category)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<CategoryCatalog> CategoryCatalogs_CategoryId { get; set; } // CategoryCatalog.FK_CategoryCatalog_Category
        /// <summary>
        /// Child CategoryCatalogs where [CategoryCatalog].[SourceCategoryId] point to this entity (FK_CategoryCatalog_Category2)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<CategoryCatalog> CategoryCatalogs_SourceCategoryId { get; set; } // CategoryCatalog.FK_CategoryCatalog_Category2
        ///// <summary>
        ///// Child CustomAttributes where [CustomAttribute].[CategoryId] point to this entity (FK_CustomAttribute_Category)
        ///// </summary>
        //public virtual System.Collections.Generic.ICollection<CustomAttribute> CustomAttributes { get; set; } // CustomAttribute.FK_CustomAttribute_Category
        ///// <summary>
        ///// Child CustomProperties where [CustomProperty].[CategoryId] point to this entity (FK_dbo.tblDynamicProperty.tblCategories_CategoryId)
        ///// </summary>
        //public virtual System.Collections.Generic.ICollection<CustomProperty> CustomProperties { get; set; } // CustomProperty.FK_dbo.tblDynamicProperty.tblCategories_CategoryId
        /// <summary>
        /// Child Members where [Member].[CategoryId] point to this entity (FK_Member_Category)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<Member> Members { get; set; } // Member.FK_Member_Category
        /// <summary>
        /// Child Products where [Product].[CategoryId] point to this entity (FK_Product_Category)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<Product> Products { get; set; } // Product.FK_Product_Category
        ///// <summary>
        ///// Child Settings where [Setting].[CategoryId] point to this entity (FK_trSettings_trCategories)
        ///// </summary>
        //public virtual System.Collections.Generic.ICollection<Setting> Settings { get; set; } // Setting.FK_trSettings_trCategories

        // Foreign keys

        /// <summary>
        /// Parent Category pointed by [Category].([ParentId]) (FK_trCategories_ToTable)
        /// </summary>
        public virtual Category Parent { get; set; } // FK_trCategories_ToTable

        public Category()
        {
            Addresses = new System.Collections.Generic.List<Address>();
            Categories = new System.Collections.Generic.List<Category>();
            CategoryCatalogs_CategoryId = new System.Collections.Generic.List<CategoryCatalog>();
            CategoryCatalogs_SourceCategoryId = new System.Collections.Generic.List<CategoryCatalog>();
          
            Members = new System.Collections.Generic.List<Member>();
            Products = new System.Collections.Generic.List<Product>();

            //CustomAttributes = new System.Collections.Generic.List<CustomAttribute>();
            //CustomProperties = new System.Collections.Generic.List<CustomProperty>();
            //Settings = new System.Collections.Generic.List<Setting>();
        }
    }

}
