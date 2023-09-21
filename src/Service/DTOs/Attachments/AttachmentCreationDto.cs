using Microsoft.AspNetCore.Http;

namespace Service.DTOs.Attachments;

public class AttachmentCreationDto
{
    public IFormFile formFile { get; set; }
}
