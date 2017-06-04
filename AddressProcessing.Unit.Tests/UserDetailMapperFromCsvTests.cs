using System.Text;
using System.Threading.Tasks;
using System.Xml;
using AddressProcessing.CSV;
using Microsoft.SqlServer.Server;
using NUnit.Framework;
using Moq;

namespace AddressProcessing.Unit.Tests
{
    [TestFixture]
    class UserDetailMapperFromCsvTests 
    {
        [Test]
        public void GetUserDetailFieldsFromCsvFileText_WithValidUserDetails_MapsUserDetails()
        {
            //Arrange
            IUserDetailsMapper mapper = new UserDetailsMapper();
            string userName;
            string userContactDetails;
            string userDetails = "userName\tuserContactDetails";

            mapper.GetUserDetailFieldsFromCsvFileText(out userName, out userContactDetails, userDetails, "\t");

            //Assert
            Assert.AreEqual("userName",userName);
            //Assert
            Assert.AreEqual("userContactDetails", userContactDetails);
        }

        [Test]
        public void GetUserDetailFieldsFromCsvFileText_WithJustUserNameInUserDetails_MapsJustUserName()
        {
            //Arrange
            IUserDetailsMapper mapper = new UserDetailsMapper();
            string userName;
            string userContactDetails;
            string userDetails = "userName";

            mapper.GetUserDetailFieldsFromCsvFileText(out userName, out userContactDetails, userDetails, "\t");

            //Assert
            Assert.AreEqual("userName", userName);
            //Assert
            Assert.IsNull(userContactDetails);
        }
    }
}
