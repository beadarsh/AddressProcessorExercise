using System;
using System.IO;
using AddressProcessing.CSV;

namespace AddressProcessing.Unit.Tests
{
    public class FileWriter : IFileWriter, IDisposable
    {
        private IFileStore _fileStore;
        private TextWriter _textWriter;

        public FileWriter(IFileStore fileStore)
        {
            _fileStore = fileStore ?? throw new ArgumentException("the file store cannot be null");
        }

        public void CreateFile(string fileName)
        {
            _textWriter = _fileStore.GetWriteStream(fileName);
        }

        public void WriteLine(string text)
        {
            if (_textWriter == null)
            {
                throw new Exception("The text writer is null. Invoke CreateFile before calling this method.");
            }
            _textWriter.WriteLine(text);
            _textWriter.Flush();
        }

        public void Dispose()
        {
            _textWriter?.Dispose();
        }
    }
}