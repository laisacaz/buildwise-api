using Microsoft.AspNetCore.Mvc;

namespace BuildWise.Entities
{
    public class ServiceOrder
    {
        public int Id { get; set; } 
        public DateTime CreatedAt { get; set; }
        public string Description { get; set; } = string.Empty;
        public bool Status { get; set; }
        public decimal Price { get; set; }  
    }
}
