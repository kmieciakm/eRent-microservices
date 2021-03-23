using Database.Entities;

namespace Database.Repositories.Contracts
{
    interface IClientRepository
    {
        bool CreateAndSave(DbClientEntity clientEnt);
    }
}