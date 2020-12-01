﻿// <copyright file="IShader.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Pipeline
{
    using System;

    public enum PipelineTarget
    {
        None,

        Vertex,

        Fragment,
    }

    public interface IShader : IDisposable
    {
        PipelineTarget EntryPoint { get; }
    }
}