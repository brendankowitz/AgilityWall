using Caliburn.Micro;

namespace AgilityWall.WinPhone.Features.CardDetails
{
    public class CardDetailsViewModelStorageHandler : StorageHandler<CardDetailsViewModel>
    {
        public override void Configure()
        {
            Property(x => x.DisplayName).RestoreAfterActivation()
                .InPhoneState();
        }
    }
}