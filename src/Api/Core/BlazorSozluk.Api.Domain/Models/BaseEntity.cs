namespace BlazorSozluk.Api.Domain.Models;

public class BaseEntity
{
    public Guid Id { get; set; }

    public DateTime CreateDate { get; set; }
}