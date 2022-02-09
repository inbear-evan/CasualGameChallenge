using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

namespace Yellotail
{
    struct items
    {
        public int index;
        public int type;
        public int cost;
        public string thumnail;
    }
    struct InventoryData
    {
        public List<items> items;
    }
    struct MyData
    {
        public List<int> myitems;
    }

    public class ItemList : MonoBehaviour
    {
        ItemSlot[] slots;
        InventoryData data = new InventoryData();
        MyData mydata = new MyData();

        [SerializeField]
        TextAsset inventoryItems, myItems;

        private void Awake()
        {
            slots = GetComponentsInChildren<ItemSlot>();
            DeserializeItemList();
        }

        private void DeserializeItemList()
        {
            string jdata = inventoryItems.ToString();
            data = JsonConvert.DeserializeObject<InventoryData>(jdata);

            string myData = myItems.ToString();
            mydata = JsonConvert.DeserializeObject<MyData>(myData);

            //Debug.Log(mydata.myitems);
        }

        public void LoadItemByType(int type)
        {
            int cnt = 1;
            int acnt = 0;
            for (int i = 0; i < data.items.Count; i++)
            {
                if (data.items[i].type == type)
                {
                    slots[acnt].UpdateView(data.items[i].cost.ToString(), data.items[i].index);
                    acnt++;
                }
                else
                {
                    slots[slots.Length - cnt].gameObject.SetActive(false);
                    cnt++;
                }
                IsMyItem(i);
            }
        }


        public void LoadMyItems()
        {
            SetActiveSlots();

            int acnt = 0;
            int cnt = 1;
            bool IsMine = false;
            for (int i = 0; i < data.items.Count; i++)
            {
                IsMine = false;
                for (int j = 0; j < mydata.myitems.Count; j++)
                {
                    if (mydata.myitems[j] == data.items[i].index)
                    {
                        slots[acnt].UpdateView(data.items[i].cost.ToString(), data.items[i].index);
                        slots[acnt].UpdateMyView(data.items[i].index);
                        acnt++;
                        IsMine = true;
                    }

                }
                if (!IsMine)
                {
                    slots[slots.Length - cnt].gameObject.SetActive(false);
                    cnt++;
                }
            }
        }

        public void IsMyItem(int index)
        {
            for (int i = 0; i < mydata.myitems.Count; i++)
            {
                if (mydata.myitems[i] == index)
                {
                    slots[i].UpdateMyView(index);
                }
            }
        }

        private void SetActiveSlots()
        {
            for (int i = 0; i < slots.Length -1; i++)
            {
                slots[i].gameObject.SetActive(true);
            }
        }
    }
}
