using System;

namespace AddressProcessing.Unit.Tests
{
    public interface IFileWriter: IDisposable
    {
        void CreateFile(string fileName);
        void WriteLine(string textInFile);
    }
}