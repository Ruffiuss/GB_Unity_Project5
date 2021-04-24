using System;
using System.Collections.Generic;


namespace Tournament
{
    internal sealed class ControllerManager : IExecutable, IFixedExecutable
    {
        #region Fields

        private List<IExecutable> _executables;
        private List<IFixedExecutable> _fixedExecutables;

        #endregion


        #region ClassLifeCycles

        internal ControllerManager()
        {
            _executables = new List<IExecutable>();
            _fixedExecutables = new List<IFixedExecutable>();
        }

        #endregion


        #region Methods

        public void Execute(float deltaTime)
        {
            foreach (var executable in _executables)
            {
                executable.Execute(deltaTime);
            }
        }

        public void FixedExecute(float fixedDeltaTime)
        {
            foreach (var fixedExecutable in _fixedExecutables)
            {
                fixedExecutable.FixedExecute(fixedDeltaTime);
            }
        }

        internal void AddController<T>(T controller)
        {
            switch (controller)
            {
                case IExecutable executable:
                    _executables.Add(executable);
                    break;
                default:
                    break;
            }
        }

        #endregion
    }
}