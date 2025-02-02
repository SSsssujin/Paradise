using System;
using Paradise.Battle;
using UnityEngine;

namespace Paradise.Data.Unit
{
    public abstract class UnitData : ScriptableObject
    {
        [Header("Info")] 
        [SerializeField] private string _name;
        [SerializeField] private string _description;
        [SerializeField] private int _cost;
        [SerializeField] private string _key;

        [Space] 
        
        [Header("Stat")] 
        [SerializeField] private float _hp;
        [SerializeField] private float _attackPower;
        [SerializeField] [Range(1, 5)] private int _attackRange;
        [SerializeField] [Range(1, 5)] private int _attackSpeed;
        [SerializeField] private Element _element;
        [SerializeField] private StarRank _starRank;

        [Space] 
        
        [Header("Model")] 
        [SerializeField] private Sprite _portrait;
        [SerializeField] private GameObject _model;

        [Space] 
        
        [Header("Skill")] 
        [SerializeField] private Sprite _passiveSkillIcon;
        [SerializeField] [TextArea(3, 10)] private string _passiveSkillDescription;

        private void OnEnable()
        {
            _key = name;
        }

        public PlayerUnit CreateInstance(MapTile targetTile)
        {
            string key = $"{UnitType}/{_key}.prefab";
            var unit = GameManager.Resource.Instantiate(key).FetchComponent<PlayerUnit>();
            unit.Create(this, targetTile);
            return unit;
        }
        
        public UnitType UnitType { get; protected set; }
        
        public string Name => _name;
        public string Description => _description;
        public string Key => _key;

        // Stat
        public float Hp => _hp;
        public float AttackPower => _attackPower;
        public int AttackRange => _attackRange;
        public int AttackSpeed => _attackSpeed;
        public Element Element => _element;
        public StarRank StarRank => _starRank;
        
        // Object
        public int Cost => _cost;
        public Sprite Portrait => _portrait;
        public GameObject Model => _model;

        // Skill
        public Sprite PassiveSkillIcon => _passiveSkillIcon;
        public string PassiveSkillDescription => _passiveSkillDescription;
    }
}