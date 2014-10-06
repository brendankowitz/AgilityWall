using AgilityWall.Core.Features.CardDetails;
using Caliburn.Micro;
using Microsoft.Phone.Tasks;
using PortableTrello.Client;
using TaskResult = Microsoft.Phone.Tasks.TaskResult;

namespace AgilityWall.WinPhone.Features.CardDetails
{
    public class CardDetailsViewModel : Core.Features.CardDetails.CardDetailsViewModel,
        IHandle<TaskCompleted<PhotoResult>>
    {
        private readonly IEventAggregator _eventAggregator;

        public CardDetailsViewModel(ITrelloClient trelloClient, 
            CardActionsViewModel.Factory actionFactory,
            IEventAggregator eventAggregator)
            : base(trelloClient, actionFactory)
        {
            _eventAggregator = eventAggregator;
        }

        public void AttachImage()
        {
            _eventAggregator.RequestTask<CameraCaptureTask>();
        }

        public void Handle(TaskCompleted<PhotoResult> message)
        {
            if (message.Result.TaskResult == TaskResult.OK)
            {

            }
        }
    }
}