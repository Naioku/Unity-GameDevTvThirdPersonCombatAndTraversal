using System;
using UnityEngine;

namespace Combat
{
    [Serializable]
    public class Attack
    {
        [field: SerializeField]
        public string AnimationName { get; private set; }
        
        [field: SerializeField]
        public float TransitionDuration { get; private set; }
        
        [field: Tooltip("Which attack in the attack array should be next.")]
        [field: SerializeField]
        public int NextAttackIndex { get; private set; } = -1;
        
        [field: Tooltip("In/after what fraction of the animation time next attack can be performed? " +
                 "In/after what time player should click to perform next combo attack?")]
        [field: Range(0f, 1f)]
        [field: SerializeField]
        public float NextComboAttackTime { get; private set; }
        
        [field: SerializeField]
        public float ForceValue { get; private set; }
        
        [field: Tooltip("In what fraction of the animation time attack impacting the player should be applied " +
                        "Should be greater than Next Combo Attack Time. Otherwise next attack can cancel the " +
                        "force addition from the current attack.")]
        [field: Range(0f, 1f)]
        [field: SerializeField]
        public float ForceTime { get; private set; }

        [field: SerializeField] public int Damage { get; private set; } = 5;
        [field: SerializeField] public float KnockBack { get; private set; } = 5f;
    }
}
