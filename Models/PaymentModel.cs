﻿namespace ETMP.Models
{
    public class PaymentModel
    {
        public int Id { get; set; }
        public string? cType { get; set; }
        public string? username { get; set; }
        public long? cardNo { get; set; }
        public string? expiration { get; set; }
        public int? CVV { get; set; }

    }
}
