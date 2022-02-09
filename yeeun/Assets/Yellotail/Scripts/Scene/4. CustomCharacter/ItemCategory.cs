using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

namespace Yellotail
{
    public class ItemCategory : MonoBehaviour
    {
        [SerializeField] ToggleGroup categoryToggles;
        [SerializeField] ItemList itemList;
        private void Start()
        {
          itemList.LoadMyItems();
        }
        public void OnCategory(bool value)
        {
            if (value)
            {
                Toggle theActiveCategory;
                theActiveCategory = categoryToggles.ActiveToggles().FirstOrDefault();
                itemList.LoadItemByType(Int32.Parse(theActiveCategory.gameObject.name));
            }
        }

        public void OnMyItem(bool value)
        {
            if (value)
            {
                itemList.LoadMyItems();
            }
        }
    }
}
