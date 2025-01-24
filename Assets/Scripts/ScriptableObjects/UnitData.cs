using UnityEngine;

namespace Paradise.Data.Unit
{
    public abstract class UnitData : ScriptableObject
    {
        [Header("Info")] 
        [SerializeField] private string _name;
        [SerializeField] private string _description;
        [SerializeField] private int _price;

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
        [SerializeField] private string _skillKey;
        [SerializeField] private Sprite _passiveSkillIcon;
        [SerializeField] [TextArea(3, 10)] private string _passiveSkillDescription;

        private void OnEnable()
        {
            _skillKey = _name;
        }

        //public PlayerUnit CreateInstance(MapTileBase currentTileBase)
        //{
        //    Transform contentTr = FindObjectOfType<GamePlayContent>().transform;
        //    PlayerUnit unit = Instantiate(_model, contentTr).GetComponent<PlayerUnit>();
        //    unit.Create(this, currentTileBase);
        //    return unit;
        //}

        public UnitType UnitType { get; protected set; }
        
        public string Name => _name;
        public string Description => _description;

        // Stat
        public float Hp => _hp;
        public float AttackPower => _attackPower;
        public int AttackRange => _attackRange;
        public int AttackSpeed => _attackSpeed;
        public Element Element => _element;
        public StarRank StarRank => _starRank;
        
        // Object
        public int Price => _price;
        public Sprite Portrait => _portrait;
        public GameObject Model => _model;

        // Skill
        public string SkillKey => _skillKey;
        public Sprite PassiveSkillIcon => _passiveSkillIcon;
        public string PassiveSkillDescription => _passiveSkillDescription;
    }
}