using System.IO;

namespace AddressProcessing.CSV
{
    public interface IFileStore
    {
        TextReader GetStream(string fileName);
    }
}