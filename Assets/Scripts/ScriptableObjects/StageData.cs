using System;
using System.Collections.Generic;
using Paradise;
using Unity.Collections;
using UnityEngine;

namespace Paradise.Data.Stage
{
    [CreateAssetMenu(fileName = "Stage", menuName = "ScriptableObjects/Stage", order = 1)]
    public class StageData : ScriptableObject
    {
        private void OnValidate()
        {
            _stage = name;
        }

        [SerializeField, ReadOnly] private string _stage;
        [SerializeField, TextArea(3, 10)] private string _description;
        [SerializeField] private List<EnemyInfo> _enemyList;

        public string Stage => _stage;
        public string Description => _description;
        public IReadOnlyList<EnemyInfo> EnemyList => _enemyList;
    }

    [Serializable]
    public class EnemyInfo
    {
        [Header("Enemy Stat")] 
        public GameObject Model;

        public float Hp = 30;
        public float Speed = 2;
        public float Defence = 0;
        public float AttackPower = 10;
        public int Silver = 10;
        public Element Element;

        [Space] [Header("Wave Info")] public int Count = 10;
        public float SpawnDelay = 2;
    }
}