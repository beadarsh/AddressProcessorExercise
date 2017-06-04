using System;
using AddressProcessing.CSV;
using NUnit.Framework;

namespace AddressProcessing.Unit.Tests
{
    [TestFixture]
    class CsvReaderWriterTests
    {
        [Test]
        public void Write_WithNullColumsParameter_ThrowsArgumentException()
        {
            //Arrange
            CSVReaderWriter readerWriter = new CSVReaderWriter();
            //Assert
            Assert.Throws<ArgumentException>(() => readerWriter.Write(null));
        }
    }
}
