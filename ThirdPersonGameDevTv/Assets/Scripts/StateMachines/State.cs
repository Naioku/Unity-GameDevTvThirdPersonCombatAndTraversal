using UnityEngine;

namespace StateMachines
{
    public abstract class State
    {
        public abstract void Enter();
        public abstract void Tick(float deltaTime);
        public abstract void Exit();
        
        /// <summary>
        /// Normalized time - The integer part is the number of time a state has been looped.
        /// The fractional part is the % (0-1) of progress in the current loop.
        ///
        /// It gets the normalized time of currently played animation tagged with "Attack".
        /// </summary>
        /// <returns></returns>
        protected float GetNormalizedAnimationTime(Animator animator)
        {
            AnimatorStateInfo currentInfo = animator.GetCurrentAnimatorStateInfo(0);
            AnimatorStateInfo nextInfo = animator.GetNextAnimatorStateInfo(0);

            if (animator.IsInTransition(0) && nextInfo.IsTag("Attack"))
            {
                return nextInfo.normalizedTime;
            }

            if (!animator.IsInTransition(0) && currentInfo.IsTag("Attack"))
            {
                return currentInfo.normalizedTime;
            }

            return 0f;
        }
        
        protected void FaceTowards(GameObject subject, GameObject target)
        {
            Vector3 pointingVector = target.transform.position - subject.transform.position;
            pointingVector.y = 0f;

            subject.transform.rotation = Quaternion.LookRotation(pointingVector);
        }
    }
}
