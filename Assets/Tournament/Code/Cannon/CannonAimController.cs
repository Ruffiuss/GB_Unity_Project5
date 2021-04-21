using UnityEngine;


namespace Tournament
{
    internal sealed class CannonAimController : IExecutable
    {
        #region Fields

        private ITrackable _target;
        private Transform _muzzleTransform;
        private Transform _aimTransform;

        private Vector3 _targetPosition;
        private Vector3 _direction;
        private Vector3 _axis;
        private float _angle;

        #endregion


        #region ClassLifeCycles

        internal CannonAimController(Transform muzzleTransform, Transform aimTransform, ITrackable target)
        {
            _target = target;
            _target.CurrentPosition += TrackTargetPosition;

            _muzzleTransform = muzzleTransform;
            _aimTransform = aimTransform;
        }

        #endregion


        #region Methods

        public void Execute(float deltaTime)
        {
            _direction = _targetPosition - _muzzleTransform.position; 
            _angle = Vector3.Angle(Vector3.down, _direction);
            _axis = Vector3.Cross(Vector3.down, _direction);
            _muzzleTransform.rotation = Quaternion.AngleAxis(_angle, _axis);
        }

        private void TrackTargetPosition(Vector3 position)
        {
            _targetPosition = position;
        }

        private void UntrackTargetPosition()
        {
            _target.CurrentPosition -= TrackTargetPosition;
        }

        #endregion
    }
}