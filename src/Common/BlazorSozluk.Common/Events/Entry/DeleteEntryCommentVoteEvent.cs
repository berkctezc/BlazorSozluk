namespace BlazorSozluk.Common.Events.Entry;

public class DeleteEntryCommentVoteEvent
{
    public Guid EntryCommentId { get; set; }

    public Guid UserId { get; set; }
}