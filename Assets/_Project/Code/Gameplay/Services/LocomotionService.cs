using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public sealed class LocomotionService : MonoBehaviour
{
    [SerializeField] private GameObject _teleportationLocomotion;
    [SerializeField] private XRRayInteractor _teleportationInteractor;
    [SerializeField] private GameObject _continiousLocomotion;
    [SerializeField] private GameObject _continiousTurn;
    [SerializeField] private GameObject _snapTurn;

    public void EnableContinious()
    {
        _continiousLocomotion.SetActive(true);
    }

    public void DisableContinious()
    {
        _continiousLocomotion.SetActive(false);
    }

    public void EnableTeleportation()
    {
        _teleportationLocomotion.SetActive(true);
        _teleportationInteractor.enabled = true;
    }

    public void DisableTeleportation()
    {
        _teleportationLocomotion.SetActive(false);
        _teleportationInteractor.enabled = false;
    }

    public void EnableContiniousTurn()
    {
        _snapTurn.SetActive(false);
        _continiousTurn.SetActive(true);
    }

    public void DisableContiniousTurn()
    {
        _snapTurn.SetActive(true);
        _continiousTurn.SetActive(false);
    }
}