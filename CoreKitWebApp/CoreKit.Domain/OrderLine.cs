namespace CoreKit.Domain
{
    // OrderLine
    public class OrderLine
    {
        public int Id { get; set; } // Id (Primary key)
        public int OrderId { get; set; } // OrderId
        public int ProductId { get; set; } // ProductId
        public int Quantity { get; set; } // Quantity
        public decimal? UnitPrice { get; set; } // UnitPrice
        public System.DateTime? CheckinDate { get; set; } // CheckinDate
        public System.DateTime? CheckinTime { get; set; } // CheckinTime
        public System.DateTime? CheckoutDate { get; set; } // CheckoutDate
        public System.DateTime? CheckoutTime { get; set; } // CheckoutTime
        public string Vendor { get; set; } // Vendor (length: 100)
        public string Note { get; set; } // Note (length: 4000)
        public string AttributeData { get; set; } // AttributeData

        // Foreign keys

        /// <summary>
        /// Parent Order pointed by [OrderLine].([OrderId]) (FK_OrderLine_Order)
        /// </summary>
        public virtual Order Order { get; set; } // FK_OrderLine_Order

        /// <summary>
        /// Parent Product pointed by [OrderLine].([ProductId]) (FK_OrderLine_Product)
        /// </summary>
        public virtual Product Product { get; set; } // FK_OrderLine_Product

        public OrderLine()
        {
        }
    }

}
