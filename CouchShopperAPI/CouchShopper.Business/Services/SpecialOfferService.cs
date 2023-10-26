using AutoMapper;
using CouchShopper.Business.Exceptions;
using CouchShopper.Business.Interfaces;
using CouchShopper.Business.Validators;
using CouchShopper.Data.Constants;
using CouchShopper.Data.DTOs.Request.Common.SpecialOffer;
using CouchShopper.Data.DTOs.Response.Common.SpecialOffer;
using CouchShopper.Data.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CouchShopper.Business.Services
{
    public class SpecialOfferService : BaseService<Common>, ISpecialOfferService
    {
        private readonly IMapper _mapper;
        public SpecialOfferService(IConfiguration configuration, IHttpClientFactory clientFactory, IMapper mapper)
            : base(configuration, clientFactory)
        {
            _mapper = mapper;
        }

        public async Task<SpecialOfferResponse> GetSpecialOffer(string id)
        {
            var commonSpecialOffers = await GetByIdAsync("SpecialOffers");
            var specialOffer = commonSpecialOffers.SpecialOffers?.Where(x => x.Id == id && !x.Deleted)?.FirstOrDefault();
            if (specialOffer == null)
            {
                throw new InvalidRequestException($"Special offer does not exist.");
            }
            var response = _mapper.Map<SpecialOfferResponse>(specialOffer);

            var photo = await GetAttachemntContent("SpecialOffers", specialOffer.PhotoId);
            response.ImageBase64 = photo != null ? Convert.ToBase64String(photo) : null;

            return response;
        }

        public async Task<List<SpecialOfferResponse>> GetSpecialOffers()
        {
            var commonSpecialOffers = await GetByIdAsync("SpecialOffers");
            var specialOffer = commonSpecialOffers.SpecialOffers?.Where(x => x.Published && !x.Deleted)?.ToList();
            if (specialOffer == null)
            {
                throw new InvalidRequestException($"Special offer does not exist.");
            }
            var response = _mapper.Map<List<SpecialOfferResponse>>(specialOffer);
            foreach (var item in response)
            {
                var photo = await GetAttachemntContent("SpecialOffers", item.PhotoId);
                item.ImageBase64 = photo != null ? Convert.ToBase64String(photo) : null;
            }

            return response;
        }

        public async Task<SpecialOfferListResponse> GetActiveSpecialOffers(int page)
        {
            page = page == 0 ? 1 : page;
            var activeSpecialOffers = (await GetByIdAsync("SpecialOffers"))?.SpecialOffers?.OrderBy(x => x.CreatedOn)?.ToList()?.FindAll(x => !x.Deleted);
            if (activeSpecialOffers != null)
            {
                var response = new SpecialOfferListResponse
                {
                    TotalEntities = activeSpecialOffers.Count,
                    Offset = (page),
                    SpecialOffers = _mapper.Map<List<SpecialOfferResponse>>(activeSpecialOffers.Skip((page - 1) * Numeric.ItemsPerPage).Take(Numeric.ItemsPerPage))
                };
                foreach (var item in response.SpecialOffers)
                {
                    var imageBytes = await GetAttachemntContent("SpecialOffers", item.PhotoId);
                    item.ImageBase64 = Convert.ToBase64String(imageBytes);
                }
                return response;
            }

            return new SpecialOfferListResponse
            {
                TotalEntities = 0,
                Offset = (page),
                SpecialOffers = new List<SpecialOfferResponse>()
            };

        }

        public async Task CreateSpecialOffer(SpecialOfferAddRequest request)
        {
            request.Validate();
            var specialOfferToInsert = _mapper.Map<SpecialOffer>(request);
            var commonSpecialOffers = await GetByIdAsync("SpecialOffers");
            if (commonSpecialOffers == null)
            {
                if (!await InsertAsync(new Common() { Id = "SpecialOffers", SpecialOffers = new List<SpecialOffer>() }))
                {
                    throw new InvalidRequestException($"Special offers could not be created");
                }
                commonSpecialOffers = await GetByIdAsync("SpecialOffer");
            }
            var backgroudPhoto = _mapper.Map<SpecialOfferPhoto>(request);
            specialOfferToInsert.PhotoId = backgroudPhoto.Id;
            commonSpecialOffers.SpecialOffers.Add(specialOfferToInsert);
            var response = await UpdateAsync(commonSpecialOffers);
            if (!response)
            {
                throw new InvalidRequestException($"SpecialOffers could not be created");
            }
            await InsertAttachment("SpecialOffers", backgroudPhoto.Id, backgroudPhoto.Content, (await GetByIdAsync("SpecialOffers")).Rev);
        }

        public async Task PublishUpublishSpecialOffer(string offerId)
        {
            var commonSpecialOffers = await GetByIdAsync("SpecialOffers");
            var specialOffer = commonSpecialOffers.SpecialOffers?.Where(x => x.Id == offerId)?.FirstOrDefault();
            if (specialOffer == null)
            {
                throw new InvalidRequestException($"Special offer does not exist.");
            }
            specialOffer.Published = !specialOffer.Published;
            var response = await UpdateAsync(commonSpecialOffers);
            if (!response)
            {
                throw new InvalidRequestException($"SpecialOffers could not be published");
            }
        }

        public async Task UpdateSpecialOffer(SpecialOfferUpdateRequest request)
        {
            request.Validate();
            var commonSpecialOffers = await GetByIdAsync("SpecialOffers");
            var specialOfferToUpdate = commonSpecialOffers != null
                ? commonSpecialOffers.SpecialOffers.FirstOrDefault(x => x.Id.Equals(request.Id) && !x.Deleted)
                : throw new InvalidRequestException($"Special Offers could not be updated");
            if (specialOfferToUpdate == null)
            {
                throw new InvalidRequestException($"Special Offers does not exist.");
            }
            specialOfferToUpdate.Title = request.Title;
            specialOfferToUpdate.Description = request.Description;
            specialOfferToUpdate.BackgroundColor = request.BackgroundColor;
            specialOfferToUpdate.TextColor = request.TextColor;
            if (!await UpdateAsync(commonSpecialOffers))
            {
                throw new InvalidRequestException($"Special Offers could not be updated");
            }
            await InsertAttachment("SpecialOffers", specialOfferToUpdate.PhotoId, Convert.FromBase64String(request.ImageBase64), (await GetByIdAsync("SpecialOffers")).Rev);
        }

        public async Task DeleteSpecialOffer(SpecialOfferDeleteRequest request)
        {
            request.Validate();
            var commonSpecialOffers = await GetByIdAsync("SpecialOffers");
            var specialOfferToUpdate = commonSpecialOffers != null
                ? commonSpecialOffers.SpecialOffers.FirstOrDefault(x => x.Id.Equals(request.SpecialOfferId) && !x.Deleted)
                : throw new InvalidRequestException($"Special Offers could not be updated");
            if (specialOfferToUpdate == null)
            {
                throw new InvalidRequestException($"Special Offers does not exist.");
            }
            specialOfferToUpdate.Deleted = true;
            commonSpecialOffers._attachments.Remove(specialOfferToUpdate.PhotoId);
            if (!await UpdateAsync(commonSpecialOffers))
            {
                throw new InvalidRequestException($"Special Offers could not be deleted");
            }

        }
    }
}
