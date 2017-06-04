namespace AddressProcessing.Unit.Tests
{
    internal interface IUserDetailsMapper
    {
        void GetUserDetailFieldsFromCsvFileText(out string userName, out string userContactDetails, string userDetails, string delimiter);
    }
}
