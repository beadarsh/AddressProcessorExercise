using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;


namespace AddressProcessing.Unit.Tests
{
    [TestFixture]
    public class FilReaderTests
    {
        [Test]
        public void OpenFile_UsingAFileStore_AttemptsToGetATextReader()
        {
            IFileReader fileReader = new FileReader(new FileStore());
        }
    }

    public class FileStore
    {
    }

    public class FileReader : IFileReader
    {
        public FileReader(FileStore fileStore)
        {
            throw new NotImplementedException();
        }
    }

    public interface IFileReader
    {
    }
}
