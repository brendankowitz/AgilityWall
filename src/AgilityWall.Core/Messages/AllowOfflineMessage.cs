namespace AgilityWall.Core.Messages
{
    public class AllowOfflineMessage
    {
        public bool Show { get; private set; }

        public AllowOfflineMessage(bool show)
        {
            Show = show;
        }
    }
}