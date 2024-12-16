using BuildWise.DTO.Sale;
using BuildWise.DTO.ServiceOrder;

namespace BuildWise.DTO.Cashier
{
    public class CashierGetValuesDTO
    {
        public CashierGetValuesDTO()
        {
            Values = new CashierReceivementMethodsDTO()
            {
                Money = 0,
            };
        }
        public CashierReceivementMethodsDTO Values { get; set; }
        public DateTime OpeningDate { get; set; }   
        public DateTime ClosedDate { get; set;}

    }
}
