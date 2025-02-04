using Paradise.Battle;
using Paradise.Data.Unit;
using UnityEngine;

namespace Paradise
{
    public abstract class PlayerUnit : MonoBehaviour
    {
        [SerializeField] protected Vector2 _scale = Vector2.one * 0.3f;        
        [SerializeField] protected Vector2 _positionOffset = new (0, -0.5f);

        private UnitData _unitData;
        private GameObject _attackRangerViewer;
        
        public void Show(UnitData data, MapTile tile)
        {
            gameObject.SetActive(false);
            transform.SetParent(tile.transform);
            transform.ResetLocal();

            Vector3 offset = new(_positionOffset.x, _positionOffset.y, -1);
            transform.position += offset;
            transform.localScale = _scale;

            _unitData = data;
            _attackRangerViewer ??= Manager.Resource.Instantiate("AttackRangeViewer", tile.transform);
            _attackRangerViewer.SetActive(true);
            _attackRangerViewer.transform.ResetLocal2D();
            _attackRangerViewer.transform.localScale *= _unitData.AttackRange + 2; // 수정
            
            gameObject.SetActive(true);
        }

        public void Create()
        {
            _attackRangerViewer.SetActive(false);
           
            // Initialize
            
        }

        public void Destroy()
        {
            _attackRangerViewer.SetActive(false);
            Manager.Resource.Destroy(gameObject);
        }
    }
}