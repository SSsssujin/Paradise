using System;
using UnityEngine;

namespace Paradise.Battle
{
    public class MapTile : MonoBehaviour
    {
        private bool _isSelected;

        private Color _originColor;
        private SpriteRenderer _spriteRenderer;

        private void Start()
        {
            _spriteRenderer = gameObject.FetchComponent<SpriteRenderer>();
            _originColor = _spriteRenderer.color;
        }

        public void Selected()
        {
            // 수정
            if (_isSelected)
            {
                Released();
                return;
            }
            
            _isSelected = true;
            TileSelected?.Invoke();
            _spriteRenderer.color = Color.red;
        }

        public void Released()
        {
            _isSelected = false;
            _spriteRenderer.color = _originColor;
        }
        
        public event TouchHandler TileSelected;
    }
}