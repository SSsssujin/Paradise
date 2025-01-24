using UnityEngine;

namespace Paradise.Battle
{
    public class BattleManager : MonoBehaviour
    {
        
        
        public void SetGameSpeed(InGameSpeed speed)
        {
            float timeScale = speed switch
            {
                InGameSpeed.Normal => 1f,
                InGameSpeed.Double => 2f,
                InGameSpeed.Half => 0.5f,
                _ => 1f,
            };
            Time.timeScale = timeScale;
            GameSpeed = speed;
        }
        
        public InGameSpeed GameSpeed { get; private set; } = InGameSpeed.Normal;
    }
}