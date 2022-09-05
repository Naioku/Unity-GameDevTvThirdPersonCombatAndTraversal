using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

namespace Combat.Targeting
{
    public class Targeter : MonoBehaviour
    {
        public Target CurrentTarget { get; private set; }

        [SerializeField] private CinemachineTargetGroup cinemachineTargetGroup;

        public List<Target> _targetList = new();

        private Camera _mainCamera;

        private void Start()
        {
            _mainCamera = Camera.main;
        }

        public bool SelectTarget()
        {
            if (_targetList.Count == 0) return false;

            Target targetClosestToScreenCenter = null;
            float closestTargetDistanceToCenter = Mathf.Infinity;
            Vector2 screenCenter = new(0.5f, 0.5f);
            
            foreach (var target in _targetList)
            {
                Vector2 targetScreenPosition = _mainCamera.WorldToViewportPoint(target.transform.position);
                if (!IsTargetOnTheScreen(targetScreenPosition))
                {
                    continue;
                }

                float checkedDistance = Vector2.Distance(screenCenter, targetScreenPosition);
                if (checkedDistance < closestTargetDistanceToCenter)
                {
                    closestTargetDistanceToCenter = checkedDistance;
                    targetClosestToScreenCenter = target;
                }
            }

            CurrentTarget = targetClosestToScreenCenter;

            if (targetClosestToScreenCenter == null) return false;
            
            cinemachineTargetGroup.AddMember(CurrentTarget.transform, 1f, 2f);

            return true;
        }

        private bool IsTargetOnTheScreen(Vector2 targetScreenPosition)
        {
            return targetScreenPosition.x is > 0f and < 1f &&
                   targetScreenPosition.y is > 0f and < 1f;

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
