namespace CoreKit.Domain
{

    public class Member
    {
        public int Id { get; set; } // Id (Primary key)
        public System.Guid? IdKey { get; set; } // IdKey
        public string UserId { get; set; } // UserId (length: 128)
        public string FirstName { get; set; } // FirstName (length: 1000)
        public string LastName { get; set; } // LastName (length: 1000)
        public string Email { get; set; } // Email (length: 100)
        public string Identity { get; set; } // Identity (length: 100)
        public string Passport { get; set; } // Passport (length: 100)
        public string Gender { get; set; } // Gender (length: 100)
        public System.DateTime? BirthDate { get; set; } // BirthDate
        public string Phone { get; set; } // Phone (length: 1000)
        public string Fax { get; set; } // Fax (length: 1000)
        public int? AddressId { get; set; } // AddressId
        public string Address { get; set; } // Address (length: 1000)
        public string Address2 { get; set; } // Address2 (length: 1000)
        public int? Ward { get; set; } // Ward
        public int? District { get; set; } // District
        public int? Province { get; set; } // Province
        public int? StateProvince { get; set; } // StateProvince
        public int? Country { get; set; } // Country
        public string PostalCode { get; set; } // PostalCode (length: 500)
        public string Link { get; set; } // Link (length: 1000)
        public string Note { get; set; } // Note (length: 2000)
        public string Image { get; set; } // Image (length: 4000)
        public string Status { get; set; } // Status (length: 100)
        public int? CategoryId { get; set; } // CategoryId
        public string Type { get; set; } // Type (length: 100)
        public bool? IsActived { get; set; } // IsActived
        public bool? IsDeleted { get; set; } // IsDeleted
        public string LastIpAddress { get; set; } // LastIpAddress (length: 100)
        public System.DateTime? LastLoginDate { get; set; } // LastLoginDate
        public System.DateTime? LastActivityDate { get; set; } // LastActivityDate
        public System.DateTime? UpdatedDate { get; set; } // UpdatedDate
        public string UpdatedBy { get; set; } // UpdatedBy (length: 500)
        public System.DateTime? CreatedDate { get; set; } // CreatedDate
        public string CreatedBy { get; set; } // CreatedBy (length: 500)

        // Reverse navigation

        /// <summary>
        /// Child MemberAddresses where [MemberAddress].[MemberId] point to this entity (FK_CustomerAddress_Customer)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<MemberAddress> MemberAddresses { get; set; } // MemberAddress.FK_CustomerAddress_Customer
        /// <summary>
        /// Child Orders where [Order].[MemberId] point to this entity (FK_Order_Member)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<Order> Orders { get; set; } // Order.FK_Order_Member
        ///// <summary>
        ///// Child Vendors where [Vendor].[MemberId] point to this entity (FK_Vendor_Member)
        ///// </summary>
        //public virtual System.Collections.Generic.ICollection<Vendor> Vendors { get; set; } // Vendor.FK_Vendor_Member

        // Foreign keys

        /// <summary>
        /// Parent Address pointed by [Member].([AddressId]) (FK_Member_Address)
        /// </summary>
        public virtual Address Address_AddressId { get; set; } // FK_Member_Address

        /// <summary>
        /// Parent Category pointed by [Member].([CategoryId]) (FK_Member_Category)
        /// </summary>
        public virtual Category Category { get; set; } // FK_Member_Category

        ///// <summary>
        ///// Parent Country pointed by [Member].([Country]) (FK_Member_Country)
        ///// </summary>
        //public virtual Country Country_Country { get; set; } // FK_Member_Country

        ///// <summary>
        ///// Parent District pointed by [Member].([District]) (FK_Member_District)
        ///// </summary>
        //public virtual District District_District { get; set; } // FK_Member_District

        ///// <summary>
        ///// Parent Province pointed by [Member].([Province]) (FK_Member_Province)
        ///// </summary>
        //public virtual Province Province_Province { get; set; } // FK_Member_Province

        ///// <summary>
        ///// Parent Ward pointed by [Member].([Ward]) (FK_Member_Ward)
        ///// </summary>
        //public virtual Ward Ward_Ward { get; set; } // FK_Member_Ward

        public Member()
        {
            MemberAddresses = new System.Collections.Generic.List<MemberAddress>();
            Orders = new System.Collections.Generic.List<Order>();
            //Vendors = new System.Collections.Generic.List<Vendor>();
        }
    }

}
