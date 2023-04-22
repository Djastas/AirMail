using System.inventorySystem;
using CorpKaktus.scripts.System.DayNightSystem;
using UnityEngine;


namespace System
{
    public class GameSession : MonoBehaviour
    {
        [SerializeField] private DayCycleManager dayCycleManager;
        public float Time => dayCycleManager.timeOfDay;
        
        [SerializeField] private PlayerData data;
        public PlayerData Data => data;
        private void Awake()
        {
            // LoadHud();
            if (IsSessionExit())
            {
                DestroyImmediate(gameObject);
            }
            else
            {
                // InitModels();
                DontDestroyOnLoad(this);
            }
        }

        // private void InitModels()
        // {
        //     QuickInventory = new QuickInventoryModel(data);
        // }

        // private void LoadHud()
        // {
        //     SceneManager.LoadScene("hud", LoadSceneMode.Additive);
        // }

        private bool IsSessionExit()
        {
            var sessions = FindObjectsOfType<GameSession>();
            foreach (var gameSession in sessions)
            {
                if (gameSession != this)
                {
                    return true;
                }
            }
            return false;
        }
    }
}