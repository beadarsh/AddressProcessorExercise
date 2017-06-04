using System;

namespace AddressProcessing.CSV
{
    public class UserDetailsMapper : IUserDetailsMapper
    {
        public void MapUserDataFieldsFromUserDetailsText(out string userName, out string userContactDetails, string userDetails, string delimiter)
        {
            var userDetailColumnFields = userDetails.Split(new string[] { delimiter }, StringSplitOptions.None);

            userName = userDetailColumnFields[0];

            userContactDetails = userDetailColumnFields.Length > 1 ? userDetailColumnFields[1] : null;
        }
       
    }
}
