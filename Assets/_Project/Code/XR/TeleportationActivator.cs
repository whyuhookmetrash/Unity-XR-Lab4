using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

namespace XR
{
    public sealed class TeleportationActivator : MonoBehaviour
    {
        [SerializeField] private XRRayInteractor _interactor;
        [SerializeField] private InputActionProperty _teleportAction;

        private void Start()
        {
            _interactor.gameObject.SetActive(false);
            _teleportAction.action.performed += OnTeleportAction;
        }

        private void OnTeleportAction(InputAction.CallbackContext _)
        {
            _interactor.gameObject.SetActive(true);
        }

        private void Update()
        {
            if (_teleportAction.action.WasReleasedThisFrame())
                _interactor.gameObject.SetActive(false);
        }
    }
}