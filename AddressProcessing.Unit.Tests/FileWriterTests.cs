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
    public class FileWriterTests
    {
        MemoryStream _writeMemoryStream = new MemoryStream();

        [SetUp]
        public void SetUp()
        {
            _writeMemoryStream = new MemoryStream(); 
        }

        [TearDown]
        public void TearDown()
        {
            _writeMemoryStream = null;
        }


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


        [Test]
        public void WriteLine_WhenWeWriteUserDetails_StreamIsUpdatedToThoseDetails()
        {
            //Arrange
            string textForFile = "word1 \t word2";
            var fileStoreMoq = GenerateMockWriteStream();
            IFileWriter fileWriter = new FileWriter(fileStoreMoq.Object);

            //Act
            fileWriter.CreateFile("sasa");
            fileWriter.WriteLine(textForFile);
            // convert to string
            string fileData = GetTextInFile();

            //Assert
            Assert.AreEqual(textForFile+Environment.NewLine, fileData);
        }

        private string GetTextInFile()
        {
            var fileData = Encoding.UTF8.GetString(_writeMemoryStream.ToArray());
            return fileData;
        }

        [Test]
        public void WriteLine_WhenUsedWithoutCallingCreateFile_ThrowsException()
        {
            //Arrange
            string textForFile = "word1 \t word2";
            var fileStoreMoq = GenerateMockWriteStream();
            IFileWriter fileWriter = new FileWriter(fileStoreMoq.Object);


            //Assert
            Assert.Throws<Exception>(() => fileWriter.WriteLine(textForFile));
        }

       



        private  Mock<IFileStore> GenerateMockWriteStream()
        {
            var fileStoreMoq = new Mock<IFileStore>();
            UTF8Encoding encoding = new UTF8Encoding();
            _writeMemoryStream = new MemoryStream();
            StreamWriter writer = new StreamWriter(_writeMemoryStream);
            fileStoreMoq.Setup(fs => fs.GetWriteStream(It.IsAny<string>())).Returns(() => writer);
            return fileStoreMoq;
        }
    }
}