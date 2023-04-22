using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace System.inventorySystem
{
    [CreateAssetMenu(menuName = "Data/InventoryItem",fileName = "InventoryItem")]
    public class InventoryItemDef : ScriptableObject
    {
        [SerializeField] private ItemDef[] items;

        public ItemDef Get(string id)
        {
            foreach (var itemDef in items)
            {
                if (itemDef.Id == id)
                {
                    return itemDef;
                }
            }

            return default;
        }
        #if UNITY_EDITOR
        public ItemDef[] ItemsForEditors => items;
        #endif
    }
    
[Serializable]
    public struct ItemDef
    {
        [SerializeField] private string id;

        [SerializeField] private Sprite sprite;
        [SerializeField] private ItemTag[] tags;

        public Sprite Sprite => sprite;
        public string Id => id;
        
        public bool IsVoid => string.IsNullOrEmpty(id);

        public bool HasTag(ItemTag tag)
        {
            return tags.Contains(tag);
        }


    }
    [Serializable]
    public class Event : UnityEvent<GameObject>
    {
            
    }
}
