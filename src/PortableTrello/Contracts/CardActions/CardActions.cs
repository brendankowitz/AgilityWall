using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace PortableTrello.Contracts.CardActions
{
    public class CardAction
    {
        public string Id { get; set; }
        public string IdMemberCreator { get; set; }
        public CardActionData Data { get; set; }
        [JsonProperty("type", Required = Required.Always)]
        [JsonConverter(typeof(StringEnumConverter))]
        public CardActionType Type { get; set; }
        public DateTime Date { get; set; }
        public CardActionMemberCreator MemberCreator { get; set; }
    }

}