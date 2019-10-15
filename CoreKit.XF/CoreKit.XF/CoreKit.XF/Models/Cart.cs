namespace CoreKit.Domain
{

    // Cart
    public class Cart
    {
        public int Id { get; set; } // Id (Primary key)
        public int MemberId { get; set; } // MemberId
        public System.DateTime? ExpiredDate { get; set; } // ExpiredDate
        public System.DateTime? InitializedDate { get; set; } // InitializedDate
        public string SourceUrl { get; set; } // SourceUrl (length: 10)
        public System.DateTime? UpdatedDate { get; set; } // UpdatedDate
        public string UpdatedBy { get; set; } // UpdatedBy (length: 500)
        public System.DateTime? CreatedDate { get; set; } // CreatedDate
        public string CreatedBy { get; set; } // CreatedBy (length: 500)
    }

}
