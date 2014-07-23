using System.Collections.Generic;
using System.Threading.Tasks;
using PortableTrello.Client.Parameters;
using PortableTrello.Client.Requests.BoardRequests;
using PortableTrello.Client.Requests.CardRequests;
using PortableTrello.Client.Requests.ListRequests;
using PortableTrello.Client.Requests.MemberRequests;
using PortableTrello.Contracts;

namespace PortableTrello.Client
{
    public static class TrelloClientExtensions
    {
        public static Task<IEnumerable<Board>> GetBoardsForMe(this ITrelloClient client)
        {
            return client.ExecuteRequest(new GetBoardsForMeRequest());
        }

        public static Task<IEnumerable<Board>> GetBoardsForUser(this ITrelloClient client, string userId)
        {
            return client.ExecuteRequest(new GetBoardsForMemberRequest(userId));
        }

        public static Task<Board> GetBoardById(this ITrelloClient client, string boardId)
        {
            return client.ExecuteRequest(new GetBoardByIdRequest(boardId));
        }

        public static Task<IEnumerable<Card>> GetCardsByBoardId(this ITrelloClient client, string boardId, FilterOptions options = FilterOptions.all)
        {
            return client.ExecuteRequest(new GetCardsByBoardIdRequest(boardId, options));
        }

        public static Task<IEnumerable<List>> GetListsByBoardId(this ITrelloClient client, string boardId, ListFilterOptions options = ListFilterOptions.all, FilterOptions cards = FilterOptions.none)
        {
            return client.ExecuteRequest(new GetListsByBoardIdRequest(boardId, options, cards));
        }

        public static Task<Card> GetCardById(this ITrelloClient client, string cardId)
        {
            return client.ExecuteRequest(new GetCardByIdRequest(cardId));
        }

        public static Task<Member> GetMemberById(this ITrelloClient client, string memberId)
        {
            return client.ExecuteRequest(new GetMemberById(memberId));
        }

        public static Task<Attachment> GetAttachmentById(this ITrelloClient client, string cardId, string attachmentId)
        {
            return client.ExecuteRequest(new GetAttachmentByIdRequest(cardId, attachmentId));
        }

        public static Task<List> GetListById(this ITrelloClient client, string idList, FilterOptions cards = FilterOptions.none)
        {
            return client.ExecuteRequest(new GetListByIdRequest(idList, cards));
        }
    }
}