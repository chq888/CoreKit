namespace CoreKit.Domain
{

    // Address
    public class Address
    {
        public int Id { get; set; } // Id (Primary key)
        public System.Guid? IdKey { get; set; } // IdKey
        public string FirstName { get; set; } // FirstName (length: 1000)
        public string LastName { get; set; } // LastName (length: 1000)
        public string Salutation { get; set; } // Salutation (length: 1000)
        public string Email { get; set; } // Email (length: 1000)
        public string Title { get; set; } // Title (length: 1000)
        public string Organization { get; set; } // Organization (length: 1000)
        public string Phone { get; set; } // Phone (length: 2000)
        public string Fax { get; set; } // Fax (length: 2000)
        public string Address_ { get; set; } // Address (length: 1000)
        public string Address2 { get; set; } // Address2 (length: 1000)
        public int? Ward { get; set; } // Ward
        public int? District { get; set; } // District
        public int? Province { get; set; } // Province
        public string StateProvince { get; set; } // StateProvince (length: 500)
        public int? Country { get; set; } // Country
        public string PostalCode { get; set; } // PostalCode (length: 500)
        public string Link { get; set; } // Link (length: 1000)
        public string Note { get; set; } // Note (length: 2000)
        public string Status { get; set; } // Status (length: 500)
        public int? CategoryId { get; set; } // CategoryId
        public string Type { get; set; } // Type (length: 100)
        public bool? IsActived { get; set; } // IsActived
        public bool? IsDeleted { get; set; } // IsDeleted
        public System.DateTime? UpdatedDate { get; set; } // UpdatedDate
        public string UpdatedBy { get; set; } // UpdatedBy (length: 500)
        public System.DateTime? CreatedDate { get; set; } // CreatedDate
        public string CreatedBy { get; set; } // CreatedBy (length: 500)

        // Reverse navigation

        /// <summary>
        /// Child Members where [Member].[AddressId] point to this entity (FK_Member_Address)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<Member> Members { get; set; } // Member.FK_Member_Address
        /// <summary>
        /// Child MemberAddresses where [MemberAddress].[AddressId] point to this entity (FK_CustomerAddress_Address)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<MemberAddress> MemberAddresses { get; set; } // MemberAddress.FK_CustomerAddress_Address
        /// <summary>
        /// Child Orders where [Order].[ShippingAddressId] point to this entity (FK_Order_Address)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<Order> Orders { get; set; } // Order.FK_Order_Address

        // Foreign keys

        /// <summary>
        /// Parent Category pointed by [Address].([CategoryId]) (FK_Address_Category)
        /// </summary>
        public virtual Category Category { get; set; } // FK_Address_Category

        ///// <summary>
        ///// Parent Country pointed by [Address].([Country]) (FK_Address_Country)
        ///// </summary>
        //public virtual Country Country_Country { get; set; } // FK_Address_Country

        ///// <summary>
        ///// Parent District pointed by [Address].([District]) (FK_Address_District)
        ///// </summary>
        //public virtual District District_District { get; set; } // FK_Address_District

        ///// <summary>
        ///// Parent Province pointed by [Address].([Province]) (FK_Address_Province)
        ///// </summary>
        //public virtual Province Province_Province { get; set; } // FK_Address_Province

        ///// <summary>
        ///// Parent Ward pointed by [Address].([Ward]) (FK_Address_Ward)
        ///// </summary>
        //public virtual Ward Ward_Ward { get; set; } // FK_Address_Ward

        public Address()
        {
            Members = new System.Collections.Generic.List<Member>();
            MemberAddresses = new System.Collections.Generic.List<MemberAddress>();
            Orders = new System.Collections.Generic.List<Order>();
        }
    }

}
