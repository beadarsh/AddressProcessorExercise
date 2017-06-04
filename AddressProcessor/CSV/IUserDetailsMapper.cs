namespace AddressProcessing.CSV
{
    internal interface IUserDetailsMapper
    {
        void MapUserDataFieldsFromUserDetailsText(out string userName, out string userContactDetails, string userDetails, string delimiter);
    }
}
