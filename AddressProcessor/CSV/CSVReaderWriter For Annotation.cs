using System;
using System.IO;

namespace AddressProcessing.CSV
{
    /*
       // 1) List three to five key concerns with this implementation that you would discuss with the junior developer. 
       // Please leave the rest of this file as it is so we can discuss your concerns during the next stage of the interview process.
        
         ========================================================================================================================

          First thing that strikes me is that the 'CSVReaderWriterForAnnotation' is that it does not seem to adhere
          the single reponsiblity principle and could possibly have more than one reason to change? 
          We should think of reasons why the class could change (ex: what if the structure in csv file changes and logic changes
          What if we change the way we read and want to implement caching for the data?
          or would there be a a need to write to encrypted stream for security reasons? Does not promote reusablity.

          ========================================================================================================================
          
          The class is not unit testable as it seems to have an dependencies to the stream. Unit tests give you the confidence to
          make your changes quickly and make sure you have not introduced bugs
          
          ========================================================================================================================
                   
           One more issue is the class does not seem to follow the Idisposable pattern as it is the responsibility of the class
           to dispose unmanaged resource Irrespsctive when the object is out of scope and not depend on if the user invokes
           close method (Exceptions?).         
         
          ========================================================================================================================
          
           I see method names confusing and are not self descriptive. ex: what is the point of the Read method returning bool value.
           Also i am not sure all the methods are needed? Prefer TDD that supports YAGNI principle.        
    */

    public class CSVReaderWriterForAnnotation
    {
        private StreamReader _readerStream = null;
        private StreamWriter _writerStream = null;

        [Flags]
        public enum Mode { Read = 1, Write = 2 };

        public void Open(string fileName, Mode mode)
        {
            if (mode == Mode.Read)
            {
                _readerStream = File.OpenText(fileName);
            }
            else if (mode == Mode.Write)
            {
                FileInfo fileInfo = new FileInfo(fileName);
                _writerStream = fileInfo.CreateText();
            }
            else
            {
                throw new Exception("Unknown file mode for " + fileName);
            }
        }

        public void Write(params string[] columns)
        {
            string outPut = "";

            for (int i = 0; i < columns.Length; i++)
            {
                outPut += columns[i];
                if ((columns.Length - 1) != i)
                {
                    outPut += "\t";
                }
            }

            WriteLine(outPut);
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
            _writerStream.WriteLine(line);
        }

        private string ReadLine()
        {
            return _readerStream.ReadLine();
        }

        public void Close()
        {
            if (_writerStream != null)
            {
                _writerStream.Close();
            }

            if (_readerStream != null)
            {
                _readerStream.Close();
            }
        }
    }
}
