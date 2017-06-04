using System;

namespace AddressProcessing.Unit.Tests
{
    internal class UserDetailsMapper : IUserDetailsMapper
    {
        public void GetUserDetailFieldsFromCsvFileText(out string userName, out string userContactDetails, string userDetails, string delimiter)
        {
            var userDetailColumnFields = userDetails.Split(new string[] { delimiter }, StringSplitOptions.None);

            userName = userDetailColumnFields[0];

            userContactDetails = userDetailColumnFields.Length > 1 ? userDetailColumnFields[1] : null;
        }

       
    }
}
