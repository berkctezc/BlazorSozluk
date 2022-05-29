using BlazorSozluk.Api.Application.Interfaces.Repositories;
using BlazorSozluk.Api.Domain.Models;
using BlazorSozluk.Infrastructure.Persistence.Context;

namespace BlazorSozluk.Infrastructure.Persistence.Repositories;

public class EntryFavoriteRepository : GenericRepository<EntryFavorite>, IEntryFavoriteRepository
{
    protected EntryFavoriteRepository(BlazorSozlukContext dbContext) : base(dbContext)
    {
    }
}