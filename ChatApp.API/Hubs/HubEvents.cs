namespace ChatApp.API.Hubs
{
    public static class HubEvents
    {
        public const string ReceiveMessage =
            nameof(ReceiveMessage);

        public const string MessageDeleted =
            nameof(MessageDeleted);

        public const string MessageUpdated =
            nameof(MessageUpdated);

        public const string MessageSeen =
            nameof(MessageSeen);

        public const string UserTyping =
            nameof(UserTyping);

        public const string UserStoppedTyping =
            nameof(UserStoppedTyping);
    }
}