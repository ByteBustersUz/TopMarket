using Service.DTOs.Attachments;
using Service.Interfaces;

namespace Service.Services;

public class AttachmentService : IAttachmentService
{
    public Task<AttachmentResultDto> UploadImageAsync(AttachmentCreationDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(long id)
    {
        throw new NotImplementedException();
    }
}
