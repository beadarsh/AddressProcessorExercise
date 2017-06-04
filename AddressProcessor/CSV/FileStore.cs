using System.IO;

namespace AddressProcessing.CSV
{
    public class FileStore : IFileStore
    {
        public TextReader GetReadStream(string fileName)
        {
            var fileStreamToRead = new FileStream(fileName,FileMode.Open);
            return new StreamReader(fileStreamToRead);
        }

        public TextWriter GetWriteStream(string fileName)
        {
            var fileStreamToRead = new FileStream(fileName, FileMode.CreateNew);
            return new StreamWriter(fileStreamToRead);
        }
    }
}