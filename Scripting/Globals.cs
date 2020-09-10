using System;

namespace Scripting
{
    public class Globals
    {
        public Variables Variables { get; private set; }

        public Globals()
        {
            Variables = new Variables();
        }

        public TResult ConvertOrDefault<TResult>(object value)
            where TResult : IConvertible
        {
            return value.ConvertOrDefault<TResult>();
        }
    }
}