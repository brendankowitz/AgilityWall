using Caliburn.Micro;

namespace AgilityWall.Core.Features.Shared.Designer
{
    public class DesignNetworkErrorViewModel : NetworkErrorViewModel
    {
        public DesignNetworkErrorViewModel() : base(null)
        {
            ShowOfflineStatus = false;
        }
    }
}