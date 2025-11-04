using UnityEngine;
using UnityEngine.InputSystem;

namespace XR
{
    public sealed class HandInput : MonoBehaviour
    {
        [SerializeField] private InputActionProperty _triggerValue;
        [SerializeField] private InputActionProperty _gripValue;
        [SerializeField] private Animator _animator;
        [SerializeField] GameObject _handBody;

        private void Update()
        {
            float trigger = _triggerValue.action.ReadValue<float>();
            float grip = _gripValue.action.ReadValue<float>();

            if (trigger >= 0.8f)
                _handBody.SetActive(true);
            else
                _handBody.SetActive(false);

            _animator.SetFloat("Trigger", grip);
            _animator.SetFloat("Grip", trigger);
        }
    }
}