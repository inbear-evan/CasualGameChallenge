using Com.TheFallenGames.OSA.Core;
using UnityEngine.UI;

namespace Yellotail.InGame
{
    public class ChatView : BaseItemViewsHolder
    {
        private ChatItem item;

        public override void CollectViews()
        {
            base.CollectViews();

            item = root.GetComponent<ChatItem>();
            item.gameObject.SetActive(true);

            var csf = root.GetComponent<ContentSizeFitter>();
            csf.enabled = true;
        }

        public void UpdateView(ChatModel model)
        {
            item.UpdateView(model);
        }
    }
}
