using System.Collections.Generic;
using Paradise.Data.Unit;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;
using Toggle = UnityEngine.UI.Toggle;

namespace Paradise.UI
{
    public partial class UI_UnitPanel : UI_Base
    {
        private ScrollRect _unitList;
        private ScrollRect _statInfo;
        
        private UnitData _selectedUnitData;
        
        private List<Toggle> _unitToggles = new();
        
        protected override bool _Initialize()
        {
            if (!base._Initialize())
                return false;
           
            // Bind
            BindObject(typeof(GameObjects));
            BindText(typeof(Texts));
            BindButton(typeof(Buttons));
            BindToggle(typeof(Toggles));
            
            BindEvent(GetObject((int)GameObjects.BasicUnit), () => SwitchUnitContent(0));
            BindEvent(GetObject((int)GameObjects.EliteUnit), () => SwitchUnitContent(1));
            BindEvent(GetObject((int)GameObjects.CancelButton), () => GameManager.UI.ClosePopup());

            // Caching
            _unitList = GetObject((int)GameObjects.UnitList).FetchComponent<ScrollRect>();
            _statInfo = GetObject((int)GameObjects.StatInfo).FetchComponent<ScrollRect>();
            
            RefreshUnitList();
            
            return true;
        }

        protected override void OnShow()
        {
            GetToggle((int)Toggles.BasicUnit).isOn = true;
            _unitToggles.ForEach(x => x.isOn = false);
            _unitToggles[0].isOn = true;
            SwitchUnitContent(0);
        }
        
        protected override void OnHide()
        {

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

        private void SwitchUnitContent(int unitIndex)
        {
            int childCount = _unitList.viewport.childCount;
            for (int i = 0; i < childCount; i++)
            {
                bool isMyIndex = unitIndex == i;
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

                _GetText(GetObject((int)GameObjects.Hp)).text = $"{data.Hp}";
                _GetText(GetObject((int)GameObjects.Attack)).text = $"{data.AttackPower}";
                _GetText(GetObject((int)GameObjects.AttackRange)).text = $"{data.AttackRange}";
                _GetText(GetObject((int)GameObjects.Speed)).text = $"{data.AttackSpeed}";
                _GetText(GetObject((int)GameObjects.Cost)).text = $"{data.Price}";

                var activeSkill = GetObject((int)GameObjects.Skill).transform.Find("Active").gameObject;
                activeSkill.SetActive(false);
                
                GetObject((int)GameObjects.Skill).transform
                    .Find("Passive")
                    .Find("Icon")
                    .FetchComponent<Image>()
                    .sprite = data.PassiveSkillIcon;
                GetText((int)Texts.SkillDescription).text = data.PassiveSkillDescription;
                
                if (data is EliteUnitData)
                {
                    activeSkill.SetActive(true);
                    EliteUnitData eliteUnitData = data as EliteUnitData;
                    activeSkill.transform
                        .Find("Icon")
                        .FetchComponent<Image>()
                        .sprite = eliteUnitData.ActiveSkillIcon;
                }

                
            }

            TextMeshProUGUI _GetText(GameObject origin)
            {
                return origin.transform.Find("Value").FetchComponent<TextMeshProUGUI>();
            }
        }

    }

    public partial class UI_UnitPanel
    {
        enum GameObjects
        {
            // ScrollRects
            UnitList,
            StatInfo,
            
            // Toggles
            BasicUnit,
            EliteUnit,
            
            // Stats
            Hp,
            Attack,
            AttackRange,
            Speed,
            Cost,
            Skill,
            SkillDescription,
            
            // Buttons
            CancelButton,
        }

        enum Texts
        {
            SkillDescription,
        }

        enum Buttons
        {
            
        }

        enum Toggles
        {
            BasicUnit,
            EliteUnit,
        }
    }
}