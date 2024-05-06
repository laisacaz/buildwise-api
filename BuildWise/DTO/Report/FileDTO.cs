namespace BuildWise.DTO.Report
{
    public class FileDTO
    {
        public FileDTO(Stream content, string contentType, string filename)
        {
            Content = content;
            ContentType = contentType;
            FileName = filename;
            Metadata = new Dictionary<string, string>();
        }

        public Stream Content { get; }
        public string ContentType { get; }
        public string FileName { get; }
        public Dictionary<string, string> Metadata { get; }
    }
}
