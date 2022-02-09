using Com.TheFallenGames.OSA.Core;
using Com.TheFallenGames.OSA.CustomParams;
using Com.TheFallenGames.OSA.DataHelpers;
using System.Collections.Generic;
using UnityEngine;

namespace Yellotail.Lobby
{
    public class RoomTypeList : OSA<BaseParamsWithPrefab, RoomTypeView>
    {
        public SimpleDataHelper<RoomTypeModel> Data { get; private set; }

        public RoomTypeView SelectedView { get; private set; } = null;

        private float contentCenter, contentRange;
        private const float contentScaleMin = 0.875f;
        private const float contentScaleScale = 1 - contentScaleMin;

        #region OSA implementation
        protected override void Start()
        {
            Data = new SimpleDataHelper<RoomTypeModel>(this);
            base.Start();

            var contentSize = GetComponent<RectTransform>().rect.size;
            contentCenter = contentSize.x / 2f;
            contentRange = contentCenter;
        }

        protected override void Update()
        {
            base.Update();

            for (int i = 0; i < VisibleItemsCount; i++)
            {
                var itemView = GetItemViewsHolder(i);
                var itemX = itemView.Rect.localPosition.x;
                var itemScale = GetItemScale(itemX);

                itemView.SetScale(itemScale);
            }

            int centerViewIndex = GetCenterViewIndex();
            var centerItemView = GetItemViewsHolder(centerViewIndex);

            if (centerItemView != SelectedView)
            {
                SelectedView = centerItemView;
                SelectedView.Select();
            
                Debug.Log(SelectedView.Index);
            }
        }

        private float GetItemScale(float contentPosX)
        {
            float percent;
            if (contentPosX > contentCenter)
            {
                contentPosX -= contentRange;
                percent = 1 - (contentPosX / contentRange);
            }
            else
            {
                percent = contentPosX / contentRange;
            }

            return contentScaleMin + (percent * contentScaleScale);
        }

        private int GetCenterViewIndex()
        {
            int closeToCenterIndex = 0;
            float closeToOffset = contentCenter;

            for (int i = 0; i < VisibleItemsCount; i++)
            {
                var itemView = GetItemViewsHolder(i);
                var itemX = itemView.Rect.localPosition.x;

                var offset = Mathf.Abs(contentCenter - itemX);
                if (offset < closeToOffset)
                {
                    closeToCenterIndex = i;
                    closeToOffset = offset;
                }
            }

            return closeToCenterIndex;
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            var roomTypeModels = new List<RoomTypeModel>()
            {
                new RoomTypeModel() { index = 0, name = "컨퍼런스 홀" },
                new RoomTypeModel() { index = 1, name = "사무실" },
                new RoomTypeModel() { index = 0, name = "컨퍼런스 홀" },
                new RoomTypeModel() { index = 1, name = "사무실" },
                new RoomTypeModel() { index = 0, name = "컨퍼런스 홀" },
                new RoomTypeModel() { index = 1, name = "사무실" }
            };
            Data.ResetItems(roomTypeModels);
        }

        protected override RoomTypeView CreateViewsHolder(int itemIndex)
        {
            var instance = new RoomTypeView();
            instance.Init(_Params.ItemPrefab, _Params.Content, itemIndex);

            return instance;
        }

        protected override void UpdateViewsHolder(RoomTypeView newOrRecycled)
        {
            var model = Data[newOrRecycled.ItemIndex];
            newOrRecycled.UpdateView(model);
        }

        protected override void OnBeforeRecycleOrDisableViewsHolder(RoomTypeView inRecycleBinOrVisible, int newItemIndex)
        {
            base.OnBeforeRecycleOrDisableViewsHolder(inRecycleBinOrVisible, newItemIndex);
            inRecycleBinOrVisible.OnDisableView();
        }

        #endregion

        #region data manipulation
        public void AddItemsAt(int index, IList<RoomTypeModel> items)
        {
            Data.InsertItems(index, items);
        }

        public void RemoveItemsFrom(int index, int count)
        {
            Data.RemoveItems(index, count);
        }

        public void SetItems(IList<RoomTypeModel> items)
        {
            Data.ResetItems(items);
        }
        #endregion
    }
}
