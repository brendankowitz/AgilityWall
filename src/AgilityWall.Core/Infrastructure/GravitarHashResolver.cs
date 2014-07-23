using System.Collections.Generic;
using System.Threading.Tasks;
using PortableTrello.Client;
using PortableTrello.Client.Fields;
using PortableTrello.Client.Requests.MemberRequests;
using PortableTrello.Contracts;

namespace AgilityWall.Core.Infrastructure
{
    public interface IAvatarUrlResolver
    {
        Task<IEnumerable<string>> GetTrelloMemberGravitar(int preferredSize = 56, params string[] trelloMemberId);
    }

    public class AvatarUrlResolver : IAvatarUrlResolver
    {
        private readonly ITrelloClient _client;
        readonly IDictionary<string,string> _resolvedHashes = new Dictionary<string, string>();
        readonly object _insertLock = new object();

        public AvatarUrlResolver(ITrelloClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<string>> GetTrelloMemberGravitar(int preferredSize = 30, params string[] trelloMemberId)
        {
            var response = new List<string>();
            foreach (var member in trelloMemberId)
            {
                if (!_resolvedHashes.ContainsKey(member))
                {
                    var hashResponse =
                        await _client.ExecuteRequest(new GetMemberById(member, MemberFields.avatarHash | MemberFields.avatarSource | MemberFields.gravatarHash));
                    lock (_insertLock)
                    {
                        if (!_resolvedHashes.ContainsKey(member))
                        {
                            if (hashResponse.AvatarSource == AvatarSource.upload || !string.IsNullOrEmpty(hashResponse.AvatarHash))
                            {
                                _resolvedHashes.Add(member,
                                    string.Format("https://trello-avatars.s3.amazonaws.com/{0}/30.png", hashResponse.AvatarHash));
                            }
                            else
                            {
                                _resolvedHashes.Add(member,
                                    string.Format("http://www.gravatar.com/avatar/{0}?size={1}", hashResponse.GravatarHash, preferredSize));
                            }
                        }
                    }
                }

                response.Add(_resolvedHashes[member]);
            }
            return response;
        }
    }
}