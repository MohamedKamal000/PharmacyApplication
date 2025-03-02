using System;
using System.Data;
using DataAccess;
using Dapper;
using Moq;
using Xunit;

namespace DataAccessTests
{
    public class TableManager_Tests
    {
        [Theory]
        [ClassData(typeof(InsertInto_TestData))]
        public void Insert_Should_ReturnNonNegativeInteger_WhenUserInserted(Users user)
        {
            // act
            var moq_connection = new Mock<IDbConnection>();

            moq_connection.Setup(c => c.ExecuteScalar<int>(
                It.IsAny<string>(),
                It.IsAny<object>(),
                null,
                null,
                It.IsAny<CommandType>())).Returns(1);

            var moq_logger = new Mock<DataBase_Logger>();

            moq_logger.Setup(l => l.LogErrorMessage(It.IsAny<string>(), It.IsAny<string>()));
            
            
            GenericRepository<Users> T = new GenericRepository<Users>(moq_connection.Object,moq_logger.Object);
            
            // assert 
            var result = T.Add(user);

            Assert.True(result != -1);
        }
        
        
    }


    public class InsertInto_TestData : TheoryData<Users>
    {
        public InsertInto_TestData()
        {
            Add(new Users()
            {
                PhoneNumber = "01225500",
                Password = "Null_IDK?",
                Role = true,
                UserName = "HelloWorld"
            });
        }
    }
}