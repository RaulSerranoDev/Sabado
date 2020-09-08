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

        public bool Move { get; set; }

        /// <summary>
        /// Obtiene referencias
        /// </summary>
        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        /// <summary>
        /// Inicializa variables
        /// </summary>
        private void Start()
        {
            Move = true;
        }

        private void Update()
        {
            // Handle screen touches.
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                    Move = !Move;
            }
        }
        /// <summary>
        /// Es llamado con un intervalo fijo independiente del frame rate.
        /// Utilizado para la física.
        /// </summary>
        private void FixedUpdate()
        {
            //Mueve el objeto hacía arriba en función de la velocidad.
            if (Move)
                rb.MovePosition(rb.position + Vector2.up * speed * Time.fixedDeltaTime);
        }
    }
}