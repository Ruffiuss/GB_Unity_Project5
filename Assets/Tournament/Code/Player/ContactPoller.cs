using System.Collections.Generic;
using System.Collections;
using UnityEngine;


namespace Tournament
{
    internal class ContactPoller : IExecutable
    {
        #region Fields

        private const float _COLLISION_TRESH = 0.6f;

        private readonly Collider2D _collider2D;
        private ContactPoint2D[] _contacts = new ContactPoint2D[10];
        private int _contactCount;

        #endregion


        #region Properties

        public bool IsGrounded { get; private set; }
        public bool HasLeftContact { get; private set; }
        public bool HasRightContact { get; private set; }
        public bool HasUpContact { get; private set; }

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
            IsGrounded = false;
            HasLeftContact = false;
            HasRightContact = false;
            HasUpContact = false;

            _contactCount = _collider2D.GetContacts(_contacts);

            for (int i = 0; i < _contactCount; i++)
            {
                var normal = _contacts[i].normal;
                var rigidBody = _contacts[i].rigidbody;

                if (normal.y > _COLLISION_TRESH) IsGrounded = true;
                if (normal.y < _COLLISION_TRESH) HasUpContact = true;
                if (normal.x > _COLLISION_TRESH) HasLeftContact = true;
                if (normal.x < -_COLLISION_TRESH) HasRightContact = true;
            }
        }

        #endregion
    }
}