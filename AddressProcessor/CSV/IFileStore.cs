using System.IO;

namespace AddressProcessing.CSV
{
    public interface IFileStore
    {
        TextReader GetReadStream(string fileName);
        TextWriter GetWriteStream(string fileName);
    }
}