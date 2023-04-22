using UnityEngine;

namespace System.inventorySystem
{
    [CreateAssetMenu(menuName = "Defs/PlayerDef",fileName = "PlayerDef")]
    public class PlayerDef : ScriptableObject
    {
        [SerializeField] private int inventorySize;
        [SerializeField] private int maxHealth;
        public int InventorySize => inventorySize;
        public int MaxHealth => maxHealth;
        
    }
}