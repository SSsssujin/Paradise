using UnityEngine;

namespace Paradise
{
    public class GameManager
    {
        private Party[] _party;
        
        public void Initialize()
        {
            _party = new Party[MaxCount.Party];
        }

    }
}