using System;
using Paradise.Data.Unit;
using UnityEngine;

namespace Paradise.Battle
{
    public class MapTile : MonoBehaviour
    {
        private Color _originColor;
        private SpriteRenderer _spriteRenderer;
        
        private PlayerUnit _playerUnit;

        private void Start()
        {
            _spriteRenderer = gameObject.FetchComponent<SpriteRenderer>();
            _originColor = _spriteRenderer.color;
        }

        public void Selected()
        {
            _spriteRenderer.color = Color.red;
        }

        public void Released()
        {
            _spriteRenderer.color = _originColor;
        }
        
        public void PreviewUnit(UnitData data)
        {
            _playerUnit = data.CreateInstance();
            _playerUnit.Show(data, this);
        }
        
        public void CreateUnit()
        {
            _playerUnit.Create();
            CurrentState = TileState.Occupied;
        }

        public void DestroyUnit()
        {
            // 가끔 여기 들어오는부분 고치기
            if (_playerUnit is null)
            {
                Debug.Log("No unit on the tile"); 
                return;
            }
            
            _playerUnit.Destroy();
            _playerUnit = null;
            CurrentState = TileState.Empty;
        }

        public TileState CurrentState { get; private set; }
    }
}