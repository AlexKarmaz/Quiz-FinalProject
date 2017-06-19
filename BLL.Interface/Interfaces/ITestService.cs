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
        /// <summary>
        /// Removes all test questions
        /// </summary>
        /// <param name="testId">Test id</param>
        void DeleteTestQuestions(int testId);
        /// <summary>
        /// Looks for all tests on request
        /// </summary>
        /// <param name="searchString">Search request</param>
        /// <returns>Test collection</returns>
        IEnumerable<BllTest> Search(string text);
        /// <summary>
        /// Checks user answers
        /// </summary>
        /// <param name="testId">Test id</param>
        /// <param name="testResults">An array of user responses</param>
        /// <returns>A collection of checked questions</returns>
        IEnumerable<bool> CheckAnswers(int testId, bool[][] results);
        /// <summary>
        /// Checks the readiness of the test
        /// </summary>
        /// <param name="testId">Test id</param>
        /// <returns>bool value, is the test ready or not</returns>
        bool IsTestReady(int testId);
    }
}
