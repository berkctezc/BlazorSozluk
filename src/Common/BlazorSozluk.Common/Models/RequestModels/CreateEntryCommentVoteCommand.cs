using MediatR;

namespace BlazorSozluk.Common.Models.RequestModels;

public class CreateEntryCommentVoteCommand : IRequest<bool>, IRequest<Guid>
{
    public Guid EntryCommentId { get; set; }

    public VoteType VoteType { get; set; }

    public Guid CreatedBy { get; set; }

    public CreateEntryCommentVoteCommand()
    {
    }

    public CreateEntryCommentVoteCommand(Guid entryCommentId, VoteType voteType, Guid createdBy)
    {
        EntryCommentId = entryCommentId;
        VoteType = voteType;
        CreatedBy = createdBy;
    }
}