using System;
using OpenTracing;

namespace Datadog.Trace.OpenTracing
{
    internal class OpenTracingScopeManager : global::OpenTracing.IScopeManager
    {
        private readonly IScopeManager _scopeManager;

        internal OpenTracingScopeManager(IScopeManager scopeManager)
        {
            _scopeManager = scopeManager;
        }

        public IScope Active
        {
            get => new OpenTracingScope(_scopeManager.Active);
            set => Activate(value.Span, false);
        }

        public IScope Activate(ISpan span, bool finishSpanOnDispose)
        {
            if (span is OpenTracingSpan openTracingSpan)
            {
                var scope = _scopeManager.Activate(openTracingSpan.DDSpan, finishSpanOnDispose);
                return new OpenTracingScope(scope);
            }

            throw new NotSupportedException("OpenTracingScopeManager only supports OpenTracingSpan");
        }

        public void Close(Scope scope)
        {
            _scopeManager.Close(scope);
        }
    }
}
