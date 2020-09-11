using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Componente encargado de realizar el Fade entre escenas
/// </summary>
public class Fader : MonoBehaviour
{
    private string _sceneName;          // Nombre de la escena a la que se transiciona
    private float _fadeTime = 0.0f;     // Duración en segundos del Fade
    private Color _fadeColor;           // Color con el que se desvanece

    CanvasGroup myCanvas;               // Referencia al canvas

    private bool fadeIn = false;        // Booleana que indica si se está realizando actualmente el fade in

    /// <summary>
    /// Inica el fade, inicializando variables y detectando posibles errores
    /// </summary>
    /// <param name="sceneName">Nombre de la escena a la que se transiciona</param>
    /// <param name="fadeTime">Duración en segundos del Fade</param>
    /// <param name="fadeColor">Color con el que se desvanece</param>
    public void Init(string sceneName, float fadeTime, Color fadeColor)
    {
        _sceneName = sceneName;
        _fadeTime = fadeTime;
        _fadeColor = fadeColor;

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
            image.color = _fadeColor;
            myCanvas.alpha = 0.0f;
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
            //Fade in
            if (!fadeIn)
            {
                //Cálculo del nuevo alpha
                alpha = newAlpha(1, alpha);

                //Fade in completo
                if (alpha == 1 && !startedLoading)
                {
                    startedLoading = true;
                    SceneManager.LoadScene(_sceneName);
                }
            }

            //Fade out
            else
            {
                //Cálculo del nuevo alpha
                alpha = newAlpha(0, alpha);

                //Fade out completo
                if (alpha == 0)
                    fadeEnd = true;
            }

            //Aplica el alpha correspondiente
            myCanvas.alpha = alpha;

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
    private float newAlpha(int to, float currAlpha)
    {
        switch (to)
        {
            case 0:
                currAlpha -= _fadeTime * Time.deltaTime;
                if (currAlpha <= 0)
                    currAlpha = 0;

                break;
            case 1:
                currAlpha += _fadeTime * Time.deltaTime;
                if (currAlpha >= 1)
                    currAlpha = 1;

                break;
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
    /// Hace que empiece el fade in
    /// </summary>
    /// <param name="scene"></param>
    /// <param name="mode"></param>
    private void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        fadeIn = true;
    }
}
