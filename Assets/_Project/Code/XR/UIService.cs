using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public sealed class UIService : MonoBehaviour
{
    [SerializeField] private GameObject _settings;
    [SerializeField] private GameObject _canvas;
    [SerializeField] private AudioListener _audioListener;
    [SerializeField] private Slider _volumeSlider;

    private bool _isSettings = false;
    private bool _isShow = true;

    void Start()
    {
        _volumeSlider.onValueChanged.AddListener(UpdateVolume);
    }

    private void UpdateVolume(float volume)
    {
        AudioListener.volume = volume;
    }

    private void OnOpenSettings(InputAction.CallbackContext _)
    {
        _isSettings = !_isSettings;
        _settings.gameObject.SetActive(_isSettings);
    }

    public void OpenCloseSettings()
    {
        _isSettings = !_isSettings;
        _settings.gameObject.SetActive(_isSettings);
    }

    public void ShowHideAll()
    {
        _isShow = !_isShow;
        _canvas.gameObject.SetActive(_isShow);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}
