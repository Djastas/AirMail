using UnityEngine;

namespace System.inventorySystem
{
    [CreateAssetMenu(menuName = "Defs/defFacede",fileName = "defFacede")]

    public class DefFacede : ScriptableObject
    {
        [SerializeField] private InventoryItemDef items;
        
        [SerializeField] private PlayerDef player;


        public PlayerDef Player => player;
        public InventoryItemDef Items => items;
        private static DefFacede _intance;
        public static DefFacede I => _intance == null ? LoadDefs() : _intance;

        private static DefFacede LoadDefs()
        {
            _intance = UnityEngine.Resources.Load<DefFacede>("defFacede");
            return _intance;

        }

       
    }
}