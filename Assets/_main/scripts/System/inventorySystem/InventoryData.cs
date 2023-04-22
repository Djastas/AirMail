using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace System.inventorySystem
{
    [Serializable]
    public class InventoryData : MonoBehaviour
    {
        [SerializeField] private List<InventoryItemData> inventoryItemDatas = new List<InventoryItemData>();

        public delegate void OnInventoryCange(string id, int value);

        public OnInventoryCange onchanged;
        
        public void Add(string id , int value)
        {
            if (value <= 0) return;
            var itemdef =  DefFacede.I.Items.Get(id);
            if(itemdef.IsVoid) return;
            var isFull = inventoryItemDatas.Count >= DefFacede.I.Player.InventorySize;
            
            if (itemdef.HasTag(ItemTag.Stackable))
            {
                var item = GetItem(id);
                if (item == null)
                {
                    if (isFull) return;

                    item = new InventoryItemData(id);
                    inventoryItemDatas.Add(item);
                }

                item.value += value;
            }
            else
            {
                
                for (int i = 0; i < value; i++)
                {
                    isFull = inventoryItemDatas.Count >= DefFacede.I.Player.InventorySize;
                    if (isFull) return;
                    var item = new InventoryItemData(id) { value = 1 };
                    inventoryItemDatas.Add(item);
                }
            }
            
            onchanged?.Invoke(id,Count(id));
        }

        public InventoryItemData[] GetAll(params ItemTag[] tags)
        {
            var retValue = new List<InventoryItemData>();
            foreach (var item in inventoryItemDatas)
            {
                var itemDef = DefFacede.I.Items.Get(item.id);
                var isAllRequirementMet = tags.All(x => itemDef.HasTag(x));
                if(isAllRequirementMet)
                    retValue.Add(item);

            }
            return retValue.ToArray();
          
        }

        public void Remove(string id , int value)
        {
            var itemdef =  DefFacede.I.Items.Get(id);
            if(itemdef.IsVoid) return;
            if (itemdef.HasTag(ItemTag.Stackable))
            {
                var item = GetItem(id);
                if(item == null) return;
                item.value -= value;

                if (item.value <= 0)
                {
                    inventoryItemDatas.Remove(item);
                }
            }
            else
            {
                for (int i = 0; i < value; i++)
                {
                    var item = GetItem(id);
                    if(item == null) return;
                    inventoryItemDatas.Remove(item);
                    
                }
            }
           
            onchanged?.Invoke(id,Count(id));
        }

        private InventoryItemData GetItem(string id)
        {
            foreach (var itemData in inventoryItemDatas)
            {
                if (itemData.id == id) return itemData;

            }

            return null;
        }

        public int Count(string id)
        {
            var count = 0;
            foreach (var itemData in inventoryItemDatas)
            {
                if (itemData.id == id)
                {
                    count += itemData.value;
                }
            }
            return count;
        }
        
        
        
        [Serializable]
        public class InventoryItemData
        {
            [InventoryId]  public string id;
            public int value;
            public InventoryItemData(string id)
            {
                this.id = id;
            }
        }
    }
}
