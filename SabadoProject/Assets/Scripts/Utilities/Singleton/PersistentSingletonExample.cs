using UnityEngine;

namespace RaulSerranoDev
{
    /// <summary>
    /// Ejemplo de Singleton Persistente entre escenas
    /// </summary>
    public class PersistentSingletonExample : MonoBehaviour
    {
        public static PersistentSingletonExample Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
                Destroy(this);
        }
    }
}
