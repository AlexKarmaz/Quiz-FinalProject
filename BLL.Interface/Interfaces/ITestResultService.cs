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
        /// <summary>
        /// Verifies the time spent by the user and the time limit
        /// </summary>
        /// <param name="timeLimit">Time limit</param>
        /// <param name="userTime">Time spent by the user to pass the test</param>
        /// <returns>bool value</returns>
        bool CheckTime(TimeSpan timeLimit, TimeSpan userTime);
        /// <summary>
        /// Calculates the percentage of correct answers and compares them with the minimum required
        /// </summary>
        /// <param name="results">A collection of user responses</param>
        /// <param name="minToSuccess">The minimum percentage of correct answers</param>
        /// <returns>bool value</returns>
        bool CheckPercentAnswers(IEnumerable<bool> results, double minToSuccess);
        /// <summary>
        /// Calculates the percentage of correct user responses
        /// </summary>
        /// <param name="results">A collection of user responses</param>
        /// <returns>Percent of correct answers</returns>
        double GetPercentGoodAnswers(IEnumerable<bool> results);
    }
}
