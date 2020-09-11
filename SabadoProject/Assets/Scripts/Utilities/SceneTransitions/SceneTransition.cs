using UnityEngine;
using UnityEngine.UI;

namespace RaulSerranoDev
{
    /// <summary>
    /// Clase estática que facilita la transición entre escenas mediante Fade de imagen y audio
    /// </summary>
    public static class SceneTransition
    {
        /// <summary>
        /// Booleana que indica si se está transicionando actualmente
        /// </summary>
        static bool fading = false;

        /// <summary>
        /// Crea el objeto Fader, asigna todos los scripts correspondientes, lo inicializa y empieza el Fade
        /// </summary>
        /// <param name="sceneName">Nombre de la escena a la que se transiciona</param>
        /// <param name="fadeTime">Duración en segundos del Fade</param>
        /// <param name="fadeColor">Color con el que se desvanece</param>
        public static void Fade(string sceneName, float fadeTime, Color fadeColor)
        {
            //Detección de error
            if (fading)
            {
                Debug.Log("Already Fading");
                return;
            }
            fading = true;

            //Creación del gameObject encargado de hacer el gade
            GameObject sceneTransition = new GameObject("SceneFader");

            //Se le añaden los componentes necesarios
            Canvas myCanvas = sceneTransition.AddComponent<Canvas>();
            myCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
            sceneTransition.AddComponent<Fader>();
            sceneTransition.AddComponent<CanvasGroup>();
            sceneTransition.AddComponent<Image>();

            //Se inicia el fade
            Fader fader = sceneTransition.GetComponent<Fader>();
            fader.Init(sceneName, fadeTime, fadeColor);
        }

        /// <summary>
        /// Reinicia variables
        /// </summary>
        public static void DoneFading()
        {
            fading = false;
        }
    }
}