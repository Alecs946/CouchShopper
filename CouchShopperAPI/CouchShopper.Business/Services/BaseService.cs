using CouchShopper.Business.Interfaces;
using CouchShopper.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Mail;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace CouchShopper.Business.Services
{
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : BaseEntity
    {
        private readonly string _couchDbUrl;
        private readonly string _couchDbUser;
        private readonly IConfiguration _configuration;
        protected readonly IHttpClientFactory _clientFactory;

        protected readonly HttpClient client;

        public BaseService(IConfiguration configuration, IHttpClientFactory clientFactory)
        {
            _configuration = configuration;
            _clientFactory = clientFactory;
            _couchDbUrl = this._configuration["CouchDB:URL"];
            _couchDbUser = this._configuration["CouchDB:User"];
            client = DbHttpClient();
        }

        public virtual async Task<TEntity> GetByIdAsync(string id)
        {
            var response = await client.GetAsync($"{typeof(TEntity).Name.ToLower()}/{id}");
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<TEntity>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                return null;
            }
        }

        public virtual async Task<bool> InsertAsync(TEntity entity)
        {
            var jsonData = JsonConvert.SerializeObject(entity);
            var httpContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var postResult = await client.PostAsync(typeof(TEntity).Name.ToLower(), httpContent).ConfigureAwait(true);

            return postResult.IsSuccessStatusCode;
        }

        public virtual async Task<bool> UpdateAsync(TEntity entity)
        {
            var httpContent = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8);
            var putResult = await client.PutAsync($"{typeof(TEntity).Name.ToLower()}/{entity.Id}?rev={entity.Rev}", httpContent).ConfigureAwait(true);

            return putResult.IsSuccessStatusCode;
        }

        public virtual async Task<bool> DeleteAsync(string id)
        {
            var entity = await GetByIdAsync(id);
            entity.Deleted = true;
            var response = await UpdateAsync(entity);

            return response;
        }


        public virtual async Task<byte[]> GetAttachemntContent(string documentId, string attacmentName)
        {
            var response = await client.GetAsync($"{typeof(TEntity).Name.ToLower()}/{documentId}/{attacmentName}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content?.ReadAsByteArrayAsync();
            }
            else
            {
                return null;
            }
        }

        public async Task InsertAttachment(string documentId, string attachmentName, byte[] attachment, string docRevision)
        {
            var content = new ByteArrayContent(attachment);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");

            var response = await client.PutAsync($"{typeof(TEntity).Name.ToLower()}/{documentId}/{attachmentName}?rev={docRevision}", content);

            response.EnsureSuccessStatusCode();
        }

        private HttpClient DbHttpClient()
        {
            var httpClient = this._clientFactory.CreateClient();
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Clear();
            httpClient.BaseAddress = new Uri(_couchDbUrl);
            var dbUserByteArray = Encoding.ASCII.GetBytes(_couchDbUser);
            httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + Convert.ToBase64String(dbUserByteArray));
            return httpClient;
        }

    }
}
