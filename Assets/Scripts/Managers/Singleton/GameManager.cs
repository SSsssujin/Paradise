using System;
using UnityEngine;

namespace Paradise
{
    public class GameManager : Singleton<GameManager>
    {
        private readonly ResourceManager _resource = new();

        private void Start()
        {
            //Resource.LoadScene("BattleScene");
        }

        public static ResourceManager Resource => Instance._resource;
    }
}
