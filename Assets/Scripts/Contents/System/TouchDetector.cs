using UnityEngine;
using UnityEngine.InputSystem;

namespace Paradise
{
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
            _playerInput.onActionTriggered += onActionTriggered;
        }
            
        private void onActionTriggered(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                Vector2 touchPosition = Utils.GetTouchPosition();
                Vector3 touchedScreenPoint = _camera.ScreenToWorldPoint(touchPosition);
                touchedScreenPoint.z = 0;

                RaycastHit2D hit = Physics2D.Raycast(touchedScreenPoint, 
                    _camera.transform.forward, 
                    Mathf.Infinity,
                    layerMask:_targetLayers);

                if (hit.collider != null)
                {
                    if (hit.collider.TryGetComponent<MapTile>(out var tile))
                    {
                        tile.Selected();
                    }
                }
            }
        }

        private void OnDestroy()
        {
            _playerInput.onActionTriggered -= onActionTriggered;
        }
    }
}