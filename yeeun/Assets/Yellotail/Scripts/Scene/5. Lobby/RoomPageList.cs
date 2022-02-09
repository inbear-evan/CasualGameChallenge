using Com.TheFallenGames.OSA.Core;
using Com.TheFallenGames.OSA.CustomParams;
using Com.TheFallenGames.OSA.DataHelpers;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Yellotail.Lobby
{
    public class RoomPageList : OSA<BaseParamsWithPrefab, RoomPageView>
    {
        public SimpleDataHelper<RoomPageModel> Data { get; private set; }

        #region OSA implementation
        protected override void Start()
        {
            Data = new SimpleDataHelper<RoomPageModel>(this);
            base.Start();
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            var models = new List<RoomPageModel>()
            {
                new RoomPageModel()
                {
                    rooms = new List<RoomInfo>()
                    {
                        new RoomInfo()
                        {
                            index = 0,
                            title = "안녕하세요.",
                            master = "25종",
                            thumnail = null
                        },
                        new RoomInfo()
                        {   index = 1,
                            title = "함께해요!!!!",
                            master = "기100",
                            thumnail = null
                        },
                        new RoomInfo()
                        {  index = 2,

                           title = "드루와~드루와~",
                            master = "제임스",
                            thumnail = null

                        },
                        new RoomInfo()
                        {
                            index = 0,
                            title = "여기가 어디지......",
                            master = "있는둥없는둥",
                            thumnail = null

                        },
                        new RoomInfo()
                        {
                        index = 2,
                           title = "렛츠 플레이~",
                            master = "스티븐",
                            thumnail = null

                        }
                    }
                },
                new RoomPageModel()
                {
                    rooms = new List<RoomInfo>()
                    {
                        new RoomInfo()
                        {
                            index = 0,
                            title = "너무 좋아요!!!",
                            master = "시녕",
                            thumnail = null

                        },
                        new RoomInfo()
                        {
                            index = 1,
                            title = "감기약이 졸려요T.T",
                            master = "Stella",
                            thumnail = null

                        }
                    }
                }
            };

            SetItems(models);
        }

        protected override RoomPageView CreateViewsHolder(int itemIndex)
        {
            var instance = new RoomPageView();
            instance.Init(_Params.ItemPrefab, _Params.Content, itemIndex);

            return instance;
        }

        protected override void UpdateViewsHolder(RoomPageView newOrRecycled)
        {
            var model = Data[newOrRecycled.ItemIndex];
            newOrRecycled.UpdateView(model);
        }

        protected override void OnBeforeRecycleOrDisableViewsHolder(RoomPageView inRecycleBinOrVisible, int newItemIndex)
        {
            base.OnBeforeRecycleOrDisableViewsHolder(inRecycleBinOrVisible, newItemIndex);
            inRecycleBinOrVisible.OnDisableView();
        }

        protected override void OnItemIndexChangedDueInsertOrRemove(RoomPageView shiftedViewsHolder, int oldIndex, bool wasInsert, int removeOrInsertIndex)
        {
            base.OnItemIndexChangedDueInsertOrRemove(shiftedViewsHolder, oldIndex, wasInsert, removeOrInsertIndex);
        }
        #endregion

        #region data manipulation
        public void AddItemsAt(int index, IList<RoomPageModel> items)
        {
            Data.InsertItems(index, items);
        }

        public void RemoveItemsFrom(int index, int count)
        {
            Data.RemoveItems(index, count);
        }

        public void SetItems(IList<RoomPageModel> items)
        {
            Data.ResetItems(items);
        }
        #endregion

        #region NestedScroll Control
        private bool routeToParent = false;

        /// <summary> Do action for all parents </summary>
        private void DoForParents<T>(Action<T> action) where T : IEventSystemHandler
        {
            Transform parent = transform.parent;
            while (parent != null)
            {
                foreach (var component in parent.GetComponents<Component>())
                {
                    if (component is T)
                        action((T)(IEventSystemHandler)component);
                }
                parent = parent.parent;
            }
        }

        /// <summary> Always route initialize potential drag event to parents </summary>
        public override void OnInitializePotentialDrag(PointerEventData eventData)
        {
            DoForParents<IInitializePotentialDragHandler>((parent) => { parent.OnInitializePotentialDrag(eventData); });
            base.OnInitializePotentialDrag(eventData);
        }

        /// <summary> Drag event </summary>
        public override void OnDrag(PointerEventData eventData)
        {
            if (routeToParent)
                DoForParents<IDragHandler>((parent) => { parent.OnDrag(eventData); });
            else
                base.OnDrag(eventData);
        }

        /// <summary> Begin drag event </summary>
        public override void OnBeginDrag(PointerEventData eventData)
        {
            if (!IsHorizontal && Math.Abs(eventData.delta.x) > Math.Abs(eventData.delta.y))
                routeToParent = true;
            else if (!IsVertical && Math.Abs(eventData.delta.x) < Math.Abs(eventData.delta.y))
                routeToParent = true;
            else
                routeToParent = false;

            if (routeToParent)
                DoForParents<IBeginDragHandler>((parent) => { parent.OnBeginDrag(eventData); });
            else
                base.OnBeginDrag(eventData);
        }

        /// <summary> End drag event </summary>
        public override void OnEndDrag(PointerEventData eventData)
        {
            if (routeToParent)
                DoForParents<IEndDragHandler>((parent) => { parent.OnEndDrag(eventData); });
            else
                base.OnEndDrag(eventData);
            routeToParent = false;
        }
        #endregion
    }
}
