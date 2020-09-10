using System;
using System.Collections.Generic;

namespace Scripting
{
    public class Variables
    {
        private readonly Dictionary<string, object> variables;

        public Variables()
        {
            variables = new Dictionary<string, object>();
        }

   

        public object this[string name]
        {
            get
            {
                variables.TryGetValue(name, out object _value);
                return _value;
            }
            set
            {
                if (variables.ContainsKey(name))
                    variables[name] = value;
                else
                    variables.Add(name, value);
            }
        }
    }
}