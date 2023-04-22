using UnityEngine;

namespace System.inventorySystem
{
    [Serializable]
    public class PlayerData
    {
        [SerializeField] private InventoryData inventoryData;
        public InventoryData Inventory => inventoryData;
        
    }
}
