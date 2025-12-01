using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public sealed class UIService : MonoBehaviour
{
    [SerializeField] private GameObject _settings;
    [SerializeField] private GameObject _canvas;
    [SerializeField] private AudioListener _audioListener;
    [SerializeField] private Slider _volumeSlider;

    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _gameOverScoreText;
    [SerializeField] private TMP_Text _ammoText;
    [SerializeField] private TMP_Text _rockText;

    [SerializeField] private GameObject _turnMark;
    [SerializeField] private GameObject _continiousMark;
    [SerializeField] private GameObject _teleportationMark;
    private bool _isTurn = true;
    private bool _isContinious = true;
    private bool _isTeleportation = true;

    private bool _isSettings = false;
    private bool _isShow = true;

    private LocomotionService _locomotionService;
    private AmmoService _ammoService;
    private RockService _rockService;
    private ScoreService _scoreService;

    [SerializeField] private InputActionProperty _settingsAction;

    private void Awake()
    {
        _locomotionService = FindFirstObjectByType<LocomotionService>();
        _ammoService = FindFirstObjectByType<AmmoService>();
        _rockService = FindFirstObjectByType<RockService>();
        _scoreService = FindFirstObjectByType<ScoreService>();

        _settingsAction.action.performed += Action_performed;
        _ammoService.OnAmmoChanged += OnAmmoChanged;
        _rockService.OnRockChanged += OnRockChanged;
        _scoreService.OnScoreChanged += OnScoreChanged;

        _scoreText.text = _scoreService.CurrentScore.ToString("0000");
        _gameOverScoreText.text = "Your score: " + _scoreService.CurrentScore.ToString("0000");
        _rockText.text = _rockService.CurrentRock.ToString("00");
        _ammoText.text = _ammoService.CurrentAmmo.ToString("00");
    }
    
    private void OnScoreChanged(int score)
    {
        _scoreText.text = score.ToString("0000");
        _gameOverScoreText.text = "Your score: " + _scoreService.CurrentScore.ToString("0000");
    }

    private void OnRockChanged(int rock)
    {
        _rockText.text = rock.ToString("00");
    }

    private void OnAmmoChanged(int ammo)
    {
        _ammoText.text = ammo.ToString("00");
    }

    private void Action_performed(InputAction.CallbackContext _)
    {
        OpenCloseSettings();
    }

    void Start()
    {
        _volumeSlider.onValueChanged.AddListener(UpdateVolume);
    }

    private void UpdateVolume(float volume)
    {
        AudioListener.volume = volume;
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

    public void OnTurnClicked()
    {
        if (_isTurn == true)
        {
            _locomotionService.DisableContiniousTurn();
            _turnMark.SetActive(false);
            _isTurn = false;
        }
        else
        {
            _locomotionService.EnableContiniousTurn();
            _turnMark.SetActive(true);
            _isTurn = true;
        }
    }

    public void OnTeleportationClicked()
    {
        if (_isTeleportation == true)
        {
            _locomotionService.DisableTeleportation();
            _teleportationMark.SetActive(false);
            _isTeleportation = false;
        }
        else
        {
            _locomotionService.EnableTeleportation();
            _teleportationMark.SetActive(true);
            _isTeleportation = true;
        }
    }

    public void OnContiniousCliked()
    {
        if (_isContinious == true)
        {
            _locomotionService.DisableContinious();
            _continiousMark.SetActive(false);
            _isContinious = false;
        }
        else
        {
            _locomotionService.EnableContinious();
            _continiousMark.SetActive(true);
            _isContinious = true;
        }
    }

    private void OnDestroy()
    {
        _settingsAction.action.performed -= Action_performed;
        _ammoService.OnAmmoChanged -= OnAmmoChanged;
        _rockService.OnRockChanged -= OnRockChanged;
        _scoreService.OnScoreChanged -= OnScoreChanged;
    }
}
