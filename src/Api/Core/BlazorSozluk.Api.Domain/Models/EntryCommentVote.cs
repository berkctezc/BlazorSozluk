using BlazorSozluk.Common.Models;

namespace BlazorSozluk.Api.Domain.Models;

public class EntryCommentVote : BaseEntity
{
    public Guid EntryCommentVoteId { get; set; }

    public VoteType VoteType { get; set; }

    public Guid CreatedById { get; set; }

    public Guid EntryCommentId { get; set; }

    public virtual EntryComment EntryComment { get; set; }
}