namespace Models.DTOs
{
    public class FileStreamDto
    {
        public string fileName { get; set; } = string.Empty;
        public string mimeType { get; set; } = string.Empty;
        public MemoryStream stream { get; set; }
    }
}
