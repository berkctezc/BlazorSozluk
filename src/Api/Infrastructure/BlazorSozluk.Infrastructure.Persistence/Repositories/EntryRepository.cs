using BlazorSozluk.Api.Application.Interfaces.Repositories;
using BlazorSozluk.Api.Domain.Models;
using BlazorSozluk.Infrastructure.Persistence.Context;

namespace BlazorSozluk.Infrastructure.Persistence.Repositories;

public class EntryRepository : GenericRepository<Entry>, IEntryRepository
{
    protected EntryRepository(BlazorSozlukContext dbContext) : base(dbContext)
    {
    }
}