using System.IO;

namespace AddressProcessing.CSV
{
    public class FileStore : IFileStore
    {
        public TextReader GetStream(string fileName)
        {
            var fileStreamToRead = new FileStream(fileName,FileMode.Open);
            return new StreamReader(fileStreamToRead);
        }
    }
}