using System;
using System.IO;

namespace AddressProcessing.CSV
{
    public class FileReader : IFileReader, IDisposable
    {
        private readonly IFileStore _fileStore;
        private TextReader _textReader;

        public FileReader(IFileStore fileStore)
        {
            _fileStore = fileStore ?? throw new ArgumentException("the file store cannot be null");
        }

        public void GetStream(string fileName)
        {
            _textReader = _fileStore.GetStream(fileName);
        }

        public string ReadLine()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _textReader?.Dispose();
        }
    }
}