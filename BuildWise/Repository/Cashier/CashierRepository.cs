using BuildWise.DTO.Cashier;
using BuildWise.Interfaces.Repository.Cashier;
using BuildWise.Interfaces.Repository.Construction;
using Dapper;
using static BuildWise.Enum.SaleEnum;
using static BuildWise.Interface.DbConnection.IBaseConnection;
using static Dapper.SqlMapper;

namespace BuildWise.Repository.Cashier
{
    public class CashierRepository : BaseRepository<Entities.Cashier>, ICashierRepository
    {
        private readonly IClientConnection _conn;
        public CashierRepository(IClientConnection conn) : base(conn.GetConnection(), conn.GetTransaction())
        {
            _conn = conn;
        }

        public async Task<decimal?> GetAmountPaidByReceivementMethod(
            DateTime startDate,
            DateTime endDate,
            ESaleReceivementMethod receivementMethodId)
        {
            DynamicParameters parameters = new();

            string sql = @" Select
                                sum(client.sale.paid_amount)
                            From client.sale
                            Where client.sale.receivement_method = @receivementMethodId
                            and client.sale.finalized_at between @startDate and @endDate";

            parameters.Add("startDate", startDate);
            parameters.Add("endDate", endDate);
            parameters.Add("receivementMethodId", receivementMethodId);

            decimal? total = await _conn.GetConnection().QueryFirstOrDefaultAsync<decimal?>(sql, parameters, _conn.GetTransaction());
            return total??null;
        }

        public async Task<CashierSimpleDTO> GetLastCashier()
        {
            string sql = "SELECT client.ca_cashier.id as Id, client.ca_cashier.status as IsOpen FROM client.ca_cashier ORDER BY id DESC LIMIT 1";
            CashierSimpleDTO? cashier = await _conn.GetConnection().QueryFirstOrDefaultAsync<CashierSimpleDTO>(sql, _conn.GetTransaction());
            return cashier;
        }
    }
}
