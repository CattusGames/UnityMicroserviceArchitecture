using System;
using UnityEngine;

namespace Services.InputHandler
{
    public interface ITouchInput
    {
        event Action<Vector3> OnScreenTouched;
    }
}