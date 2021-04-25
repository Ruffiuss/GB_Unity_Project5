using System.Collections.Generic;
using UnityEngine;


namespace Tournament
{ 
    public class GameLoader : MonoBehaviour
    {
        #region Fields

        [SerializeField] private Transform _background;
        [SerializeField] private Transform _mainSprites;
        [SerializeField] private Transform _playerSpawn;
        [SerializeField] private LevelObjectView _deathZone;
        [SerializeField] private CannonView _cannonView;
        [SerializeField] private List<LevelObjectView> _coinList;

        private SpriteAnimatorController _coinAnimator;
        private ControllerManager _controllerManager;
        private ParalaxManager _paralaxManager;
        private CannonAimController _cannonAimController;
        private BulletsEmitterController _bulletsEmitterController;
        private CoinsManager _coinsManager;
        private ResourceLoader _resourcesLoader;
        private PlayerInit _player;

        #endregion


        #region UnityMethods

        private void Awake()
        {
            _resourcesLoader = new ResourceLoader();

            _controllerManager = new ControllerManager();

            _player = new PlayerInit(_resourcesLoader.LoadConfig("AnimPlayerConfig"), _resourcesLoader.LoadPrefab("Player"), _playerSpawn.position); 

            _paralaxManager = new ParalaxManager(Camera.main.transform, _background, _mainSprites);

            _cannonAimController = new CannonAimController(_cannonView._muzzleTransform, _cannonView._emitterTransform, _player.Controller);
            _bulletsEmitterController = new BulletsEmitterController(_cannonView._bullets, _cannonView._emitterTransform);

            _coinAnimator = new SpriteAnimatorController(_resourcesLoader.LoadConfig("AnimCoinConfig"));
            _coinsManager = new CoinsManager(_player.View, _coinList, _coinAnimator);

            _deathZone.OnLevelObjectContact += DeathZoneHandler;

            _controllerManager.AddController(_player.Animator);
            _controllerManager.AddController(_player.Controller);
            _controllerManager.AddController(_paralaxManager);
            _controllerManager.AddController(_cannonAimController);
            _controllerManager.AddController(_bulletsEmitterController);
            _controllerManager.AddController(_coinAnimator);
        }

        private void Update()
        {
            _controllerManager.Execute(Time.deltaTime);
        }

        private void FixedUpdate()
        {
            _controllerManager.FixedExecute(Time.fixedDeltaTime);
        }

        #endregion


        #region Methods

        private void DeathZoneHandler(LevelObjectView levelObjectView)
        {
            if (levelObjectView.gameObject.CompareTag("Player"))
            {
                _player.Respawn(_playerSpawn.position);
            }
        }

        #endregion
    }
}