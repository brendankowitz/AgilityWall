namespace PortableTrello.Client.Requests
{
    public static class ResourcePathFor
    {
        public static string Member(params string[] parts)
        {
            return string.Concat("/members/", string.Join("/", parts));
        }

        public static string List(params string[] parts)
        {
            return string.Concat("/lists/", string.Join("/", parts));
        }

        public static string Card(params string[] parts)
        {
            return string.Concat("/cards/", string.Join("/", parts));
        }

        public static string Board(params string[] parts)
        {
            return string.Concat("/boards/", string.Join("/", parts));
        }
    }
}