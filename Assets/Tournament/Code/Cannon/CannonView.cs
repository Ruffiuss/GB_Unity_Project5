using UnityEngine;
using System.Collections.Generic;


namespace Tournament
{
    internal sealed class CannonView : MonoBehaviour, IView
    {
        [SerializeField] internal Transform _muzzleTransform;
        [SerializeField] internal Transform _emitterTransform;
        [SerializeField] internal List<LevelObjectView> _bullets;
    }
}