using System.Collections;
using UnityEngine;
using WanzyeeStudio;

namespace Sabado
{
    /// <summary>
    /// Singleton persistente entre escenas
    /// </summary>
    public class GameManager : BaseSingleton<GameManager>
    {
        /// <summary>
        /// Nombre del juego
        /// </summary>
        public string GameName;

        /// <summary>
        /// Hace Fade de todos los Audio Sources que haya en la escena.
        /// </summary>
        /// <param name="time">Tiempo en segundos que tarda en completarlo</param>
        /// <param name="up">'True' sube el volumen</param>
        public void AudioFade(float time, bool up)
        {
            //Obtiene todos los audios sources en la escena
            AudioSource[] audioSources = FindObjectsOfType<AudioSource>();
            int numAudios = audioSources.Length;

            //Inicializa array con el volumen inicial de todos los audios
            float[] initialVolume = new float[numAudios];
            for (int i = 0; i < numAudios; i++)
                initialVolume[i] = audioSources[i].volume;

            //Fade in
            if (up)
            {

            }



        }

        private IEnumerator AudioFadeIn(float time, AudioSource[] audioSources, float[] initialVolume)
        {
            int numAudios = audioSources.Length;

            //Todos los audios empiezan en volumen 0
            for (int i = 0; i < numAudios; i++)
                audioSources[i].volume = 0.0f;

            while(true)
            {

                yield return null;
            }
        }

        private float NewVolume(float time, float delta, bool up, float currentVolume)
        {
            //Fade in
            if (up)
            {
                currentVolume += time * delta;
                if (currentVolume >= 1f)
                    currentVolume = 1f;
            }

            //Fade out
            else
            {
                currentVolume -= time * delta;
                if (currentVolume <= 0f)
                    currentVolume = 0f;
            }
            return currentVolume;
        }
    }
}