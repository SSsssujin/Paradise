using System.Collections.Generic;
using Paradise.Data.Unit;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;
using Toggle = UnityEngine.UI.Toggle;

namespace Paradise.UI
{
    public partial class UI_UnitPanel : UI_Base
    {
        private ScrollRect _unitList;
        private UnitData _selectedUnitData;
        
        private List<Toggle> _unitToggles = new();
        
        protected override bool _Initialize()
        {
            if (!base._Initialize())
                return false;
           
            BindObject(typeof(GameObjects));
            BindText(typeof(Texts));
            BindScrollRect(typeof(ScrollRects));
            BindToggle(typeof(Toggles));
            BindImage(typeof(Images));
            
            BindEvent(GetObject((int)GameObjects.UnitListBasicToggle), () => SwitchUnitContent(UnitType.Basic));
            BindEvent(GetObject((int)GameObjects.UnitListEliteToggle), () => SwitchUnitContent(UnitType.Elite));
            BindEvent(GetObject((int)GameObjects.CancelButton), () => GameManager.UI.ClosePopup());

            _unitList = GetScrollRect((int)ScrollRects.UnitListScrollRect);
            
            RefreshUnitList();
            
            return true;
        }

        protected override void OnShow()
        {
            GetToggle((int)Toggles.UnitListBasicToggle).isOn = true;
            _unitToggles.ForEach(x => x.isOn = false);
            _unitToggles[0].isOn = true;
            SwitchUnitContent(0);
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

        private void RefreshUnitList()
        {
            foreach (var unit in GameManager.Instance.UnitList)
            {
                int index = unit is BasicUnitData ? 0 : 1;
                Transform parent = _unitList.viewport.GetChild(index);
                var toggle = GameManager.Resource.Instantiate("UnitCard").FetchComponent<Toggle>();
                toggle.transform.SetParent(parent);
                toggle.transform.ResetLocal();
                toggle.group = _unitList.viewport.FetchComponent<ToggleGroup>();
                toggle.transform.Find("Portrait").FetchComponent<Image>().sprite = unit.Portrait;
                toggle.onValueChanged.AddListener((x) => PrintUnitInfo(x, unit));
                _unitToggles.Add(toggle);
            }
            SwitchUnitContent(0);
        }

        private void SwitchUnitContent(UnitType unitIndex)
        {
            // Toggle 0 : Basic, 1 : Elite
            int childCount = _unitList.viewport.childCount;
            for (int i = 0; i < childCount; i++)
            {
                bool isMyIndex = (int)unitIndex == i;
                var unitContent = _unitList.viewport.GetChild(i);
                var canvasGroup = unitContent.FetchComponent<CanvasGroup>();
                canvasGroup.alpha = System.Convert.ToInt32(isMyIndex);
                canvasGroup.interactable = isMyIndex;
                canvasGroup.blocksRaycasts = isMyIndex;
            }
        }

        private void PrintUnitInfo(bool isOn, UnitData data)
        {
            if (isOn)
            {
                _selectedUnitData = data;

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