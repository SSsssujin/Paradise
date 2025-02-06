using Paradise.Data.Unit;
using Paradise.UI;
using Unity.VisualScripting;
using UnityEngine;

namespace Paradise.UI
{
    public class UI_Party : UI_Base
    {
        enum GameObjects
        {
            UnitListScrollRect,
            CancelButton
        }

        private UnitCollectionView _unitCollectionView;
        
        protected override bool Initialize()
        {
            if (!base.Initialize())
                return false;
 
            BindObject(typeof(GameObjects));

            ActivateUnitCollectionView(out _unitCollectionView);
            _unitCollectionView.SetToggleListeners(ShowUnitPortrait);
            
            BindEvent(GetObject((int)GameObjects.CancelButton), () => Manager.UI.ClosePopup());
            
            return true;
        }

        private void ShowUnitPortrait(bool isOn, UnitData data)
        {
            if (isOn)
            {
                
            }
        }
    }
}