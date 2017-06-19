using BLL.Interface.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface.Interfaces
{
    public interface IQuestionService : IService<BllQuestion>
    {
        /// <summary>
        /// Creates a question and updates the test id
        /// </summary>
        /// <param name="entity">Test</param>
        /// <param name="testId">Test id</param>
        void CreateAndUpdateTestId(BllQuestion entity, int testId);
    }
}
