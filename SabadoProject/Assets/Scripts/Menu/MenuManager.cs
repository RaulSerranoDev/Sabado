﻿using UnityEngine;
using UnityEngine.UI;

namespace Sabado
{
    /// <summary>
    /// Controlador de la escena de menu
    /// </summary>
    public class MenuManager : MonoBehaviour
    {
        [Header("Text References")]
        [SerializeField] private Text titleText = null;

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
            Initiate.Fade("Street", fadeColor, fadeTime);
        }
    }
}