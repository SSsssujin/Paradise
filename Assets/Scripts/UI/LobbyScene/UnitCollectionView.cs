using System;
using System.Collections.Generic;
using Paradise.Data.Unit;
using UnityEngine;
using UnityEngine.UI;

namespace Paradise.UI
{
    public class UnitCollectionView : MonoBehaviour
    {
        private ScrollRect _unitListScrollRect;

        private Toggle[] _gradeSelectionToggles;
        private List<UnitToggleHandler> _unitToggles = new();
        
        public void Initialize()
        {
            _unitListScrollRect = gameObject.FetchComponent<ScrollRect>();
            _gradeSelectionToggles = new[]
            {
                transform.GetChild(0).GetChild(0).FetchComponent<Toggle>(),
                transform.GetChild(0).GetChild(1).FetchComponent<Toggle>(),
            };
            
            RefreshUnitList();
            _gradeSelectionToggles[0].onValueChanged.AddListener(x => SwitchUnitContent(UnitType.Basic));
            _gradeSelectionToggles[1].onValueChanged.AddListener(x => SwitchUnitContent(UnitType.Elite));

            SwitchUnitContent(UnitType.Basic);
        }

        public void RefreshUnitList()
        {
            foreach (var unit in Manager.Instance.UnitList)
            {
                int index = unit is BasicUnitData ? 0 : 1;
                Transform parent = _unitListScrollRect.viewport.GetChild(index);
                var toggle = Manager.Resource.Instantiate("UnitCard").FetchComponent<Toggle>();
                toggle.transform.SetParent(parent);
                toggle.transform.ResetLocal();
                toggle.group = _unitListScrollRect.viewport.FetchComponent<ToggleGroup>();
                toggle.transform.Find("Portrait").FetchComponent<Image>().sprite = unit.Portrait;

                UnitToggleHandler toggleHandler = new(toggle, unit);
                _unitToggles.Add(toggleHandler);
            }
        }
        
        public void SwitchUnitContent(UnitType unitIndex)
        {
            // Toggle 0 : Basic, 1 : Elite
            int childCount = _unitListScrollRect.viewport.childCount;
            for (int i = 0; i < childCount; i++)
            {
                bool isMyIndex = (int)unitIndex == i;
                var unitContent = _unitListScrollRect.viewport.GetChild(i);
                var canvasGroup = unitContent.FetchComponent<CanvasGroup>();
                canvasGroup.alpha = System.Convert.ToInt32(isMyIndex);
                canvasGroup.interactable = isMyIndex;
                canvasGroup.blocksRaycasts = isMyIndex;
            }
        }

        public void SetToggleListeners(Action<bool, UnitData> onToggleValueChanged)
        {
            foreach (var toggleHandler in _unitToggles)
            {
                toggleHandler.SetDelegate(onToggleValueChanged);
            }
        }

        public IReadOnlyList<UnitToggleHandler> GetToggles()
        {
            return _unitToggles;
        }
    }

    public class UnitToggleHandler
    {
        public Toggle Toggle { get; private set; }
        public UnitData UnitData { get; private set; }
        public Action<bool, UnitData> OnToggleChanged;

        public UnitToggleHandler(Toggle toggle, UnitData unitData)
        {
            Toggle = toggle;
            UnitData = unitData;
            Toggle.onValueChanged.AddListener(InvokeDelegate);
        }

        public void SetDelegate(Action<bool, UnitData> action)
        {
            OnToggleChanged = action;
        }

        private void InvokeDelegate(bool isOn)
        {
            OnToggleChanged?.Invoke(isOn, UnitData);
        }
    }
}