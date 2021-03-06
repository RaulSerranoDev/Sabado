﻿using UnityEngine;
using RaulSerranoDev;

namespace Sabado
{
    /// <summary>
    /// Controlador de la escena principal
    /// Singleton
    /// </summary>
    public class StreetManager : MonoBehaviour
    {
        public static StreetManager Instance { get; private set; }

        [Header("Game Settings")]
        [SerializeField] private int initialTime = 0;     //Hora inicial en minutos

        [Header("Fade Settings")]
        [SerializeField] private Color fadeColor = Color.white;
        [SerializeField] private float fadeTime = 0f;

        public delegate void IntDelegate(int value);

        /// <summary>
        /// Hora en el juego (en minutos). 
        /// Cuando es modificado se transforma a una hora valida.
        /// Cuando es modificado informa a los suscritos a OnTimeChanged.
        /// </summary>
        public int Time
        {
            get { return time; }
            set
            {
                time = value;
                time %= 60 * 24;                    //Ajuste a hora válida
                OnTimeChanged?.Invoke(time);
            }
        }
        public IntDelegate OnTimeChanged;
        private int time;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(this);
        }

        /// <summary>
        /// Inicializa variables
        /// </summary>
        private void Start()
        {
            Time = initialTime;
        }

        /// <summary>
        /// Es llamado cuando la pareja llega al final de la calle
        /// Pasa a la siguiente escena
        /// </summary>
        public void CoupleArrived()
        {
            SceneTransition.Fade("Room", fadeTime, fadeColor);
        }

        /// <summary>
        /// Hace que el tiempo avance "minutes"
        /// </summary>
        /// <param name="minutes"></param>
        public void SpendTime(int minutes)
        {
            Time += minutes;
        }
    }
}