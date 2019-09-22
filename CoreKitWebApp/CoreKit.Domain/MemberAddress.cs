namespace CoreKit.Domain
{
    public class MemberAddress
    {
        public int Id { get; set; } // Id (Primary key)
        public int MemberId { get; set; } // MemberId
        public int AddressId { get; set; } // AddressId

        // Foreign keys

        /// <summary>
        /// Parent Address pointed by [MemberAddress].([AddressId]) (FK_CustomerAddress_Address)
        /// </summary>
        public virtual Address Address { get; set; } // FK_CustomerAddress_Address

        /// <summary>
        /// Parent Member pointed by [MemberAddress].([MemberId]) (FK_CustomerAddress_Customer)
        /// </summary>
        public virtual Member Member { get; set; } // FK_CustomerAddress_Customer
    }

}
