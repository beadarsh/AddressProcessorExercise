using System.IO;

namespace AddressProcessing.Unit.Tests
{
    public interface IFileStore
    {
        TextReader GetStream(string fileName);
    }
}