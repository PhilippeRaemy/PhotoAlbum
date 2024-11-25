using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Tracer
{
    public static class Tracer
    {
        static bool _tracing;
        static Tracer()
        {
            Trace.Listeners.Add(new ConsoleTraceListener());
            if (Trace.Listeners != null && Trace.Listeners.Cast<TraceListener>().Any()){
                _tracing = true;
            }
        }

        public static void WriteLine(params (string, object)[] traces)
        {
            if (!_tracing) return;
            var message = string.Join(", ", traces.Select(s => $"{s.Item1}: {s.Item2}").ToArray());
            Trace.WriteLine(message);
        }

        public static void WriteLine(params object[] traces)
        {
            if (!_tracing) return;
            var message = string.Join(" ", traces);
            Trace.WriteLine(message);
        }

        public static void WriteLine(Func<string> tracer)
        {
            if (!_tracing || tracer is null) return;
            Trace.WriteLine(tracer.Invoke());
        }
    }
}
