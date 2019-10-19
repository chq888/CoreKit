namespace CoreKit.XF.Models
{
    // Order
    public class Order
    {
        public int Id { get; set; } // Id (Primary key)
        public System.Guid? IdKey { get; set; } // IdKey
        public string Code { get; set; } // Code (length: 100)
        public int MemberId { get; set; } // MemberId
        public int? ShippingAddressId { get; set; } // ShippingAddressId
        public string PurchaseOrderNo { get; set; } // PurchaseOrderNo (length: 1000)
        public string SalesOrderNo { get; set; } // SalesOrderNo (length: 1000)
        public System.DateTime? CheckinDate { get; set; } // CheckinDate
        public System.DateTime? CheckinTime { get; set; } // CheckinTime
        public System.DateTime? CheckoutDate { get; set; } // CheckoutDate
        public System.DateTime? CheckoutTime { get; set; } // CheckoutTime
        public System.DateTime? DueDate { get; set; } // DueDate
        public int? PromotionId { get; set; } // PromotionId
        public decimal? Discount { get; set; } // Discount
        public decimal? PromoDiscount { get; set; } // PromoDiscount
        public decimal? SubTotal { get; set; } // SubTotal
        public decimal? Total { get; set; } // Total
        public string Note { get; set; } // Note (length: 4000)
        public int? Source { get; set; } // Source
        public int? Status { get; set; } // Status
        public bool? IsActived { get; set; } // IsActived
        public bool? IsDeleted { get; set; } // IsDeleted
        public System.DateTime? UpdatedDate { get; set; } // UpdatedDate
        public string UpdatedBy { get; set; } // UpdatedBy (length: 500)
        public System.DateTime? CreatedDate { get; set; } // CreatedDate
        public string CreatedBy { get; set; } // CreatedBy (length: 500)

        // Reverse navigation

        ///// <summary>
        ///// Child Shipments where [Shipment].[OrderId] point to this entity (FK_Shipment_Order)
        ///// </summary>
        //public virtual System.Collections.Generic.ICollection<Shipment> Shipments { get; set; } // Shipment.FK_Shipment_Order

        // Foreign keys

        /// <summary>
        /// Parent Address pointed by [Order].([ShippingAddressId]) (FK_Order_Address)
        /// </summary>
        public virtual Address Address { get; set; } // FK_Order_Address

        /// <summary>
        /// Parent Member pointed by [Order].([MemberId]) (FK_Order_Member)
        /// </summary>
        public virtual Member Member { get; set; } // FK_Order_Member

        public Order()
        {
            //Shipments = new System.Collections.Generic.List<Shipment>();
        }
    }

}
