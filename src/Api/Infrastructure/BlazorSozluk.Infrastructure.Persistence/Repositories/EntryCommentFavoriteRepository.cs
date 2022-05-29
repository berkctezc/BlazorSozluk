using BlazorSozluk.Api.Application.Interfaces.Repositories;
using BlazorSozluk.Api.Domain.Models;
using BlazorSozluk.Infrastructure.Persistence.Context;

namespace BlazorSozluk.Infrastructure.Persistence.Repositories;

public class EntryCommentFavoriteRepository : GenericRepository<EntryCommentFavorite>, IEntryCommentFavoriteRepository
{
    protected EntryCommentFavoriteRepository(BlazorSozlukContext dbContext) : base(dbContext)
    {
    }
}