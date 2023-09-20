using Domain.Commons;

namespace Domain.Entities.AttachmentFolder;

public class Attachment : Auditable
{
    public string FileName { get; set; } = string.Empty;
    public string FilePath { get; set; } = string.Empty;
}
