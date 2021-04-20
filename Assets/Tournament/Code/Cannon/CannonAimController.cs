using UnityEngine;


namespace Tournament
{
    internal sealed class CannonAimController : IExecutable
    {
        #region Fields

        private Transform _muzzleTransform;
        private Transform _aimTransform;

        private Vector3 _direction;
        private Vector3 _axis;
        private float _angle;

        #endregion


        #region ClassLifeCycles

        internal CannonAimController(Transform muzzleTransform, Transform aimTransform)
        {
            _muzzleTransform = muzzleTransform;
            _aimTransform = aimTransform;
        }

        #endregion


        #region Methods

        public void Execute(float deltaTime)
        {
            _direction = _aimTransform.position - _muzzleTransform.position;
            _angle = Vector3.Angle(Vector3.down, _direction);
            _axis = Vector3.Cross(Vector3.down, _direction);
            _muzzleTransform.rotation = Quaternion.AngleAxis(_angle, _axis);
        }

        #endregion
    }
}