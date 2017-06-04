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
    public class FileReaderTests
    {
        [Test]
        public void OpenFile_WithAFileName_AttemptsToGetATextReaderWithThatFileName()
        {
            //Arrange
            var fileName = "testFile";
            var fileStoreMoq = new Mock<IFileStore>();
            IFileReader fileReader = new FileReader(fileStoreMoq.Object);

            //Act
            fileReader.GetStream(fileName);

            //Assert
            fileStoreMoq.Verify(x => x.GetStream(fileName));
        }

        [Test]
        public void OpenFile_WithANullFileStore_ThrowsInvalidArgumentException()
        {
            //Assert
            Assert.Throws<ArgumentException>(() => new FileReader(null));
        }

        [Test]
        public void FileReader_IsOfTypeDisposable()
        {
            //Arrange
            var fileStoreMoq = new Mock<IFileStore>();
            IFileReader fileReader = new FileReader(fileStoreMoq.Object);

            Assert.IsTrue(fileReader is IDisposable);
        }

        [Test]
        public void ReadLine_ForAFileWithUserDetails_ReturnsUserDetailsInAString()
        {
            //Arrange
            string textInFile = "word1 \t word2";
            var fileStoreMoq = GenerateMockStreamWithText(textInFile);
            IFileReader fileReader = new FileReader(fileStoreMoq.Object);;

            //Act

            string result = fileReader.ReadLine();

            //Assert
            Assert.AreEqual(textInFile,result);

        }

        private static Mock<IFileStore> GenerateMockStreamWithText(string textInFile)
        {
            var fileStoreMoq = new Mock<IFileStore>();
            UTF8Encoding encoding = new UTF8Encoding();
            MemoryStream memoryStream = new MemoryStream(encoding.GetBytes(textInFile));
            StreamReader reader = new StreamReader(memoryStream);
            fileStoreMoq.Setup(fs => fs.GetStream(It.IsAny<string>())).Returns(() => reader);
            return fileStoreMoq;
        }
    }
}
