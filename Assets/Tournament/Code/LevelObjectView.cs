using System;
using UnityEngine;


namespace Tournament
{
    internal sealed class LevelObjectView : MonoBehaviour, IView
    {
        #region Fields

        [SerializeField] internal Transform _transform;
        [SerializeField] internal SpriteRenderer _spriteRenderer;
        [SerializeField] internal Collider2D _collider2D;
        [SerializeField] internal Rigidbody2D _rigidBody2D;

        #endregion
    }
}