namespace AddressProcessing.CSV
{
    public interface IFileReader
    {
        void OpenFile(string fileName);
        string ReadLine();
    }
}