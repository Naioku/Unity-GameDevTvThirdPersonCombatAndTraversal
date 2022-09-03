using UnityEngine;

namespace StateMachines
{
    /// <summary>
    /// Abstract keyword does not allow to add object of that class to the game object.
    /// </summary>
    public abstract class StateMachine : MonoBehaviour
    {
        private State _currentState;

        private void Update()
        {
            _currentState?.Tick(Time.deltaTime);
        }

        public void SwitchState(State state)
        {
            _currentState?.Exit();
            _currentState = state;
            _currentState?.Enter();
        }
    }
}
