using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Yellotail
{
    public class ItemSlot : MonoBehaviour
    {
        [SerializeField] TMP_Text itemCost;
        [SerializeField] int itemIndex;
        [SerializeField] GameObject priceTag;
        [SerializeField] Image checkMine;

        public void UpdateView(string cost, int index)
        {
            priceTag.SetActive(true);
            checkMine.enabled = false;
            itemCost.text = cost.ToString();
            itemIndex = index;
            Debug.Log("itemindex " + index);
        }

        public void OnItemClicked()
        {
            Debug.Log("cost : " + itemCost.text + " / " + "index : " + itemIndex);
        }

        public void UpdateMyView(int index)
        {
            priceTag.SetActive(false);
            itemIndex = index;
            checkMine.enabled = true;
        }
    }
}
