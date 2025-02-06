using Paradise.UI;
using UnityEngine;

namespace Paradise
{
    public class UI_MainPanel : UI_Base
    {
        enum GameObjects
        {
            PlayerUnit,
            Party,
            Recruit,
        }
        
        enum Buttons
        {
            Character,
            Equipment,
            Enhancement,
            Party,
            Setting,
        }
        
        protected override bool Initialize()
        {
            if (!base.Initialize())
                return false;

            // Caching
            BindObject(typeof(GameObjects));
            
            _AddListener();
            return true;
        }

        private void _AddListener()
        {
            GameObject button;
            button = GetObject((int)GameObjects.PlayerUnit);
            BindEvent(button, () => Manager.UI.ShowPopup<UI_UnitPanel>());
            
            button = GetObject((int)GameObjects.Party);
            BindEvent(button, () => Manager.UI.ShowPopup<UI_Party>());
            
        }
    }
}