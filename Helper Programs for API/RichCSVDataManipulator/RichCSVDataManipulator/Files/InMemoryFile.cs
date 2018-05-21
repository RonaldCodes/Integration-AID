using System;
using System.Text;

namespace Agent.Files
{
    public class InMemoryFile : IFile
    {
        private readonly byte[] _content;

        private bool _deleted;

        public InMemoryFile(FileMetadata metadata, byte[] content)
        {
            Metadata = metadata;
            _content = content;
        }

        public FileMetadata Metadata { get; }

        public string GetContent()
        {
            if (_deleted)
            {
                throw new InvalidOperationException("File has been deleted");
            }

            return Encoding.UTF8.GetString(_content);
        }

        public void Delete()
        {
            _deleted = true;
        }
    }
}