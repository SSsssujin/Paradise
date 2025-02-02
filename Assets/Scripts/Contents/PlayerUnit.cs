using Paradise.Battle;
using Paradise.Data.Unit;
using UnityEngine;

namespace Paradise
{
    public abstract class PlayerUnit : MonoBehaviour
    {
        protected Vector3 _scale;
        
        public void Create(UnitData data, MapTile tile)
        {
            transform.SetParent(tile.transform);
            transform.ResetLocal();
            transform.localScale = _scale;
        }
    }
}