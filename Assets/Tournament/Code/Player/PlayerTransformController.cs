using System;
using UnityEngine;


namespace Tournament
{
    internal sealed class PlayerTransformController : IExecutable
    {
        #region Fields

        private const float _walkSpeed = 3f;
        private const float _animationsSpeed = 10f;
        private const float _jumpStartSpeed = 8f;
        private const float _movingThresh = 0.1f;
        private const float _flyThresh = 1f;
        private const float _groundLevel = 0.5f;
        private const float _g = -10f;

        private Vector3 _leftScale = new Vector3(-1, 1, 1);
        private Vector3 _rightScale = new Vector3(1, 1, 1);

        private float _yVelocity;
        private bool _doJump;
        private float _xAxisInput;

        private LevelObjectView _view;
        private SpriteAnimatorController _spriteAnimator;

        #endregion

        public PlayerTransformController(LevelObjectView view, SpriteAnimatorController spriteAnimator)
        {
            _view = view;
            _spriteAnimator = spriteAnimator;
        }

        public void Execute(float deltaTime)
        {
            _doJump = Input.GetAxis("Vertical") > 0;
            _xAxisInput = Input.GetAxis("Horizontal");
            var goSideWay = Mathf.Abs(_xAxisInput) > _movingThresh;

            if (IsGrounded())
            {
                //walking
                if (goSideWay) GoSideWay(deltaTime);
                _spriteAnimator.StartAnimation(_view._spriteRenderer, goSideWay ? AnimState.Run : AnimState.Idle, true, _animationsSpeed);

                //start jump
                if (_doJump && _yVelocity == 0)
                {
                    _yVelocity = _jumpStartSpeed;
                }
                //stop jump
                else if (_yVelocity < 0)
                {
                    _yVelocity = 0;
                    _view._transform.position = _view._transform.position.Change(y: _groundLevel);
                }
            }
            else
            {
                //flying
                if (goSideWay) GoSideWay(deltaTime);
                if (Mathf.Abs(_yVelocity) > _flyThresh)
                {
                    _spriteAnimator.StartAnimation(_view._spriteRenderer, AnimState.Jump, true, _animationsSpeed);
                }
                _yVelocity += _g * deltaTime;
                _view._transform.position += Vector3.up * (deltaTime * _yVelocity);
            }
        }

        private void GoSideWay(float deltaTime)
        {
            _view._transform.position += Vector3.right * (deltaTime * _walkSpeed * (_xAxisInput < 0 ? -1 : 1));
            _view._transform.localScale = (_xAxisInput < 0 ? _leftScale : _rightScale);
        }

        public bool IsGrounded()
        {
            return _view._transform.position.y <= _groundLevel + float.Epsilon && _yVelocity <= 0;
        }
    }
}