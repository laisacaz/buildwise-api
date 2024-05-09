﻿using BuildWise.Extensions;
using static BuildWise.Enum.SaleEnum;

namespace BuildWise.Entities
{
    public class Sale
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int SellerId { get; set; }
        public ESaleStatus Status { get; set;}
        public int? ConstructionId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime CanceledAt { get; set; }
        public DateTime FinalizedAt { get; set; }
        public DateTime ReceivedAt { get; set; }
        public ESaleReceivementMethod ReceivementMethod { get; set; }
        public decimal Total { get; set; }  
        public decimal TotalItems { get; set; }  
        public decimal Discount { get; set; }  
        public decimal Increase { get; set; }  
        public decimal Subtotal { get; set; }  
        public decimal PaidAmount { get; set; }
        public string? Observation { get; set; }

    }
}