using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizLayer.Services.Interfaces
{
    public interface IPropertyDocumentService
    {
        // Fayl yuklash (user tomonidan)
        Task<PropertyDocument> AddDocumentAsync(int propertyId, IFormFile file);

        // Berilgan property ga tegishli barcha dokumentlar
        Task<List<PropertyDocument>> GetDocumentsByPropertyAsync(int propertyId, CancellationToken cancellationToken = default);

        // Bitta dokumentni olish (tahrirlash yoki ko'rish uchun)
        Task<PropertyDocument?> GetDocumentByIdAsync(int documentId, CancellationToken cancellationToken = default);

        // User tomonidan faylni yangilash (faylni almashtiradi)
        Task<PropertyDocument?> UpdateDocumentFileAsync(int documentId, IFormFile newFile, CancellationToken cancellationToken = default);

        // Admin tomonidan statusni (tasdiqlash, rad etish va boshqalar) yangilash
        Task<PropertyDocument?> UpdateDocumentStatusAsync(int documentId, DocumentStatus newStatus, string? rejectionReason = null, CancellationToken cancellationToken = default);

        // Faylni va DB yozuvini o'chirish
        Task<bool> DeleteDocumentAsync(int documentId, CancellationToken cancellationToken = default);

    }
}
