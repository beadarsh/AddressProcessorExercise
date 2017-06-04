using System;
using System.Collections.Generic;
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
    public class FileWriterTests
    {
        [Test]
        public void CreatFile_WithAFileName_AttemptsToGetATextWriterWithThatFileName()
        {
            //Arrange
            var fileName = "testFile";
            var fileStoreMoq = new Mock<IFileStore>();
            IFileWriter fileWriter = new FileWriter(fileStoreMoq.Object);

            //Act
            fileWriter.CreateFile(fileName);

            //Assert
            fileStoreMoq.Verify(x => x.GetWriteStream(fileName));
        }

        [Test]
        public void CreatFile_WithANullFileStore_ThrowsInvalidArgumentException()
        {
            //Assert
            Assert.Throws<ArgumentException>(() => new FileWriter(null));
        }

        [Test]
        public void FileWriter_IsOfTypeDisposable()
        {
            //Arrange
            var fileStoreMoq = new Mock<IFileStore>();
            IFileWriter fileReader = new FileWriter(fileStoreMoq.Object);

            Assert.IsTrue(fileReader is IDisposable);
        }
    }
}