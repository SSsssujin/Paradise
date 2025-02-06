using System;
using System.Collections.Generic;
using Paradise.Battle;
using Paradise.Data.Unit;
using UnityEngine;
using UnityEngine.UI;

namespace Paradise.UI
{
    public class UI_UnitDeploy : UI_Base
    {
        private struct UnitButton
        {
            public Image Image;
            public Button Button;
            public GameObject ConfirmMarker;
        }
        
        enum GameObjects
        {
            UnitDeployCancelPanel,
            GradeSelectButtons,
            BasicUnitSelectButtons,
            EliteUnitSelectButtons,
            
            EliteUnitButton,
            BasicUnitButton,
        }

        private bool _isPreviewing;

        private MapTile _selectedTile;
        private TouchDetector _touchDetector;
        private UnitButton? _previousSelectedButton;
        
        private readonly Dictionary<UnitType, UnitButton[]> _unitButtons = new();
        
        protected override bool Initialize()
        {
            if (!base.Initialize())
            {
                return false;
            }
            
            BindObject(typeof(GameObjects));

            BindEvent(GetObject((int)GameObjects.UnitDeployCancelPanel), OnCancelPanelTouched);
            BindEvent(GetObject((int)GameObjects.BasicUnitButton), () => ActivateUnitSelectionUI(UnitType.Basic));
            BindEvent(GetObject((int)GameObjects.EliteUnitButton), () => ActivateUnitSelectionUI(UnitType.Elite));

            _touchDetector = FindAnyObjectByType<TouchDetector>();
            _touchDetector.MapTileTouched += OnTileSelected;
            
            InitializeUnitButtons(UnitType.Basic);
            InitializeUnitButtons(UnitType.Elite);
            
            DeactivateAll();
            return true;
        }

        private void InitializeUnitButtons(UnitType unitType)
        {
            if (!_unitButtons.ContainsKey(unitType))
            {
                _unitButtons.Add(unitType, new UnitButton[Utils.GetMaxCount(unitType)]);
            }

            Transform root = GetUnitSelectionUI(unitType).transform;

            for (int i = 0; i < root.childCount; i++)
            {
                var unitButtonRoot = root.GetChild(i);
                UnitButton unitButton = new UnitButton()
                {
                    Image = unitButtonRoot.Find("Image").FetchComponent<Image>(),
                    Button = unitButtonRoot.FetchComponent<Button>(),
                    ConfirmMarker = unitButtonRoot.Find("Confirm").gameObject
                };
                _unitButtons[unitType][i] = unitButton;
            }
            
            if (Manager.Instance.GetCurrentPartyList(unitType) is not IReadOnlyList<UnitData> party) return;
            
            for (int index = 0; index < party.Count; index++)
            {
                var unit = party[index];
                var unitButton = _unitButtons[unitType][index];
                unitButton.Image.sprite = unit.Portrait;
                unitButton.Button.onClick.AddListener(() =>
                {
                    OnUnitButtonTouched(unitButton, unit);
                });
            }
        }

        private void OnTileSelected(MapTile tile)
        {
            gameObject.SetActive(true);
            GetObject((int)GameObjects.UnitDeployCancelPanel).SetActive(true);
            
            var tileState = tile.CurrentState;
            _selectedTile = tile;
            _selectedTile.Selected();

            if (tileState == TileState.Empty)
            {
                GetObject((int)GameObjects.GradeSelectButtons).SetActive(true);
            }
            else if (tileState == TileState.Occupied)
            {
                
            }
        }
        
        private void OnCancelPanelTouched()
        {
            if (_isPreviewing)
            {
                _selectedTile.DestroyUnit();
            }
            DeselectTile();
            DeactivateAll();
            gameObject.SetActive(false);
            _isPreviewing = false;
            _previousSelectedButton = null;
        }

        private void OnUnitButtonTouched(UnitButton button, UnitData data)
        {
            // First touch
            if (!button.Equals(_previousSelectedButton))
            {
                // Delete previous info
                if (_previousSelectedButton.HasValue)
                {
                    _selectedTile.DestroyUnit();
                    _previousSelectedButton?.ConfirmMarker.SetActive(false);
                }
                button.ConfirmMarker.SetActive(true);
                _selectedTile.PreviewUnit(data);
                _isPreviewing = true;
                _previousSelectedButton = button;
            }
            // Second touch
            else
            {
                _selectedTile.CreateUnit();
                _isPreviewing = false;
                DeselectTile();
                DeactivateAll();
                _previousSelectedButton = null;
            }
        }

        private GameObject GetUnitSelectionUI(UnitType unitType)
        {
            GameObjects list = unitType switch
            {
                UnitType.Basic => GameObjects.BasicUnitSelectButtons,
                UnitType.Elite => GameObjects.EliteUnitSelectButtons,
                _ => throw new ArgumentOutOfRangeException(nameof(unitType), unitType, null)
            };
            return GetObject((int)list);
        }

        private void ActivateUnitSelectionUI(UnitType unitType)
        {
            GetObject((int)GameObjects.GradeSelectButtons).SetActive(false);
            GetUnitSelectionUI(unitType).SetActive(true);
        }

        
        private void DeselectTile()
        {
            _selectedTile.Released();
            _selectedTile = null;
        }
        
        private void DeactivateAll()
        {
            _previousSelectedButton?.ConfirmMarker.SetActive(false);
            GetObject((int)GameObjects.UnitDeployCancelPanel).SetActive(false);
            GetObject((int)GameObjects.GradeSelectButtons).SetActive(false);
            GetObject((int)GameObjects.BasicUnitSelectButtons).SetActive(false);
            GetObject((int)GameObjects.EliteUnitSelectButtons).SetActive(false);
            gameObject.SetActive(false);
        }
    }
}