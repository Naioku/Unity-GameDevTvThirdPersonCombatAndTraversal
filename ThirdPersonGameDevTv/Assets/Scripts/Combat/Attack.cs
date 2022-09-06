using System;
using UnityEngine;

namespace Combat
{
    [Serializable]
    public class Attack
    {
        [field: SerializeField] public string AnimationName { get; private set; }
        [field: SerializeField] public float TransitionDuration { get; private set; }
        [field: SerializeField] public int NextAttackIndex { get; private set; } = -1;
        [field: SerializeField] public float ComboAttackTime { get; private set; }
    }
}
