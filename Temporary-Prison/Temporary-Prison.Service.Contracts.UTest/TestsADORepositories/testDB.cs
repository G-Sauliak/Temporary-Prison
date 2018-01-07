using Microsoft.VisualStudio.TestTools.UnitTesting;
using Temporary_Prison.Common.Entities;
using Temporary_Prison.Service.Contracts.Contracts;

namespace Temporary_Prison.Service.Contracts.UTest.TestsADORepositories
{
    [TestClass]
    class testDB
    {
        private DataAccessService db;

        [TestInitialize]
        public void testDBb()
        {
            db = new DataAccessService();
        }
    

        [TestMethod]
        public void insertEmployee()
        {
            int id = 0;
            var emp = new Employee()
            {
                FirstName = "vasya",
                Position = "svarshik",
                LastName = "Samec",
                Surname = "Eduardovich"
            };

            db.ExecNonQuery("insertEmployee", "employeeID", out id);
            Assert.IsTrue(id > 0);
        }
    }
}
