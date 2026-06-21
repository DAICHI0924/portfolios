using UnityEngine;

namespace Kakky
{
    public abstract class StateBase : IState
    {

        public virtual void OnEnter(GameDataContext context)
        {
            Debug.Log("Enter " + GetType().Name);
        }

        public virtual void OnUpdate(GameDataContext context)
        {

        }

        public virtual void OnExit(GameDataContext context)
        {

        }
    }
}