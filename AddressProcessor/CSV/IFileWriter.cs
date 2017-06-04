namespace AddressProcessing.Unit.Tests
{
    public interface IFileWriter
    {
        void CreateFile(string fileName);
        void WriteLine(string textInFile);
    }
}