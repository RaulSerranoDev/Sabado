using UnityEngine;

namespace RaulSerranoDev
{
    /// <summary>
    /// Ejemplo de Singleton
    /// </summary>
    public class SingletonExample : MonoBehaviour
    {
        public static SingletonExample Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(this);
        }
    }
}