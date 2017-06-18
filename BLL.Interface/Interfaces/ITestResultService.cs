using BLL.Interface.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface.Interfaces
{
    public interface ITestResultService : IService<BllTestResult>
    {
        bool CheckTime(TimeSpan timeLimit, TimeSpan userTime);
        bool CheckPercentAnswers(IEnumerable<bool> results, double minToSuccess);
        double GetPercentGoodAnswers(IEnumerable<bool> results);
    }
}
