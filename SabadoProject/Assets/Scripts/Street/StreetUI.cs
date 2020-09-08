using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sabado
{
    /// <summary>
    /// Controla la interfaz que muestra cuanto queda para llegar al final
    /// </summary>
    public class StreetUI : MonoBehaviour
    {
        [Header("Game References")]
        [SerializeField] private Couple couple = null;
        [SerializeField] private End end = null;

        [Header("UI References")]
        [SerializeField] private Image mapImage = null;
        [SerializeField] private TextMeshProUGUI timeText = null;

        /// <summary>
        /// Distancia al empezar la escena entre pareja y final
        /// </summary>
        private float initialDistance;

        /// <summary>
        /// Suscripción a delegates
        /// </summary>
        private void Awake()
        {
            StreetManager.Instance.OnTimeChanged += TimeChanged;
        }

        private void Start()
        {
            initialDistance = end.transform.position.y - couple.transform.position.y;
        }

        private void TimeChanged(int newTime)
        {
            Debug.Log("hola");
            int hours = newTime / 60;
            int minutes = newTime % 60;


            string minuteText = minutes.ToString();
            if (minutes < 10)
                minuteText = "0" + minuteText;

            string hourText = hours.ToString();
            if (hours < 10)
                minuteText = "0" + hourText;

            timeText.text = hourText + ":" + minuteText;
        }

        private void Update()
        {
            float remainingDistance = end.transform.position.y - couple.transform.position.y;
            mapImage.fillAmount = remainingDistance / initialDistance;
        }
    }
}