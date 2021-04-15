using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Tournament
{ 
    public class GameLoader : MonoBehaviour
    {
        #region Fields

        [SerializeField] private SpriteAnimatorConfig _playerConfig;
        [SerializeField] private LevelObjectView _playerView;
        [SerializeField] private Transform _background;
        [SerializeField] private Transform _mainSprites;
        [SerializeField] private int _animationSpeed = 10;

        private SpriteAnimatorController _playerAnimator;
        private ControllerManager _controllerManager;
        private ParalaxManager _paralaxManager;

        #endregion


        #region UnityMethods

        private void Awake()
        {
            _controllerManager = new ControllerManager();

            _paralaxManager = new ParalaxManager(Camera.main.transform, _background, _mainSprites);

            _playerConfig = Resources.Load<SpriteAnimatorConfig>("AnimPlayerConfig");
            _playerAnimator = new SpriteAnimatorController(_playerConfig);
            _playerAnimator.StartAnimation(_playerView._spriteRenderer, AnimState.Run, true, _animationSpeed);

            _controllerManager.AddController(_playerAnimator);
            _controllerManager.AddController(_paralaxManager);
        }

        private void Update()
        {
            _controllerManager.Execute(Time.deltaTime);
        }

        private void FixedUpdate()
        {
            
        }

        #endregion
    }
}