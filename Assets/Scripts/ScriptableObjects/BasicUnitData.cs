using System;
using Paradise;
using UnityEngine;

namespace Paradise.Data.Unit
{
    [CreateAssetMenu(fileName = "Basic", menuName = "ScriptableObjects/Unit/Player/Basic", order = 1)]
    public class BasicUnitData : UnitData
    {
        private void Awake()
        {
            UnitType = UnitType.Basic;
        }
    }
}