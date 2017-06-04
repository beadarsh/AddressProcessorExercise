using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using AddressProcessing.CSV;
using Microsoft.SqlServer.Server;
using NUnit.Framework;
using Moq;

namespace AddressProcessing.Unit.Tests
{
    [TestFixture]
    class CSVReaderWriterTests
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
