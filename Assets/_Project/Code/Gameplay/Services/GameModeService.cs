using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public sealed class GameModeService : MonoBehaviour
{
    [SerializeField] private GameObject _easyModeWeapons;
    [SerializeField] private GameObject _hardModeWeapons;
    [SerializeField] private GameObject _gameOver;
    [SerializeField] private GameObject _curtain;
    [SerializeField] private Image _curtainImage;
    [SerializeField] private float _transitionTime;

    [SerializeField] private Transform _player;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private GameObject _mainMenu;

    private AmmoService _ammoService;
    private RockService _rockService;

    private bool _isRockEmpty;
    private bool _isAmmoEmpty;

    private void Awake()
    {
        _ammoService = FindFirstObjectByType<AmmoService>();
        _rockService = FindFirstObjectByType<RockService>();
        _curtainImage.color = new Color(0f, 0f, 0f, 1f);
        _curtain.SetActive(true);

        _ammoService.OnAmmoChanged += OnAmmoChanged;
        _rockService.OnRockChanged += OnRockChanged;
    }

    private void OnRockChanged(int rock)
    {
        if (rock == 0)
        {
            _isRockEmpty = true;

            if (_isAmmoEmpty == true)
                SetGameOver().Forget();
        }
    }

    private void OnAmmoChanged(int ammo)
    {
        if (ammo == 0)
        {
            _isAmmoEmpty = true;

            if (_isRockEmpty == true)
                SetGameOver().Forget();
        }
    }

    private void Start()
    {
        _curtainImage.DOFade(0f, _transitionTime).OnComplete(() => {
            _curtain.SetActive(false);
        });
    }

    public void SetEasyMode()
    {
        _mainMenu.SetActive(false);
        _curtain.SetActive(true);
        _curtainImage.color = new Color(0f, 0f, 0f, 0f);
        _curtainImage.DOFade(1f, _transitionTime).OnComplete(() => {
            _easyModeWeapons.SetActive(true);
            _hardModeWeapons.SetActive(false);
            _ammoService.SetAmmo(50);
            _rockService.SetRock(0);
            _player.position = _spawnPoint.position;
            _curtainImage.DOFade(0f, _transitionTime).OnComplete(() => {
                _curtain.SetActive(false);
            });
        });
    }

    public void SetHardMode()
    {
        _mainMenu.SetActive(false);
        _curtain.SetActive(true);
        _curtainImage.color = new Color(0f, 0f, 0f, 0f);
        _curtainImage.DOFade(1f, _transitionTime).OnComplete(() => {
            _easyModeWeapons.SetActive(false);
            _hardModeWeapons.SetActive(true);
            _ammoService.SetAmmo(15);
            _rockService.SetRock(15);
            _player.position = _spawnPoint.position;
            _curtainImage.DOFade(0f, _transitionTime).OnComplete(() => {
                _curtain.SetActive(false);
            });
        });
    }

    public void ReloadGame()
    {
        _curtain.SetActive(true);
        _curtainImage.color = new Color(0f, 0f, 0f, 0f);
        _curtainImage.DOFade(1f, _transitionTime).OnComplete(() => { 
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        });
    }

    public async UniTask SetGameOver()
    {
        await UniTask.Delay(3000);
        _gameOver.SetActive(true);
    }

    public void OnDestroy()
    {
        _ammoService.OnAmmoChanged -= OnAmmoChanged;
        _rockService.OnRockChanged -= OnRockChanged;
    }
}