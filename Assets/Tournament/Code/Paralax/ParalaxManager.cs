using UnityEngine;


namespace Tournament
{
    internal sealed class ParalaxManager : IExecutable
    {
        #region Fields

        private Transform _camera;
        private Transform _background;
        private Transform _mainSprites;
        private Vector3 _backStartPosition;
        private Vector3 _mainSpritesStartPosition;
        private Vector3 _cameraStartPosition;

        private const float _COEF = 0.3f;

        #endregion


        #region ClassLifeCycles

        internal ParalaxManager(Transform camera, Transform back, Transform mainSprites)
        {
            _camera = camera;
            _background = back;
            _mainSprites = mainSprites;

            _backStartPosition = _background.transform.position;
            _cameraStartPosition = _camera.transform.position;
            _mainSpritesStartPosition = _mainSprites.transform.position;
        }

        #endregion


        #region Methods

        public void Execute(float deltaTime)
        {
            _background.position = _backStartPosition + (_camera.position - _cameraStartPosition) * _COEF;
            _mainSprites.position = _mainSpritesStartPosition + (_camera.position - _cameraStartPosition) * (_COEF * 2);
        }

        #endregion
    }
}