namespace BuildWise.DTO.Report
{
    public class PdfDTO : FileDTO
    {
        public PdfDTO(Stream content, string name) : base(
           content,
           contentType: "application/pdf",
           filename: Path.ChangeExtension(name, "pdf"))
        {
        }
    }
}
