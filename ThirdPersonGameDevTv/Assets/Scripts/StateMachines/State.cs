using UnityEngine;

namespace StateMachines
{
    public abstract class State
    {
        public abstract void Enter();
        public abstract void Tick(float deltaTime);
        public abstract void Exit();
        
        protected void FaceTowards(GameObject subject, GameObject target)
        {
            Vector3 pointingVector = target.transform.position - subject.transform.position;
            pointingVector.y = 0f;

            subject.transform.rotation = Quaternion.LookRotation(pointingVector);
        }
    }
}
