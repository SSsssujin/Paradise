using System.Collections.Generic;
using Paradise.Data.Unit;
using UnityEngine;
using UnityEngine.UI;
using Toggle = UnityEngine.UI.Toggle;

namespace Paradise.UI
{
    public partial class UI_UnitPanel : UI_Base
    {
        private ScrollRect _unitList;
        private UnitCollectionView _unitCollectionView;
        
        private List<Toggle> _unitToggles = new();
        
        protected override bool Initialize()
        {
            if (!base.Initialize())
                return false;
           
            BindObject(typeof(GameObjects));
            BindText(typeof(Texts));
            BindScrollRect(typeof(ScrollRects));
            BindToggle(typeof(Toggles));
            BindImage(typeof(Images));
            
            BindEvent(GetObject((int)GameObjects.CancelButton), () => Manager.UI.ClosePopup());

            ActivateUnitCollectionView(out _unitCollectionView);
            _unitCollectionView.SetToggleListeners(PrintUnitInfo);
            
            return true;
        }

        protected override void OnShow()
        {
            GetToggle((int)Toggles.UnitListBasicToggle).isOn = true;
            ResetScrollRect();
        }
        
        protected override void OnHide()
        {

        }

        private void ResetScrollRect()
        {
            GetScrollRect((int)ScrollRects.ActiveSkillScrollRect).verticalNormalizedPosition = 1f;
            GetScrollRect((int)ScrollRects.PassiveSkillScrollRect).verticalNormalizedPosition = 1f;
        }

        private void PrintUnitInfo(bool isOn, UnitData data)
        {
            if (isOn)
            {
                GetText((int)Texts.StatHpText).text = $"{data.Hp}";
                GetText((int)Texts.StatAttackText).text = $"{data.AttackPower}";
                GetText((int)Texts.StatAttackRangeText).text = $"{data.AttackRange}";
                GetText((int)Texts.StatSpeedText).text = $"{data.AttackSpeed}";
                GetText((int)Texts.StatCostText).text= $"{data.Cost}";
                GetText((int)Texts.PassiveSkillDescriptionText).text = data.PassiveSkillDescription;
                GetImage((int)Images.PassiveSkillImage).sprite = data.PassiveSkillIcon;
                
                var activeSkill = GetObject((int)GameObjects.ActiveSkillObject);
                activeSkill.SetActive(false);
                
                if (data is EliteUnitData eliteUnitData)
                {
                    activeSkill.SetActive(true);
                    GetImage((int)Images.ActiveSkillImage).sprite = eliteUnitData.ActiveSkillIcon;
                    GetText((int)Texts.ActiveSkillDescriptionText).text  = eliteUnitData.ActiveSkillDescription;
                }
                ResetScrollRect();
            }
        }
    }

    public partial class UI_UnitPanel
    {
        enum GameObjects
        {
            UnitListBasicToggle,
            UnitListEliteToggle,
            CancelButton,
            ActiveSkillObject,
        }

        enum Images
        {
            PassiveSkillImage,
            ActiveSkillImage,
        }

        enum Texts
        {
            // Stats
            StatHpText,
            StatAttackText,
            StatAttackRangeText,
            StatSpeedText,
            StatCostText,
            // Skill descriptions
            PassiveSkillDescriptionText,
            ActiveSkillDescriptionText,
        }

        enum ScrollRects
        {
            PassiveSkillScrollRect,
            ActiveSkillScrollRect,
            UnitListScrollRect,
        }

        enum Toggles
        {
            UnitListBasicToggle,
            UnitListEliteToggle,
        }
    }
}