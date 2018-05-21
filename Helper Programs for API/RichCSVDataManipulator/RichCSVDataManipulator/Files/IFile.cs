namespace Agent.Files
{
    public interface IFile
    {
        FileMetadata Metadata { get; }

        string GetContent();

        void Delete();
    }
}
