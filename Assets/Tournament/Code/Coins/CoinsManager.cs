using System;
using System.Collections.Generic;
using UnityEngine;


namespace Tournament
{
    internal sealed class CoinsManager : IDisposable
    {
        #region Fields

        private const float _ANIMATION_SPEED = 10.0f;

        private LevelObjectView _characterView;
        private SpriteAnimatorController _spriteAnimator;
        private List<LevelObjectView> _coinViews;

        #endregion


        #region ClassLifeCycles

        internal CoinsManager(LevelObjectView characterView, List<LevelObjectView> coinViews, SpriteAnimatorController spriteAnimator)
        {
            _characterView = characterView;
            _spriteAnimator = spriteAnimator;
            _coinViews = coinViews;

            _characterView.OnLevelObjectContact += OnLevelObjectContact;

            foreach (var coinView in coinViews)
            {
                _spriteAnimator.StartAnimation(coinView._spriteRenderer, AnimState.Run, true, _ANIMATION_SPEED);
            }
        }

        #endregion


        #region Methods

        private void OnLevelObjectContact(LevelObjectView contactView)
        {
            if (_coinViews.Contains(contactView))
            {
                _spriteAnimator.StopAnimation(contactView._spriteRenderer);
                GameObject.Destroy(contactView.gameObject);
            }
        }

        public void Dispose()
        {
            _characterView.OnLevelObjectContact -= OnLevelObjectContact;
        }

        #endregion
    }
}
