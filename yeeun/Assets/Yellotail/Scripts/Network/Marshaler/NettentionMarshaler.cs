using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yellotail
{
    public class NettentionMarshaler : Nettention.Proud.Marshaler
    {
        /// <summary>
        /// UserInfo
        /// </summary>
        public static bool Read(Nettention.Proud.Message msg, out List<Yellotail.Common.UserInfo> userInfoList)
        {
            userInfoList = new List<Yellotail.Common.UserInfo>();

            int count = 0;
            msg.ReadScalar(ref count);

            for (int i = 0; i < count; ++i)
            {
                msg.Read(out int id);
                msg.Read(out string name);

                userInfoList.Add(new Yellotail.Common.UserInfo(id, name));
            }
            return true;
        }
        public static void Write(Nettention.Proud.Message msg, List<Yellotail.Common.UserInfo> userInfoList)
        {
            msg.WriteScalar(userInfoList.Count);

            for (int i = 0; i < userInfoList.Count; ++i)
            {
                msg.Write(userInfoList[i].ID);
                msg.Write(userInfoList[i].Name);
            }
        }

        /// <summary>
        /// ItemInfo
        /// </summary>
        public static bool Read(Nettention.Proud.Message msg, out List<Yellotail.Common.ItemInfo> itemInfoList)
        {
            itemInfoList = new List<Yellotail.Common.ItemInfo>();

            int count = 0;
            msg.ReadScalar(ref count);

            for (int i = 0; i < count; ++i)
            {
                msg.Read(out int id);
                msg.Read(out string name);

                itemInfoList.Add(new Yellotail.Common.ItemInfo(id, name));
            }
            return true;
        }
        public static void Write(Nettention.Proud.Message msg, List<Yellotail.Common.ItemInfo> itemInfoList)
        {
            msg.WriteScalar(itemInfoList.Count);

            for (int i = 0; i < itemInfoList.Count; ++i)
            {
                msg.Write(itemInfoList[i].ID);
                msg.Write(itemInfoList[i].Name);
            }
        }

        /// <summary>
        /// RoomInfo
        /// </summary>
        public static bool Read(Nettention.Proud.Message msg, out List<Yellotail.Common.RoomInfo> roomInfoList)
        {
            roomInfoList = new List<Yellotail.Common.RoomInfo>();

            int count = 0;
            msg.ReadScalar(ref count);

            for (int i = 0; i < count; ++i)
            {
                msg.Read(out int id);
                msg.Read(out string name);
                msg.Read(out int index);

                roomInfoList.Add(new Yellotail.Common.RoomInfo(id, name, index));
            }

            return true;
        }
        public static void Write(Nettention.Proud.Message msg, List<Yellotail.Common.RoomInfo> roomInfoList)
        {
            msg.WriteScalar(roomInfoList.Count);

            for (int i = 0; i < roomInfoList.Count; ++i)
            {
                msg.Write(roomInfoList[i].ID);
                msg.Write(roomInfoList[i].Name);
                msg.Write(roomInfoList[i].Index);
            }
        }
    }
}
