using DAL.Interface.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface.Interfaces
{
    public interface IQuestionRepository : IRepository<DalQuestion>
    {
        void CreateAndUpdateTestId(DalQuestion entity, int testId);
    }
}
