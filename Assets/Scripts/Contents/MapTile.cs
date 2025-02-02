using System;
using Paradise.Data.Unit;
using UnityEngine;

namespace Paradise.Battle
{
    public class MapTile : MonoBehaviour
    {
        private bool _isSelected;

        private Color _originColor;
        private SpriteRenderer _spriteRenderer;
        private PlayerUnit _deployedPlayerUnit;

        private void Start()
        {
            _spriteRenderer = gameObject.FetchComponent<SpriteRenderer>();
            _originColor = _spriteRenderer.color;
        }

        public void SetState(TileState state)
        {
            State = state;
            SetState();
        }

        private void SetState()
        {
            switch (State)
            {
                case TileState.Empty:
                    Released();
                    break;
                case TileState.Touched:
                    Selected();
                    break;
                case TileState.Occupied:
                    break;
                default:
                    Debug.LogError("Undefined state : " + State);
                    break;
            }
        }

        public void CreateUnit(UnitData data)
        {
            _deployedPlayerUnit = data.CreateInstance(this);
        }

        private void Selected()
        {
            _isSelected = true;
            _spriteRenderer.color = Color.red;
            State = TileState.Occupied;
        }

        private void Released()
        {
            _isSelected = false;
            _spriteRenderer.color = _originColor;
            State = TileState.Empty;
        }

        public TileState State { get; private set; }
    }
}