namespace AddressProcessing.CSV
{
    public interface IFileReader
    {
        void GetStream(string fileName);
        string ReadLine();
    }
}