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
    }
}