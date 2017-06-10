using DAL.Interface.DTO;


namespace DAL.Interface.Interfaces
{
    public interface IProfileRepository : IRepository<DalProfile>
    {
        void UpdateUserId(DalProfile entity, int id);
    }
}
