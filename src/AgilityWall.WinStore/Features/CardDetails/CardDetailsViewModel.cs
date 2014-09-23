using System;
using Windows.ApplicationModel.DataTransfer;
using AgilityWall.Core.Features.CardDetails;
using Caliburn.Micro;
using PortableTrello.Client;

namespace AgilityWall.WinStore.Features.CardDetails
{
    public class CardDetailsViewModel : Core.Features.CardDetails.CardDetailsViewModel, ISupportSharing
    {
        public CardDetailsViewModel(ITrelloClient trelloClient, CardActionsViewModel.Factory actionFactory)
            : base(trelloClient, actionFactory)
        {
        }

        public void OnShareRequested(DataRequest dataRequest)
        {
            dataRequest.Data.Properties.Title = Card.Name;
            if(!string.IsNullOrEmpty(Card.Desc))
                dataRequest.Data.Properties.Description = Card.Desc;

            dataRequest.Data.SetText(Card.Name);
            dataRequest.Data.SetUri(new Uri(Card.Url));
        }
    }
}