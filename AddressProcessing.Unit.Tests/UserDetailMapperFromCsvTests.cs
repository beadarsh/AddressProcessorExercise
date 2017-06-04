using NUnit.Framework;
using AddressProcessing.CSV;

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

            mapper.MapUserDataFieldsFromUserDetailsText(out userName, out userContactDetails, userDetails, "\t");

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

            mapper.MapUserDataFieldsFromUserDetailsText(out userName, out userContactDetails, userDetails, "\t");

            //Assert
            Assert.AreEqual("userName", userName);
            //Assert
            Assert.IsNull(userContactDetails);
        }
    }
}
