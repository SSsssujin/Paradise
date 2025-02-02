using System;
using Paradise.Battle;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Paradise
{
    public delegate void TouchHandler();

    [RequireComponent(typeof(PlayerInput))]
    public class TouchDetector : MonoBehaviour
    {
        [SerializeField] 
        private LayerMask _targetLayers;
        
        private PlayerInput _playerInput;
        private Camera _camera;

        private void Start()
        {
            // Caching
            _camera = Camera.main;
            _playerInput = gameObject.FetchComponent<PlayerInput>();

            // Listener
            _playerInput.onActionTriggered += InputActionTriggered;
        }
            
        private void InputActionTriggered(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                if (EventSystem.current.IsPointerOverGameObject()) return;
                
                Vector2 touchPosition = Utils.GetTouchPosition();
                Vector2 touchedScreenPoint = _camera.ScreenToWorldPoint(touchPosition);

                RaycastHit2D hit = Physics2D.Raycast(touchedScreenPoint,
                    _camera.transform.forward,
                    Mathf.Infinity,
                    layerMask: _targetLayers);

                if (hit.collider != null)
                {
                    if (hit.collider.TryGetComponent<MapTile>(out var tile))
                    {
                        MapTileTouched?.Invoke(tile);
                    }
                }
            }
        }

        private void OnDestroy()
        {
            _playerInput.onActionTriggered -= InputActionTriggered;
        }
        
        public event Action<MapTile> MapTileTouched;
    }
}