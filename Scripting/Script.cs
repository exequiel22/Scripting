using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Scripting;
using System;
using System.Runtime.Caching;
using System.Threading.Tasks;

namespace Scripting
{
    public class Script
    {
        private readonly Globals globals;
        public Script()
        {
            globals = new Globals();
        }

        public Task RunAsync(string code)
        {
            return GetScript().ContinueWithAsync(code);
        }
        public void Run(string code)
        {
            RunAsync(code).Wait();
        }
        public async Task<T> EvalAsync<T>(string code)
        {
            var state = await GetScript().ContinueWithAsync<T>(code);
            return state.ReturnValue;
        }
        public T Eval<T>(string code)
        {
            return EvalAsync<T>(code).Result;
        }
        public object Eval(string code)
        {
            return Eval<object>(code);
        }

        public T GetVariable<T>(string name) where T : IConvertible
        {
            var value = GetVariable(name);
            return value.ConvertOrDefault<T>();
        }
        public object GetVariable(string name)
        {
            return Eval($"Variables[\"{name}\"]");
        }
        public void SetVariable<T>(string name, T value) where T : IConvertible
        {
            if (typeof(T).IsAssignableFrom(typeof(string)))
                Run($"Variables[\"{name}\"] = \"{value}\";");
            else
                Run($"Variables[\"{name}\"] = {value};");
        }
        private ScriptState<object> GetScript()
        {
            var cache = MemoryCache.Default;
            var _script = cache["script"] as ScriptState<object>;
            if (_script == null)
            {
                var opts = ScriptOptions.Default;
                _script = CSharpScript.RunAsync("", opts, globals).Result;
                CacheItemPolicy policy = new CacheItemPolicy();
                cache.Set("script", _script, policy);
            }
            return _script;
        }
    }
}
