using System;
using System.Linq;

namespace Agent.Files
{
    public class FileMetadata
    {
        public FileMetadata(string path, string filename)
            : this(string.Join("/", path, filename))
        {

        }

        public FileMetadata(string path)
        {
            Id = Guid.NewGuid().ToString();
            Timestamp = DateTime.UtcNow;
            Path = path;
        }

        public string Id { get; private set; }

        public string Filename => Path.Split('/').LastOrDefault();

        public string Path { get; private set; }

        public DateTime Timestamp { get; private set; }
    }
}
