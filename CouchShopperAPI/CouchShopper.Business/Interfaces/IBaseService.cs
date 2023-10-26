using CouchShopper.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouchShopper.Business.Interfaces
{
    public interface IBaseService<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity> GetByIdAsync(string id);

        Task<bool> InsertAsync(TEntity entity);

        Task<bool> UpdateAsync(TEntity entity);

        Task<bool> DeleteAsync(string id);

        Task<byte[]> GetAttachemntContent(string documentId,string attacmentName);

        Task InsertAttachment(string documentId, string attachmentName, byte[] attachment, string docRevision);

        //Task<bool> InsertMultipleAttachments(string documentId, List<DocumentAttachment> attachments, string docRevision);

    }
}
