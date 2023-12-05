// <copyright file="StopwatchInvoker.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Runtime.Invocation;

using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage(Justification = "Invocation")]
public class StopwatchInvoker : IStopwatchInvoker
{
    private readonly Stopwatch watch;

    public StopwatchInvoker()
        : this(new Stopwatch())
    {
    }

    public StopwatchInvoker(Stopwatch watch)
    {
        this.watch = watch ?? throw new ArgumentNullException(nameof(watch));
    }

    public TimeSpan Elapsed
    {
        get { return this.watch.Elapsed; }
    }

    public bool IsRunning
    {
        get { return this.watch.IsRunning; }
    }

    public void Restart()
    {
        this.watch.Restart();
    }
}
