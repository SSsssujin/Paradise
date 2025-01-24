using UnityEngine;
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
            _playerInput.onActionTriggered += inputActionTriggered;
        }
            
        private void inputActionTriggered(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                Vector2 touchPosition = Utils.GetTouchPosition();
                Vector2 touchedScreenPoint = _camera.ScreenToWorldPoint(touchPosition);

                RaycastHit2D hit = Physics2D.Raycast(touchedScreenPoint,
                    _camera.transform.forward,
                    Mathf.Infinity,
                    layerMask: _targetLayers);

                // 수정?
                if (hit.collider != null)
                {
                    if (hit.collider.TryGetComponent<Battle.MapTile>(out var tile))
                    {
                        tile.Selected();
                    }
                }
            }
        }

        private void OnDestroy()
        {
            _playerInput.onActionTriggered -= inputActionTriggered;
        }
    }
}