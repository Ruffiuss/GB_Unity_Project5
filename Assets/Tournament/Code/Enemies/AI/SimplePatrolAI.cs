using System;
using UnityEngine;


namespace Tournament
{
    internal class SimplePatrolAI : IFixedExecutable
    {
        #region Fields

        private readonly LevelObjectView _view;
        private readonly SimplePatrolModel _model;

        #endregion


        #region Class life cycles

        internal SimplePatrolAI(LevelObjectView view, SimplePatrolModel model)
        {
            _view = view != null ? view : throw new ArgumentNullException(nameof(view));
            _model = model != null ? model : throw new ArgumentNullException(nameof(model));
        }

        #endregion


        #region Methods

        public void FixedExecute(float fixedDeltaTime)
        {
            var newVelocity = _model.CalculateVelocity(_view._transform.position) * fixedDeltaTime;
            _view._rigidbody2D.velocity = newVelocity;
        }

        #endregion
    }
}
