
namespace CoreKit.XF.Models
{
    // CartItem
    public class CartItem
    {
        public int Id { get; set; } // Id (Primary key)
        public int CartId { get; set; } // CartId
        public int MemberId { get; set; } // MemberId
        public string ProductId { get; set; } // ProductId (length: 1000)
        public int Quantity { get; set; } // Quantity
        public decimal? Price { get; set; } // Price
        public System.DateTime? SelectedDate { get; set; } // SelectedDate
        public System.DateTime? UpdatedDate { get; set; } // UpdatedDate
        public string UpdatedBy { get; set; } // UpdatedBy (length: 500)
        public System.DateTime? CreatedDate { get; set; } // CreatedDate
        public string CreatedBy { get; set; } // CreatedBy (length: 500)
    }

}
