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
        enum GameObjects
        {
            UnitDeployCancelPanel,
            GradeSelectButtonList,
            BasicUnitSelectButtons,
            EliteUnitSelectButtons,
            
            EliteUnitButton,
            BasicUnitButton,
        }

        private TouchDetector _touchDetector;
        private MapTile _selectedTile;

        protected override bool _Initialize()
        {
            if (!base._Initialize())
            {
                return false;
            }
            
            BindObject(typeof(GameObjects));

            BindEvent(GetObject((int)GameObjects.UnitDeployCancelPanel), DeselectTile);
            BindEvent(GetObject((int)GameObjects.BasicUnitButton), () => ActivateUnitList(UnitType.Basic));
            BindEvent(GetObject((int)GameObjects.EliteUnitButton), () => ActivateUnitList(UnitType.Elite));

            _touchDetector = FindAnyObjectByType<TouchDetector>();
            _touchDetector.MapTileTouched += OnTileSelected;
            
            InitializeUnitSelectButtons();
            DeactivateAll();
            
            return true;
        }

        // 수정
        private void InitializeUnitSelectButtons()
        {
            // 1. BasicUnitButtons
            if (GameManager.Instance.GetCurrentPartyList(UnitType.Basic) is not IReadOnlyList<UnitData> party) return;
            var basicUnitButtonList = GetObject((int)GameObjects.BasicUnitSelectButtons).transform;
            int index = 0;
            foreach (var unit in party)
            {
                basicUnitButtonList.GetChild(index).Find("Image").FetchComponent<Image>().sprite = unit.Portrait;
                basicUnitButtonList.GetChild(index).FetchComponent<Button>().onClick
                    .AddListener(() => CreateUnit(unit));
                index++;
            }

            // 2. EliteUnitButtons
            party = (IReadOnlyList<UnitData>)GameManager.Instance.GetCurrentPartyList(UnitType.Elite);
            var eliteUnitButtonList = GetObject((int)GameObjects.EliteUnitSelectButtons).transform;
            index = 0;
            foreach (var unit in party)
            {
                eliteUnitButtonList.GetChild(index).Find("Image").FetchComponent<Image>().sprite = unit.Portrait;
                eliteUnitButtonList.GetChild(index).FetchComponent<Button>().onClick
                    .AddListener(() => CreateUnit(unit));
                index++;
            }
        }

        private void OnTileSelected(MapTile tile)
        {
            gameObject.SetActive(true);
            GetObject((int)GameObjects.UnitDeployCancelPanel).SetActive(true);
            
            var currentTileState = tile.State;
            _selectedTile = tile;

            if (currentTileState == TileState.Empty)
            {
                _selectedTile.SetState(TileState.Touched);
                GetObject((int)GameObjects.GradeSelectButtonList).SetActive(true);
            }
            else
            {
                
            }
        }

        private void DeselectTile()
        {
            _selectedTile.SetState(TileState.Empty);
            _selectedTile = null;
            DeactivateAll();
            gameObject.SetActive(false);
        }

        private void ActivateUnitList(UnitType unitType)
        {
            GetObject((int)GameObjects.GradeSelectButtonList).SetActive(false);
            GameObjects list = unitType switch
            {
                UnitType.Basic => GameObjects.BasicUnitSelectButtons,
                UnitType.Elite => GameObjects.EliteUnitSelectButtons,
                _ => throw new ArgumentOutOfRangeException(nameof(unitType), unitType, null)
            };
            GetObject((int)list).SetActive(true);
        }

        private void CreateUnit(UnitData data)
        {
            _selectedTile.CreateUnit(data);
            DeselectTile();
            DeactivateAll();   
        }

        private void DeactivateAll()
        {
            GetObject((int)GameObjects.UnitDeployCancelPanel).SetActive(false);
            GetObject((int)GameObjects.GradeSelectButtonList).SetActive(false);
            GetObject((int)GameObjects.BasicUnitSelectButtons).SetActive(false);
            GetObject((int)GameObjects.EliteUnitSelectButtons).SetActive(false);
        }
    }
}