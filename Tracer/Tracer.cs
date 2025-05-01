using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Tracer
{
    public static class ConsoleTitle
    {
        static readonly ConsoleTitlePreserver ConsoleTitlePreserver;
        static ConsoleTitle() => ConsoleTitlePreserver = new ConsoleTitlePreserver();
        public static void Set(string title) => Console.Title = title;
    }

    class ConsoleTitlePreserver
    {
        static readonly string Title;

        static ConsoleTitlePreserver() => Title = Console.Title;
        ~ConsoleTitlePreserver() => Console.Title = Title;
    }

    public static class Tracer
    {
        static bool _tracing;

        static Tracer()
        {
            Trace.Listeners.Add(new ConsoleTraceListener());
            if (Trace.Listeners != null && Trace.Listeners.Cast<TraceListener>().Any()) _tracing = true;
        }

        public static void DisableTracing() => _tracing = false;

        public static void WriteLine(params (string, object)[] traces)
        {
            if (!_tracing) return;
            var message = string.Join(", ", traces.Select(s => $"{s.Item1}: {s.Item2}").ToArray());
            Trace.WriteLine(message);
        }

        public static void WriteInfo(params object[] traces) => WriteLine(true, traces);

        public static void WriteDebug(params object[] traces) => WriteLine(_tracing, traces);

        static void WriteLine(bool tracing, params object[] traces)
        {
            if (!tracing) return;
            var message = string.Join(" ", traces);
            Trace.WriteLine(message);
        }

        public static void WriteInfo(Func<string> tracer) => WriteLine(true, tracer);
        public static void WriteDebug(Func<string> tracer) => WriteLine(_tracing, tracer);

        static void WriteLine(bool tracing, Func<string> tracer)
        {
            if (!tracing || tracer is null) return;
            Trace.WriteLine(tracer.Invoke());
        }
    }
}