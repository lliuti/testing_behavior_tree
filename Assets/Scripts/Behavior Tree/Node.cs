using System.Collections.Generic;

namespace BehaviorTree 
{
    public enum NodeState
    {
        RUNNING,
        SUCCESS,
        FAILURE
    }
    
    public abstract class Node
    {   
        protected NodeState state;
        public Node parent;
        protected List<Node> children = new List<Node>();
        private Dictionary<string, object> _dataContext = new Dictionary<string, object>();

        public Node()
        {
            parent = null;
        }

        public Node(List<Node> children)
        {
            foreach (Node child in children)
            {
                Attach(child);
            }
        }

        private void Attach(Node node)
        {
            node.parent = this;
            children.Add(node);
        }

        public virtual NodeState Evaluate() => NodeState.FAILURE;

        public void SetData(string key, object value)
        {
            _dataContext[key] = value;
        }

        public object GetData(string key)
        {
            if (_dataContext.TryGetValue(key, out object value)) 
            {
                return value;
            }

            if (parent != null) return parent.GetData(key);

            return null;
        }

        public bool ClearData(string key)
        {
            if (_dataContext.ContainsKey(key)) 
            {
                _dataContext.Remove(key);
                return true;
            }
            
            if (parent != null) return parent.ClearData(key);
            return false;
        }
    }
}
