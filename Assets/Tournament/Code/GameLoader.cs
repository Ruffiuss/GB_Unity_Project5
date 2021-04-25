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
        [SerializeField] private CannonView _cannonView;
        [SerializeField] private List<LevelObjectView> _coinList;

        private SpriteAnimatorController _coinAnimator;
        private ControllerManager _controllerManager;
        private ParalaxManager _paralaxManager;
        private CannonAimController _cannonAimController;
        private BulletsEmitterController _bulletsEmitterController;
        private CoinsManager _coinsManager;
        private ResourceLoader _configLoader;

        #endregion


        #region UnityMethods

        private void Awake()
        {
            _configLoader = new ResourceLoader();

            _controllerManager = new ControllerManager();

            var player = new PlayerInit(_configLoader.LoadConfig("AnimPlayerConfig"), _configLoader.LoadPrefab("Player"), _playerSpawn); 

            _paralaxManager = new ParalaxManager(Camera.main.transform, _background, _mainSprites);

            _cannonAimController = new CannonAimController(_cannonView._muzzleTransform, _cannonView._emitterTransform, player.Controller);
            _bulletsEmitterController = new BulletsEmitterController(_cannonView._bullets, _cannonView._emitterTransform);

            _coinAnimator = new SpriteAnimatorController(_configLoader.LoadConfig("AnimCoinConfig"));
            _coinsManager = new CoinsManager(player.View, _coinList, _coinAnimator);

            _controllerManager.AddController(player.Animator);
            _controllerManager.AddController(player.Controller);
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
    }
}