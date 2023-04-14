namespace ETMP.Model
{
    public class Payment
    {
        public int Id { get; set; } 
        public string cType { get; set; }
        public  string username { get; set; }
        public int cardNo { get; set; }
        public int expiration { get; set; }
        public int CVV { get; set; }
    }
}
