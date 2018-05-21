using System.Collections.Generic;

namespace Agent.Files
{
    public interface IFileFactory
    {
        IEnumerable<IFile> LoadFiles(string path);

        void Upload(IFile file);
    }
}
