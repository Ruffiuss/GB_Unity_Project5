using UnityEngine;


namespace Tournament
{
    internal class ContactPoller : IExecutable
    {
        #region Fields

        private Collider2D _collider2D;

        #endregion


        #region ClassLifeCycles

        internal ContactPoller(Collider2D collider2D)
        {
            _collider2D = collider2D;
        }

        #endregion


        #region Methods

        public void Execute(float deltaTime)
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}