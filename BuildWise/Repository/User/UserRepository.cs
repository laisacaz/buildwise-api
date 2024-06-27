using BuildWise.DTO.ServiceOrder;
using BuildWise.DTO;
using BuildWise.Interfaces.Repository.User;
using static BuildWise.Interface.DbConnection.IBaseConnection;
using BuildWise.DTO.Cashier;
using Dapper;

namespace BuildWise.Repository.User
{
    public class UserRepository : BaseRepository<Entities.User>, IUserRepository
    {
        private readonly IPublicConnection _conn;
        public UserRepository(IPublicConnection conn) : base(conn.GetConnection(), conn.GetTransaction())
        {
            _conn = conn;
        }

        public async Task<Entities.User?> GetUserByEmailAndPassword(
            string email,
            string password)
        {
            string sql = @"SELECT  
                                id as Id,
                                name as Name,
                                email as Email,
                                password as Password,
                                registered_number as RegisteredNumber,
                                created_at as CreatedAt 
                                FROM public.user WHERE email = @email AND password = @password";

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("email", email);
            parameters.Add("password", password);

            Entities.User? user = await _conn.GetConnection().QueryFirstOrDefaultAsync<Entities.User>(sql, parameters, _conn.GetTransaction());
            return user ?? null;
        }

    }
}
