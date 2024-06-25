using BuildWise.DTO.Cashier;
using BuildWise.Interface.Repository;
using static BuildWise.Enum.SaleEnum;

namespace BuildWise.Interfaces.Repository.Cashier
{
    public interface ICashierRepository : IBaseRepository<Entities.Cashier>
    {
       Task<decimal?> GetAmountPaidByReceivementMethod(
           DateTime startDate,
           DateTime endDate,
           ESaleReceivementMethod receivementMethodId);

        Task<CashierSimpleDTO> GetLastCashier();
    }
}
