using Com.TheFallenGames.OSA.Core;
using Com.TheFallenGames.OSA.CustomParams;
using Com.TheFallenGames.OSA.DataHelpers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yellotail.InGame.Chat;

namespace Yellotail.InGame
{
    public enum ChatType
    {
        Enter,
        Message
    }

    public class ChatList : OSA<BaseParamsWithPrefab, ChatView>
    {
        public SimpleDataHelper<ChatModel> Data { get; private set; }

        private const string formatMessage = "[{0}] {1}";
        private const string formatEnter = "{0} 님이 입장했어요."; 

        public ChatModel GetChatModel(ChatType type, string id, string message)
        {
            return type switch
            {
                ChatType.Enter => new ChatModel() { color = ChatColors.Enter, message = string.Format(formatEnter, id, message) },
                ChatType.Message => new ChatModel() { color = ChatColors.Message, message = string.Format(formatMessage, id, message) },              
                _ => throw new System.NotImplementedException()
            };
        }

        #region OSA implementation
        protected override void Start()
        {
            Data = new SimpleDataHelper<ChatModel>(this);
            base.Start();
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            var models = new List<ChatModel>()
            {
                GetChatModel(ChatType.Enter, "시녕", ""),
                GetChatModel(ChatType.Message, "시녕", "안녕하세요."),
                GetChatModel(ChatType.Message, "시녕", "여기가 어디죠?"),
                GetChatModel(ChatType.Enter, "Stella", ""),
                GetChatModel(ChatType.Message, "Stella", "방가방가"),
                GetChatModel(ChatType.Message, "시녕", "너무 좋아요!"),
                GetChatModel(ChatType.Message, "시녕", "코로나가 빨리 사라졌으면 좋겠어요. 너무 힘들어요......."),
                GetChatModel(ChatType.Message, "Stella", "저두요...넘나 힘듦."),
                GetChatModel(ChatType.Message, "시녕", "오늘은 어떤 일이 일어날까요?"),
                GetChatModel(ChatType.Message, "Stella", "우리는 폭풍의 코어에 들어와있습니다."),
                GetChatModel(ChatType.Enter, GameManager.Instance.GameData.User.Name, "")
            };

            SetItems(models);
            ScrollTo(models.Count - 1);
        }

        protected override ChatView CreateViewsHolder(int itemIndex)
        {
            var instance = new ChatView();
            instance.Init(_Params.ItemPrefab, _Params.Content, itemIndex);

            return instance;
        }

        protected override void UpdateViewsHolder(ChatView newOrRecycled)
        {
			ChatModel model = Data[newOrRecycled.ItemIndex];
            newOrRecycled.UpdateView(model);
            ScheduleComputeVisibilityTwinPass(true); // for Content Size Fitter
        }
        #endregion

        #region data manipulation
        public void AddItemAtEnd(ChatModel model)
        {
            Data.InsertOneAtEnd(model);
            ScrollTo(Data.Count - 1);
        }

        public void SetItems(IList<ChatModel> items)
        {
            Data.ResetItems(items);
        }
        #endregion
    }
}
