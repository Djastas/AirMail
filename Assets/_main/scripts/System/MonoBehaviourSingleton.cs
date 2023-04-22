using UnityEngine;

namespace StvDEV.StarterPack
{
    /// <summary>
    /// Singleton for MonoBehaviour.
    /// </summary>
    /// <typeparam name="T">MonoBehaviour</typeparam>
    public class MonoBehaviourSingleton<T> : MonoBehaviour where T : MonoBehaviourSingleton<T>
    {
        private static T instance;

        /// <summary>
        /// Instance.
        /// </summary>
        public static T Instance
        {
            get
            {
                if (!instance)
                {
                    instance = FindObjectOfType<T>();
                    if (!instance)
                    {
                        Debug.LogError($"Singleton of type {typeof(T)} not contains in scene");
                        return null;
                    }

                    instance.AwakeSingletone();
                }

                return instance;
            }
        }

        /// <summary>
        /// Has instance.
        /// </summary>
        public static bool InstanceIsNotNull => instance;

        private void Awake()
        {
            if (!instance)
            {
                instance = GetComponent<T>();
                AwakeSingletone();
            }
            else if (instance != this)
                Debug.LogError($"Dublicated singleton instance {nameof(T)}", this);
        }

        /// <summary>
        /// On Awake.
        /// </summary>
        protected virtual void AwakeSingletone() { }

        /// <summary>
        /// On Start.
        /// </summary>
        protected virtual void Start() { }
    }
}