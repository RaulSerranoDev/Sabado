using UnityEngine;

namespace Sabado
{
    /// <summary>
    /// Detecta cuando la pareja llega al final e informa al StreetManager
    /// </summary>
    public class End : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            Couple couple = collision.GetComponent<Couple>();
            if (couple != null)
                StreetManager.Instance.CoupleArrived();
            
        }
    }
}