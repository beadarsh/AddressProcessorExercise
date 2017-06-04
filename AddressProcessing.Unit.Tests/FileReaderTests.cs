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
            fileReader.OpenFile(fileName);

            //Assert
            fileStoreMoq.Verify(x => x.GetReadStream(fileName));
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
        public void ReadLine_WhenTheFileHasUserDetails_ReturnsUserDetailsInAString()
        {
            //Arrange
            string textInFile = "word1 \t word2";
            var fileStoreMoq = GenerateMockStreamWithText(textInFile);
            IFileReader fileReader = new FileReader(fileStoreMoq.Object); ;

            //Act

            string result = fileReader.ReadLine();

            //Assert
            Assert.AreEqual(textInFile, result);

        }

        [Test]
        public void ReadLine_WhenUsedWithoutCallingOpenFile_ThrowsException()
        {
            //Arrange
            string textInFile = "word1 \t word2";
            var fileStoreMoq = GenerateMockStreamWithText(textInFile);
            IFileReader fileReader = new FileReader(fileStoreMoq.Object); ;
            

            //Assert
            Assert.Throws<Exception>(() => fileReader.ReadLine());

        }

        [Test]
        public void ReadLine_ForAFileWithUserDetails_ReturnsUserDetails()
        {
            //Arrange
            string textInFile = "word1 \t word2";
            var fileStoreMoq = GenerateMockStreamWithText(textInFile);
            IFileReader fileReader = new FileReader(fileStoreMoq.Object);;

            //Act
            fileReader.OpenFile("testFile");
            string result = fileReader.ReadLine();

            //Assert
            Assert.AreEqual(textInFile,result);

        }

        [Test]
        public void ReadLine_ForAFileWithTwoUserDetailsInEachLine_ReturnsUserDetailsForTwo()
        {
            //Arrange
            string textInFile = "word1 \t word2\nword3 \t word4";
            var fileStoreMoq = GenerateMockStreamWithText(textInFile);
            IFileReader fileReader = new FileReader(fileStoreMoq.Object); ;

            //Act
            fileReader.OpenFile("testFile");
            string result1 = fileReader.ReadLine();
            string result2 = fileReader.ReadLine();
            //Assert
            Assert.AreEqual("word1 \t word2", result1);
            Assert.AreEqual("word3 \t word4", result2);

        }

        [Test]
        public void IsEndOfFile_ForAFileWithUserDetails_ReturnsFalse()
        {
            //Arrange
            string textInFile = "word1 \t word2\nword3 \t word4";
            Mock<IFileStore> fileStoreMoq = GenerateMockStreamWithText(textInFile);
            IFileReader fileReader = new FileReader(fileStoreMoq.Object);
            //Act

            fileReader.OpenFile("fileName");
            bool isEndOfFile = fileReader.IsEndOfFile();

            //Assert
            Assert.IsFalse(isEndOfFile);
        }

        [Test]
        public void IsEndOfFile_ForAFileWithOneLineOfUserDetails_AttemptingAfterReadLineReturnsTrue()
        {
            //Arrange
            string textInFile = "word1 \t word";
            Mock<IFileStore> fileStoreMoq = GenerateMockStreamWithText(textInFile);
            IFileReader fileReader = new FileReader(fileStoreMoq.Object);

            //Act

            fileReader.OpenFile("fileName");
            fileReader.ReadLine();
            bool isEndOfFile = fileReader.IsEndOfFile();

            //Assert
            Assert.IsTrue(isEndOfFile);
        }

        [Test]
        public void IsEndOfFile_ForAFileWithTwoLineOfUserDetails_AttemptingAfterTwoReadLineReturnsTrue()
        {
            //Arrange
            string textInFile = "word1 \t word\nword3 \t word4";
            Mock<IFileStore> fileStoreMoq = GenerateMockStreamWithText(textInFile);
            IFileReader fileReader = new FileReader(fileStoreMoq.Object);

            //Act

            fileReader.OpenFile("fileName");
            fileReader.ReadLine();
            fileReader.ReadLine();
            bool isEndOfFile = fileReader.IsEndOfFile();

            //Assert
            Assert.IsTrue(isEndOfFile);
        }


        private static Mock<IFileStore> GenerateMockStreamWithText(string textInFile)
        {
            var fileStoreMoq = new Mock<IFileStore>();
            UTF8Encoding encoding = new UTF8Encoding();
            MemoryStream memoryStream = new MemoryStream(encoding.GetBytes(textInFile));
            StreamReader reader = new StreamReader(memoryStream);
            fileStoreMoq.Setup(fs => fs.GetReadStream(It.IsAny<string>())).Returns(() => reader);
            return fileStoreMoq;
        }
    }
}
