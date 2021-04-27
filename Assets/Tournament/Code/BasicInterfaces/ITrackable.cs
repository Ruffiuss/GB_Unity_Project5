using UnityEngine;
using System;


namespace Tournament
{
    internal interface ITrackable
    {
        event Action<Vector3> CurrentPosition;
    }
}