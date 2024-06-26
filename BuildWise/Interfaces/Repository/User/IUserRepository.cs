﻿using BuildWise.Interface.Repository;
using static Dapper.SqlMapper;

namespace BuildWise.Interfaces.Repository.User
{
    public interface IUserRepository : IBaseRepository<Entities.User>
    {
        Task<Entities.User?> GetUserByEmailAndPassword(
            string email,
            string password);
    }
}
