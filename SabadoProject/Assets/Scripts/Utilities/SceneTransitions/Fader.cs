using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace RaulSerranoDev
{
    /// <summary>
    /// Componente encargado de realizar el Fade entre escenas de imagen y audio
    /// </summary>
    public class Fader : MonoBehaviour
    {
        private string _sceneName;                  // Nombre de la escena a la que se transiciona
        private float _fadeTime = 0.0f;             // Duración en segundos del Fade

        private CanvasGroup myCanvas;               // Referencia al canvas

        private bool fadeIn = false;                // Booleana que indica si se está realizando actualmente el fade in

        private AudioSource[] audioSources;         // Referencia a todos los AudioSources en la escena actual
        private float[] initialVolumes;             // Volumen inicial de todos los AudioSources en la escena actual

        /// <summary>
        /// Inicia el fade, inicializando variables y detectando posibles errores
        /// </summary>
        /// <param name="sceneName">Nombre de la escena a la que se transiciona</param>
        /// <param name="fadeTime">Duración en segundos del Fade</param>
        /// <param name="fadeColor">Color con el que se desvanece</param>
        public void Init(string sceneName, float fadeTime, Color fadeColor)
        {
            _sceneName = sceneName;
            _fadeTime = fadeTime;

            //El objeto no se destruye entre escenas
            DontDestroyOnLoad(gameObject);

            //Se obtienen las referencias
            if (transform.GetComponent<CanvasGroup>())
                myCanvas = transform.GetComponent<CanvasGroup>();

            Image image = null;
            if (transform.GetComponentInChildren<Image>())
                image = transform.GetComponent<Image>();

            //Comprobación de errores y empieza la corrutina
            if (myCanvas && image)
            {
                //Establece el color inicial
                image.color = fadeColor;
                myCanvas.alpha = 0.0f;

                //Obtiene todos los audioSources en la escena
                audioSources = FindObjectsOfType<AudioSource>();

                //Inicializa array con el volumen inicial de todos los audios
                initialVolumes = new float[audioSources.Length];
                for (int i = 0; i < audioSources.Length; i++)
                    initialVolumes[i] = audioSources[i].volume;

                StartCoroutine(FadeIt());
            }
            else
                Debug.LogWarning("Something is missing please reimport the package.");
        }

        /// <summary>
        /// Corrutina encargada de realizar el Fade
        /// </summary>
        /// <returns></returns>
        private IEnumerator FadeIt()
        {
            bool fadeEnd = false;           // Booleana que indica si se ha acabado de realizar todo el Fade
            bool startedLoading = false;    // Booleana que indica si se ha empezado a cargar la escena
            float alpha = 0.0f;             // Alpha actual del color del Fade

            //Bucle de Fade
            while (!fadeEnd)
            {
                //Cálculo del nuevo alpha
                alpha = NewAlpha(alpha);

                //Fade in completo
                if (!fadeIn && alpha == 1 && !startedLoading)
                {
                    startedLoading = true;

                    //Limpiamso variables que ya no existen en la nueva escena
                    audioSources = null;
                    initialVolumes = null;

                    SceneManager.LoadScene(_sceneName);
                }

                //Fade out completo
                else if (alpha == 0)
                    fadeEnd = true;

                //Aplica el alpha correspondiente a la transparencia
                myCanvas.alpha = alpha;

                //Comprobación de si hay audioSources en la escena
                //Aplica el alpha correspondiente a las fuentes de sonido
                if (audioSources != null)
                    for (int i = 0; i < audioSources.Length; i++)
                        audioSources[i].volume = initialVolumes[i] * (1 - alpha);

                yield return null;
            }

            //Fade acabado, se destruye el objeto

            SceneTransition.DoneFading();

            Debug.Log("Your scene has been loaded , and fading in has just ended");

            Destroy(gameObject);

            yield return null;
        }

        /// <summary>
        /// Calcula el nuevo valor de alpha
        /// </summary>
        /// <param name="to">Hacia que alpha transiciona</param>
        /// <param name="currAlpha">alpha actual</param>
        /// <returns></returns>
        private float NewAlpha(float currAlpha)
        {
            if (fadeIn)
            {
                currAlpha -= Time.deltaTime / _fadeTime;
                if (currAlpha < 0)
                    currAlpha = 0;
            }
            else
            {
                currAlpha += Time.deltaTime / _fadeTime;
                if (currAlpha > 1)
                    currAlpha = 1;

            }
            return currAlpha;
        }

        /// <summary>
        /// Suscripción a evento de escena cargada para empezar a realizar el fade in
        /// </summary>
        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnLevelFinishedLoading;
        }

        /// <summary>
        /// Desuscripción a evento de escena cargada antes de que el objeto se destruya
        /// </summary>   
        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnLevelFinishedLoading;
        }

        /// <summary>
        /// Es llamado cuando la escena ha cargado.
        /// Obtiene los nuevos AudioSources y hace que empiece el fade in.
        /// </summary>
        /// <param name="scene"></param>
        /// <param name="mode"></param>
        private void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
        {
            fadeIn = true;

            //Obtiene todos los audioSources en la escena
            audioSources = FindObjectsOfType<AudioSource>();

            //Inicializa array con el volumen inicial de todos los audios
            initialVolumes = new float[audioSources.Length];
            for (int i = 0; i < audioSources.Length; i++)
                initialVolumes[i] = audioSources[i].volume;
        }
    }
}