using UnityEngine;

namespace Sabado
{
    /// <summary>
    /// Componente asociado a la pareja
    /// Controla el movimiento
    /// </summary>
    public class Couple : MonoBehaviour
    {
        [Header("Move Settings")]
        [SerializeField] private float speed = 0;

        //References
        private Rigidbody2D rb;

        /// <summary>
        /// Obtiene referencias
        /// </summary>
        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        /// <summary>
        /// Es llamado con un intervalo fijo independiente del frame rate.
        /// Utilizado para la física.
        /// </summary>
        private void FixedUpdate()
        {
            //Mueve el objeto hacía arriba en función de la velocidad.
            rb.MovePosition(rb.position + Vector2.up * speed * Time.fixedDeltaTime);
        }
    }
}