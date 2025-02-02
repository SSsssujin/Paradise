using UnityEngine;

namespace Paradise.Data.Unit
{
    [CreateAssetMenu(fileName = "Elite", menuName = "ScriptableObjects/Unit/Elite", order = 1)]
    public class EliteUnitData : UnitData
    {
        [SerializeField] private Sprite _activeSkillIcon;
        [SerializeField] [TextArea(3, 10)] private string _activeSkillDescription;
        
        public Sprite ActiveSkillIcon => _activeSkillIcon;
        public string ActiveSkillDescription => _activeSkillDescription;
        
        private void Awake()
        {
            UnitType = UnitType.Elite;
        }
    }
}