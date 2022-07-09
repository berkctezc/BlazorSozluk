namespace BlazorSozluk.Common.Events.Entry;

public class DeleteEntryCommentFavEvent
{
    public Guid EntryCommentId { get; set; }

    public Guid CreatedBy { get; set; }
}