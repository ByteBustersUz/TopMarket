using Service.DTOs.Attachments;

namespace Service.Interfaces;

public interface IAttachmentService
{
    Task<AttachmentResultDto> UploadImageAsync(AttachmentCreationDto dto);
    Task<bool> DeleteImageAsync(long id);
}
