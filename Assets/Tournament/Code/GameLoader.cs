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
        [SerializeField] private int _animationSpeed = 10;

        private SpriteAnimatorController _playerAnimator;

        #endregion


        #region UnityMethods

        private void Awake()
        {
            _playerConfig = Resources.Load<SpriteAnimatorConfig>("AnimPlayerConfig");
            _playerAnimator = new SpriteAnimatorController(_playerConfig);
            _playerAnimator.StartAnimation(_playerView._spriteRenderer, AnimState.Run, true, _animationSpeed);
        }

        private void Update()
        {
            _playerAnimator.Execute(Time.deltaTime);
        }

        private void FixedUpdate()
        {
            
        }

        #endregion
    }
}