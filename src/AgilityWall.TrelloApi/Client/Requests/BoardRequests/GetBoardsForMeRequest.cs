namespace PortableTrello.Client.Requests.BoardRequests
{
    public class GetBoardsForMeRequest : GetBoardsForMemberRequest
    {
        public GetBoardsForMeRequest() : base("me")
        {
        }
    }
}