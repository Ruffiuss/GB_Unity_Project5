using UnityEngine;
using System.Collections.Generic;


namespace Tournament
{
    internal sealed class BulletsEmitterController : IExecutable
    {
        #region Fields

        private const float _delay = 1;
        private const float _startSpeed = 5;

        private List<BulletController> _bullets = new List<BulletController>();
        private Transform _transform;

        private int _currentIndex;
        private float _timeTillNextBullet;


        #endregion


        #region ClassLifeCycles

        internal BulletsEmitterController(List<LevelObjectView> bulletViews, Transform transform)
        {
            _transform = transform;

            foreach (var bulletView in bulletViews)
            {
                _bullets.Add(new BulletController(bulletView));
            }
        }

        #endregion


        #region Methods

        public void Execute(float deltaTime)
        {
            if (_timeTillNextBullet > 0)
            {
                _timeTillNextBullet -= deltaTime;
            }
            else
            {
                _timeTillNextBullet = _delay;
                _bullets[_currentIndex].Throw(_transform.position, _transform.right * _startSpeed);
                _currentIndex++;
                if (_currentIndex >= _bullets.Count) _currentIndex = 0;
            }
            _bullets.ForEach(b => b.Execute(deltaTime));
        }

        #endregion
    }
}