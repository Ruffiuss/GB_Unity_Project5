using System;
using UnityEngine;


namespace Tournament
{
    internal sealed class PlayerController : IFixedExecutable
    {
        #region Fields

        private const float _WALK_SPEED = 150.0f;
        private const float _ANIMATIONS_SPEED = 20.0f;
        private const float _JUMP_FORCE = 10.0f;
        private const float _JUMP_THRESH = 0.1f;
        private const float _MOVING_THRESH = 0.1f;
        private const float _FLY_THRESH = 1.0f;
        private const float _GROUND_LEVEL = 0.05f;
        private const float _GRAVITY = -10.0f;

        private LevelObjectView _view;
        private SpriteAnimatorController _spriteAnimator;
        private readonly ContactPoller _contactPoller;

        private Vector3 _leftScale = new Vector3(-1, 1, 1);
        private Vector3 _rightScale = new Vector3(1, 1, 1);

        private float _yVelocity = 0.0f;
        private float _xAxisInput;
        private bool _isJump;

        #endregion


        #region ClassLifeCycles

        internal PlayerController(LevelObjectView view, SpriteAnimatorController spriteAnimator)
        {
            _view = view;
            _spriteAnimator = spriteAnimator;
            _contactPoller = new ContactPoller(_view._collider2D);
        }

        #endregion


        #region Methods

        public void FixedExecute(float fixedDeltaTime)
        {            
            _isJump = Input.GetAxis("Vertical") > 0;
            _xAxisInput = Input.GetAxis("Horizontal");
            _contactPoller.Execute(fixedDeltaTime);

            bool walks = Mathf.Abs(_xAxisInput) > _MOVING_THRESH;

            var newVelocity = 0.0f;

            if (walks && (_xAxisInput > 0 || !_contactPoller.HasLeftContact) && (_xAxisInput < 0 || !_contactPoller.HasRightContact))
            {
                newVelocity = fixedDeltaTime * _WALK_SPEED * (_xAxisInput < 0 ? -1 : 1);
                _view.transform.localScale = (_xAxisInput < 0 ? _leftScale : _rightScale);
            }
            _view._rigidBody2D.velocity = _view._rigidBody2D.velocity.Change(_xAxisInput: newVelocity);

            if (_contactPoller.IsGrounded && _isJump && Mathf.Abs(_view._rigidBody2D.velocity.y) <= _JUMP_THRESH)
            {
                _view._rigidBody2D.AddForce(Vector2.up * _JUMP_FORCE, ForceMode2D.Impulse);
            }

            if (_contactPoller.IsGrounded)
            {
                _spriteAnimator.StartAnimation(_view._spriteRenderer, walks ? AnimState.Run : AnimState.Idle, true, _ANIMATIONS_SPEED);
            }
            else if (Mathf.Abs(_view._rigidBody2D.velocity.y) > _FLY_THRESH)
            {
                _spriteAnimator.StartAnimation(_view._spriteRenderer, AnimState.Jump, true, _ANIMATIONS_SPEED);
            }
        }

        #endregion
    }
}