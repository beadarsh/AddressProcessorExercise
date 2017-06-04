using System;
using System.IO;
using AddressProcessing.Unit.Tests;

namespace AddressProcessing.CSV
{
    /*
        2) Refactor this class into clean, elegant, rock-solid & well performing code, without over-engineering.
           Assume this code is in production and backwards compatibility must be maintained.
    */

    /// <summary>
    /// Needs backward compatability, Let this class actjust as an adapter to the tested reader and writer classes
    /// </summary>
    public class CSVReaderWriter
    {
        private readonly IFileReader _fileReader;
        private readonly IFileWriter _fileWriter;
        private readonly IUserDetailsMapper _userDetailsMapper;
        public const string Delimiter = "\t";

        public CSVReaderWriter()
        {
            IFileStore fileStore = new FileStore();
            _fileReader = new FileReader(fileStore);
            _fileWriter = new FileWriter(fileStore);
            _userDetailsMapper = new UserDetailsMapper();
        }

        [Flags]
        public enum Mode { Read = 1, Write = 2 };
        
       
        public void Open(string fileName, Mode mode)
        {
            if (mode == Mode.Read)
            {
                _fileReader.OpenFile(fileName);
                return;
            }
            
            _fileWriter.CreateFile(fileName);
        }

        public void Write(params string[] columns)
        {

            columns = columns ?? throw new ArgumentException("the column text to be written to file cannot be null");

            var textOfColumnsWithSeparater = String.Join(Delimiter, columns);;

            _fileWriter.WriteLine(textOfColumnsWithSeparater);
        }

        public bool Read(string column1, string column2)
        {
            return _fileReader.IsEndOfFile();
        }

        public bool Read(out string userName, out string userContactDetails)
        {
            if (_fileReader.IsEndOfFile())
            {
                userName = null;
                userContactDetails = null;
                return false;
            }

            var userDetails = _fileReader.ReadLine();
            
            _userDetailsMapper.MapUserDataFieldsFromUserDetailsText
                (out userName,
                out userContactDetails,
                userDetails,
                Delimiter);

            return true;
        }

       

        private void WriteLine(string line)
        {
            _fileWriter.WriteLine(line);
        }

        private string ReadLine()
        {
            return _fileReader.ReadLine();
        }

        public void Close()
        {
            _fileWriter?.Dispose();
            _fileReader?.Dispose();
        }
    }
}
