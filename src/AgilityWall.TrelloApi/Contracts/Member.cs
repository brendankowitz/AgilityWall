using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace PortableTrello.Contracts
{
    public class Member
    {
        public string Id { get; set; }
        public string AvatarHash { get; set; }
        public string Bio { get; set; }
        public object BioData { get; set; }
        public bool Confirmed { get; set; }
        public string FullName { get; set; }
        public List<object> IdPremOrgsAdmin { get; set; }
        public string Initials { get; set; }
        public string MemberType { get; set; }
        public List<object> Products { get; set; }
        public string Status { get; set; }
        public string Url { get; set; }
        public string Username { get; set; }
        public AvatarSource AvatarSource { get; set; }
        public object Email { get; set; }
        public string GravatarHash { get; set; }
        public List<string> IdBoards { get; set; }
        public List<string> IdBoardsPinned { get; set; }
        public List<string> IdOrganizations { get; set; }
        public object LoginTypes { get; set; }
        public object NewEmail { get; set; }
        public List<string> OneTimeMessagesDismissed { get; set; }
        public Prefs Prefs { get; set; }
        public List<string> Trophies { get; set; }
        public object UploadedAvatarHash { get; set; }
        public List<object> PremiumFeatures { get; set; }
    }
}