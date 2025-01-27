using System;
using System.Collections.Generic;
using Paradise.Data.Unit;
using Paradise.UI;
using UnityEngine;

namespace Paradise
{
    public class GameManager : Singleton<GameManager>
    {
        private readonly ResourceManager _resource = new();
        private readonly UIManager _ui = new();

        public List<UnitData> UnitList = new();

        private async void Start()
        {
            //Resource.LoadScene("BattleScene");
            
            await UI.Initialize();
        }

        private void OnDestroy()
        {
            UI.Destroy();
        }

        public static ResourceManager Resource => Instance._resource;
        public static UIManager UI => Instance._ui;
    }
}
