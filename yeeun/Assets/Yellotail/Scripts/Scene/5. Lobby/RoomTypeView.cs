using Com.TheFallenGames.OSA.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Yellotail.Lobby
{
    public class RoomTypeView : BaseItemViewsHolder
	{
		private RoomTypeItem item;
		public RectTransform Rect => item.Rect;
		public Toggle Toggle => item.Toggle;
		public int Index;

		// Retrieving the views from the item's root GameObject
		public override void CollectViews()
		{
			base.CollectViews();
			item = root.GetComponent<RoomTypeItem>();
			item.gameObject.SetActive(true);
		}

		public void UpdateView(RoomTypeModel model)
        {
			item.UpdateView(model);
			Index = model.index;
        }

		public void OnDisableView()
        {
			item.OnDisableView();
        }

		public void SetScale(float scale)
        {
			Rect.localScale = new Vector3(scale, scale, scale);
        }

		public void Select()
        {
			Toggle.isOn = true;
		}
	}
} 