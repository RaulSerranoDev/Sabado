using TMPro;
using UnityEngine;

namespace Sabado
{
    /// <summary>
    /// Controlador de la escena de menu
    /// </summary>
    public class MenuManager : MonoBehaviour
    {
        [Header("Text References")]
        [SerializeField] private TextMeshProUGUI titleText = null;

        [Header("Fade Settings")]
        [SerializeField] private Color fadeColor = Color.white;
        [SerializeField] private float fadeTime = 0f;

        /// <summary>
        /// Inicializa el menú
        /// </summary>
        private void Start()
        {
            titleText.text = GameManager.instance.GameName;
        }

        /// <summary>
        /// Paso a la escena de juego
        /// </summary>
        public void StartGame()
        {
            SceneTransition.Fade("Street", fadeTime, fadeColor);
        }
    }
}
