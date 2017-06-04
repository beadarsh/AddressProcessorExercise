using System.IO;

namespace AddressProcessing.CSV
{
    public class FileReader : IFileReader
    {
        private readonly IFileStore _fileStore;
        private TextReader _textReader;

        public FileReader(IFileStore fileStore)
        {
            _fileStore = fileStore;
        }

        public void GetStream(string fileName)
        {
            _textReader = _fileStore.GetStream(fileName);
        }
    }
}