using DAL.Interface.DTO;


namespace DAL.Interface.Interfaces
{
    public interface IRoleRepository : IRepository<DalRole>
    {
        bool IsUserInRole(string userName, string roleName);
    }
}
