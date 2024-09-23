using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Unity.ShaderGlobals
{
    [RequireComponent(typeof(Collider))]
    public class ColliderMouseEventsComponent : MonoBehaviour
    {
        [SerializeField] UnityEvent onMouseUpAsButton;
        [SerializeField] UnityEvent onMouseEnter;
        [SerializeField] UnityEvent<bool> onMouseHover;
        [SerializeField] UnityEvent onMouseExit;

        private void OnMouseUpAsButton()
        {
            onMouseUpAsButton?.Invoke();
        }

        private void OnMouseEnter()
        {
            onMouseEnter?.Invoke();
            onMouseHover?.Invoke(true);
        }

        private void OnMouseExit()
        {
            onMouseExit?.Invoke();
            onMouseHover?.Invoke(false);
        }
    }
}