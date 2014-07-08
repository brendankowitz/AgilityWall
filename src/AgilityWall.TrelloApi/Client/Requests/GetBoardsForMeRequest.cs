namespace PortableTrello.Client.Requests
{
    public class GetBoardsForMeRequest : GetBoardsForUserRequest
    {
        public GetBoardsForMeRequest() : base("me")
        {
        }
    }
}