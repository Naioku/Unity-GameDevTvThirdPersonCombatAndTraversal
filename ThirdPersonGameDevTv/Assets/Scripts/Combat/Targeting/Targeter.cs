using System;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

namespace Combat.Targeting
{
    public class Targeter : MonoBehaviour
    {
        public Target CurrentTarget { get; private set; }

        [SerializeField] private CinemachineTargetGroup cinemachineTargetGroup;
        
        private readonly List<Target> _targetList = new();

        public bool SelectTarget()
        {
            if (_targetList.Count == 0) return false;

            CurrentTarget = _targetList[0];
            cinemachineTargetGroup.AddMember(CurrentTarget.transform, 1f, 2f);

            return true;
        }

        public void Cancel()
        {
            if (CurrentTarget == null) return;
            
            cinemachineTargetGroup.RemoveMember(CurrentTarget.transform);
            CurrentTarget = null;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent<Target>(out var target)) return;
            
            _targetList.Add(target);
            target.DestroyEvent += RemoveTarget;
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.TryGetComponent<Target>(out var target)) return;

            RemoveTarget(target);
        }

        private void RemoveTarget(Target target)
        {
            if (target == CurrentTarget)
            {
                cinemachineTargetGroup.RemoveMember(CurrentTarget.transform);
                CurrentTarget = null;
            }

            target.DestroyEvent -= RemoveTarget;
            _targetList.Remove(target);
        }
    }
}
