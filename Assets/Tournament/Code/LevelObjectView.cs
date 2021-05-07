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
        [SerializeField] internal Rigidbody2D _rigidbody2D;
        [SerializeField] internal TrailRenderer _trail;

        internal Action<LevelObjectView> OnLevelObjectContact;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var levelObject = collision.gameObject.GetComponent<LevelObjectView>();
            OnLevelObjectContact.Invoke(levelObject);
        }

        #endregion
    }
}