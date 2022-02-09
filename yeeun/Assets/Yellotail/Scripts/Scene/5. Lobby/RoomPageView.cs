using Com.TheFallenGames.OSA.Core;

namespace Yellotail.Lobby
{
    public class RoomPageView : BaseItemViewsHolder
    {
        private RoomPageItem item;
     

        public override void CollectViews()
        {
            base.CollectViews();
            item = root.GetComponent<RoomPageItem>();
            item.gameObject.SetActive(true);
        }

        public void UpdateView(RoomPageModel model)
        {
            item.UpdateView(model);
        
        }

        public void OnDisableView()
        {
            item.OnDisableView();
        }
    }
}
