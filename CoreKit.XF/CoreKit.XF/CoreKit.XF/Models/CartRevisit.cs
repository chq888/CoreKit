namespace CoreKit.Domain
{
    // CartRevisit
    public class CartRevisit
    {
        public int Id { get; set; } // Id (Primary key)
        public int? CartId { get; set; } // CartId
        public string CartCookie { get; set; } // CartCookie (length: 4000)
        public System.DateTime? CartCookieExpire { get; set; } // CartCookieExpire
    }

}
