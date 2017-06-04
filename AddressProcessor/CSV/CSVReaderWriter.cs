using System;
using System.IO;
using AddressProcessing.Unit.Tests;

namespace AddressProcessing.CSV
{
    /*
        2) Refactor this class into clean, elegant, rock-solid & well performing code, without over-engineering.
           Assume this code is in production and backwards compatibility must be maintained.
    */

    public class CSVReaderWriter
    {
        private readonly IFileReader _fileReader;
        private readonly IFileWriter _fileWriter;

        public CSVReaderWriter()
        {
            IFileStore fileStore = new FileStore();
            _fileReader = new FileReader(fileStore);
            _fileWriter = new FileWriter(fileStore);
        }

        [Flags]
        public enum Mode { Read = 1, Write = 2 };
        
        /// <summary>
        /// Needs backward compatability, Let this class act as an adapter to the implemented reader and writer
        /// </summary>
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
            var textToWriteToFile = String.Join("\t", columns);;
            _fileWriter.WriteLine(textToWriteToFile);
        }

        public bool Read(string column1, string column2)
        {
            const int FIRST_COLUMN = 0;
            const int SECOND_COLUMN = 1;

            string line;
            string[] columns;

            char[] separator = { '\t' };

            line = ReadLine();
            columns = line.Split(separator);

            if (columns.Length == 0)
            {
                column1 = null;
                column2 = null;

                return false;
            }
            else
            {
                column1 = columns[FIRST_COLUMN];
                column2 = columns[SECOND_COLUMN];

                return true;
            }
        }

        public bool Read(out string column1, out string column2)
        {
            const int FIRST_COLUMN = 0;
            const int SECOND_COLUMN = 1;

            string line;
            string[] columns;

            char[] separator = { '\t' };

            line = ReadLine();

            if (line == null)
            {
                column1 = null;
                column2 = null;

                return false;
            }

            columns = line.Split(separator);

            if (columns.Length == 0)
            {
                column1 = null;
                column2 = null;

                return false;
            } 
            else
            {
                column1 = columns[FIRST_COLUMN];
                column2 = columns[SECOND_COLUMN];

                return true;
            }
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
