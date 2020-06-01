using OpenTracing;

namespace Datadog.Trace.OpenTracing
{
    internal sealed class OpenTracingScope : IScope
    {
        private readonly OpenTracingSpan _wrappedSpan;
        private readonly Scope _wrappedScope;

        public OpenTracingScope(Scope scope)
        {
            _wrappedScope = scope;
            _wrappedSpan = new OpenTracingSpan(scope.Span);
        }

        public ISpan Span => _wrappedSpan;

        public void Dispose()
        {
            _wrappedScope.Close();
        }
    }
}
