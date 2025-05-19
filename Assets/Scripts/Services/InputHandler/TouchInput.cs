using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using Zenject;

namespace Services.InputHandler
{
    public class TouchInput : ITouchInput, ITickable
    {
        public event Action<Vector3> OnScreenTouched;

        public void Tick()
        {
            if (Mouse.current?.leftButton.wasPressedThisFrame == true)
            {
                var screenPos = Mouse.current.position.ReadValue();
                if (!IsOverUI(screenPos))
                    HandleInput(screenPos);
            }

            if (Touchscreen.current?.primaryTouch.press.wasPressedThisFrame == true)
            {
                var screenPos = Touchscreen.current.primaryTouch.position.ReadValue();
                if (!IsOverUI(screenPos))
                    HandleInput(screenPos);
            }

        }

        private void HandleInput(Vector2 screenPos)
        {
            var ray = Camera.main.ScreenPointToRay(screenPos);
            Plane plane = new Plane(Vector3.forward, 0);
            if (plane.Raycast(ray, out float distance))
            {
                Vector3 worldPos = ray.GetPoint(distance);
                OnScreenTouched?.Invoke(worldPos);
            }
        }

        private bool IsOverUI(Vector2 screenPosition)
        {
            if (EventSystem.current == null)
                return false;

            PointerEventData pointerData = new PointerEventData(EventSystem.current)
            {
                position = screenPosition
            };

            var raycastResults = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerData, raycastResults);

            return raycastResults.Count > 0;
        }
    }
}