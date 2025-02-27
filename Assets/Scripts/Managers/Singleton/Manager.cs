using System;
using System.Collections.Generic;
using Paradise.Data.Unit;
using Paradise.UI;
using UnityEngine;

namespace Paradise
{
    public class Manager : Singleton<Manager>
    {
        private readonly GameManager _game = new();
        private readonly ResourceManager _resource = new();
        private readonly UIManager _ui = new();

        // Tester
        public List<UnitData> UnitList = new();
        [SerializeField] private List<UnitData> _currentParty = new();

        private async void Start()
        {
            //Resource.LoadScene("BattleScene");
            Game.Initialize();
            await UI.Initialize();
        }

        private void OnDestroy()
        {
            UI.Destroy();
        }

        public IEnumerable<UnitData> GetCurrentPartyList(UnitType type)
        {
            var party = (type == UnitType.None) ? 
                _currentParty : _currentParty.FindAll(x => x.UnitType == type);
            return party;
        }

        public static GameManager Game => Instance._game;
        public static ResourceManager Resource => Instance._resource;
        public static UIManager UI => Instance._ui;
    }
}
