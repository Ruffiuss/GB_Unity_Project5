using System;
using UnityEngine;


namespace Tournament
{
    internal sealed class BulletController
    {
        #region Fields

        private LevelObjectView _view;
        private Vector3 _velocity;

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

        public void Throw(Vector3 position, Vector3 velocity)
        {
            Active(false);
            _view._transform.position = position;
            _view._rigidBody2D.velocity = Vector2.zero;
            _view._rigidBody2D.angularVelocity = 0;
            _view._rigidBody2D.AddForce(velocity, ForceMode2D.Impulse);
            Active(true);
        }

        private void SetVelocity(Vector3 velocity)
        {
            _velocity = velocity;
            var angle = Vector3.Angle(Vector3.left, _velocity);
            var axis = Vector3.Cross(Vector3.left, _velocity);
            _view._transform.rotation = Quaternion.AngleAxis(angle -90, axis);
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