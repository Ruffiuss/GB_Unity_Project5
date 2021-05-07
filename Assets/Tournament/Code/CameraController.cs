using UnityEngine;


namespace Tournament
{
    internal sealed class CameraController : IExecutable
    {
        #region Fields

        private Transform _mCamTransform; 
        private Vector3 _targetPosition;
        private ITrackable _target;

        private float OffsetX = 1.5f;
        private float OffsetY = 1.5f;
        private int CamSpeed = 300;

        #endregion


        #region ClassLifeCycles

        internal CameraController(ITrackable target, Transform camera)
        {
            _target = target;
            _mCamTransform = camera;
            target.CurrentPosition += TrackTarget;
        }

        #endregion


        #region Methods

        private void TrackTarget(Vector3 position)
        {
            _targetPosition = position;
        }

        public void Execute(float deltaTime)
        {
            _mCamTransform.transform.position = Vector3.Lerp(_mCamTransform.transform.position,
                                                   new Vector3(_targetPosition.x + OffsetX, _targetPosition.y + OffsetY, _mCamTransform.transform.position.z),
                                                   deltaTime * CamSpeed);
        }

        #endregion
    }
}
