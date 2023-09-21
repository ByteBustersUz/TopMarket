﻿using AutoMapper;
using Data.IRepositories;
using Domain.Entities.AttachmentFolder;
using Service.DTOs.Attachments;
using Service.Extension;
using Service.Helpers;
using Service.Interfaces;
using Service.Exceptions;

namespace Service.Services;

public class AttachmentService : IAttachmentService
{
    private readonly IMapper mapper;
    private readonly IRepository<Attachment> attachmentRepository;

    public AttachmentService(IMapper mapper, IRepository<Attachment> attachmentRepository)
    {
        this.mapper = mapper;   
        this.attachmentRepository = attachmentRepository;
    }

    public async Task<AttachmentResultDto> UploadImageAsync(AttachmentCreationDto dto)
    {
        var weebrootPath = Path.Combine(PathHepler.WebRootPath, "Images");

        if(!Directory.Exists(weebrootPath))
            Directory.CreateDirectory(weebrootPath);

        var fileExtention = Path.GetExtension(dto.FormFile.FileName);
        var fileName = $"{Guid.NewGuid().ToString("N")}{fileExtention}";
        var fullPath = Path.Combine(weebrootPath, fileName);

        var fileStream = new FileStream(fullPath, FileMode.OpenOrCreate);
        await fileStream.WriteAsync(dto.FormFile.ToByte());

        var createdAttachment = new Attachment
        {
            FileName = fileName,
            FilePath = fullPath,
        };

        await attachmentRepository.AddAsync(createdAttachment);
        await attachmentRepository.SaveAsync();

        return mapper.Map<AttachmentResultDto>(createdAttachment);
    }

    public async Task<bool> DeleteImageAsync(long id)
    {
        var existImage = await attachmentRepository.GetAsync(attachment => attachment.Equals(id))
            ?? throw new NotFoundException($"This image was not found with {id}");

        attachmentRepository.Delete(existImage);
        await attachmentRepository.SaveAsync();

        return true;
    }
}
