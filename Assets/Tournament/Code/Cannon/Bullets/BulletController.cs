using System;
using UnityEngine;


namespace Tournament
{
    internal sealed class BulletController : IExecutable
    {
        #region Fields

        private LevelObjectView _view;
        private Vector3 _velocity;

        private float _radius = 0.08f;
        private float _groundLevel = -3.0f;
        private float _g = -10.0f;
        private bool _isAtStartPosition;

        #endregion


        #region ClassLifeCycles

        internal BulletController(LevelObjectView view)
        {
            _view = view;
            Active(false);
        }

        #endregion


        #region Methods

        public void Execute(float deltaTime)
        {
            if (IsGrounded())
            {
                SetVelocity(_velocity.Change(y: -_velocity.y));
                _view._transform.position = _view._transform.position.Change(y: _groundLevel + _radius);
            }
            else
            {
                SetVelocity(_velocity + Vector3.up * _g * deltaTime);
                _view._transform.position += _velocity * deltaTime;
                if (_isAtStartPosition)
                {
                    Active(true);
                }
            }
        }

        public void Throw(Vector3 position, Vector3 velocity)
        {
            _view._transform.position = position;
            SetVelocity(velocity);
            _isAtStartPosition = true;
        }

        private void SetVelocity(Vector3 velocity)
        {
            _velocity = velocity;
            var angle = Vector3.Angle(Vector3.left, _velocity);
            var axis = Vector3.Cross(Vector3.left, _velocity);
            _view._transform.rotation = Quaternion.AngleAxis(angle -90, axis);
        }

        private bool IsGrounded()
        {
            return _view._transform.position.y <= _groundLevel + _radius + float.Epsilon && _velocity.y <= 0;
        }

        private void Active(bool value)
        {
            _view._trail.enabled = value;
            _view.gameObject.SetActive(value);
            _isAtStartPosition = false;
        }

        #endregion
    }
}