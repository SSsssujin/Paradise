using UnityEngine;
using UnityEngine.InputSystem;

namespace Paradise
{
    public static class Utils
    {
        public static Vector2 GetTouchPosition()
        {
            return Pointer.current.position.ReadValue();
        }
    }
}