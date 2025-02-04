using Paradise.UI;
using UnityEngine;

namespace Paradise
{
    public class UI_MainPanel : UI_Base
    {
        enum GameObjects
        {
            Character,
        }
        
        enum Buttons
        {
            Character,
            Equipment,
            Enhancement,
            Party,
            Setting,
        }
        
        protected override bool _Initialize()
        {
            if (!base._Initialize())
                return false;

            // Caching
            BindObject(typeof(GameObjects));
            BindButton(typeof(Buttons));
            
            _AddListener();
            return true;
        }

        private void _AddListener()
        {
            var characterButton = GetObject((int)GameObjects.Character);
            BindEvent(characterButton, () => Manager.UI.ShowPopup<UI_UnitPanel>());
        }
    }
}