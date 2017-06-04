using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using AddressProcessing.CSV;
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
            var fileStore = new Mock<IFileStore>();
            IFileReader fileReader = new FileReader(fileStore.Object);

            //Act
            fileReader.GetStream(fileName);

            //Assert
            fileStore.Verify(x => x.GetStream(fileName));
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
            var fileStore = new Mock<IFileStore>();
            IFileReader fileReader = new FileReader(fileStore.Object);

            Assert.IsTrue(fileReader is IDisposable);
        }

        //UTF8Encoding encoding = new UTF8Encoding();
        //encoding.GetBytes()
        //MemoryStream memoryStream = new MemoryStream();
        //fileStore.Setup(fs => fs.GetStream(It.IsAny<string>())).Returns(() => )
    }
}
