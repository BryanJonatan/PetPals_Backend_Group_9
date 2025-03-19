﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using PetPals_BackEnd_Group_9.Models;

namespace PetPals_BackEnd_Group_9.Handlers
{
    public class SearchAdoptionListHandler : IRequestHandler<SearchAdoptionListQuery, List<AdoptionListDto>>
    {
        private readonly PetPalsDbContext _context;

        public SearchAdoptionListHandler(PetPalsDbContext context)
        {
            _context = context;
        }

        public async Task<List<AdoptionListDto>> Handle(SearchAdoptionListQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Pets
                .Where(p => p.Status == "available")
                .AsQueryable();

            if (!string.IsNullOrEmpty(request.Name))
                query = query.Where(p => p.Name.Contains(request.Name));

            if (!string.IsNullOrEmpty(request.Breed))
                query = query.Where(p => p.Breed == request.Breed);

            if (request.SpeciesId.HasValue)
                query = query.Where(p => p.SpeciesId == request.SpeciesId);

            if (request.MinAge.HasValue)
                query = query.Where(p => p.Age >= request.MinAge);

            if (request.MaxAge.HasValue)
                query = query.Where(p => p.Age <= request.MaxAge);

            if (request.MinPrice.HasValue)
                query = query.Where(p => p.Price >= request.MinPrice);

            if (request.MaxPrice.HasValue)
                query = query.Where(p => p.Price <= request.MaxPrice);

            return await query.Select(p => new AdoptionListDto
            {
                PetId = p.PetId,
                Name = p.Name,
                Breed = p.Breed,
                Age = p.Age,
                Species = p.Species.Name,
                Price = p.Price,
                Status = p.Status
            }).ToListAsync(cancellationToken);
        }
    }

}
