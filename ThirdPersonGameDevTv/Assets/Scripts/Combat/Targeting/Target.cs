using System;
using UnityEngine;

namespace Combat.Targeting
{
    public class Target : MonoBehaviour
    {
        public event Action<Target> DestroyEvent;

        private void OnDestroy()
        {
            DestroyEvent?.Invoke(this);
        }
    }
}
