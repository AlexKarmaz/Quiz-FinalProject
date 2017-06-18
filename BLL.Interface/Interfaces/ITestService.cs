using BLL.Interface.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface.Interfaces
{
    public interface ITestService : IService<BllTest>
    {
        void DeleteTestQuestions(int testId);
        IEnumerable<BllTest> Search(string text);
        IEnumerable<bool> CheckAnswers(int testId, bool[][] results);
    }
}
