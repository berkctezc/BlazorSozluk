namespace BlazorSozluk.Common;

public static class SozlukConstants
{
    public const string RabbitMqHost = "localhost";
    public const string DefaultExchangeType = "direct";

    public const string UserExchangeName = "UserExchange";
    public const string UserEmailChangedQueueName = "UserEmailChangedQueue";

    public const string FavExchangeName = "FavExchange";
    public const string CreateEntryFavQueueName = "CreateEntryFavQueue";
    public const string CreateEntryCommentFavQueueName = "CreateEntryCommentFavQueue";
    public const string DeleteEntryFavQueueName = "DeleteEntryFavQueue";

    public const string DeleteEntryVoteQueueName = "DeleteEntryVoteQueue";

    public const string VoteExchangeName = "VoteExchange";
    public const string CreateEntryVoteQueueName = "CreateEntryVoteQueue";

    public const string CreateEntryCommentVoteQueueName = "CreateEntryCommentVoteQueue";

    public const string DeleteEntryCommentFavQueueName = "DeleteEntryCommentFavQueue";

    public const string DeleteEntryCommentVoteQueueName = "DeleteEntryCommentVoteQueue";
}