﻿using UnityEngine;

namespace Sabado
{
    /// <summary>
    /// Singleton persistente entre escenas
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
                Destroy(this);
        }


        /// <summary>
        /// Nombre del juego
        /// </summary>
        public string GameName;

    }
}