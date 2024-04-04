using UnityEngine;

namespace BehaviorTree 
{
    public abstract class Tree : MonoBehaviour
    {
        private Node _root = null;

        void Start() // no tutorial ela usou "protected" aqui
        {
            _root = SetupTree();
        }

        void Update()
        {
            if (_root != null)
            {
                _root.Evaluate();
            }
        }

        protected abstract Node SetupTree();

    }
}
