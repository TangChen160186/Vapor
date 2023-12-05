// <copyright file="OpenGLShader.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.OpenGL.Pipeline;

using System;
using FinalEngine.Rendering.Exceptions;
using FinalEngine.Rendering.OpenGL.Invocation;
using FinalEngine.Rendering.Pipeline;
using FinalEngine.Utilities;
using OpenTK.Graphics.OpenGL4;

public class OpenGLShader : IOpenGLShader, IDisposable
{
    private readonly IOpenGLInvoker invoker;

    private int rendererID;

    public OpenGLShader(IOpenGLInvoker invoker, IEnumMapper mapper, ShaderType type, string sourceCode)
    {
        ArgumentNullException.ThrowIfNull(mapper, nameof(mapper));
        ArgumentException.ThrowIfNullOrWhiteSpace(sourceCode, nameof(sourceCode));

        this.invoker = invoker ?? throw new ArgumentNullException(nameof(invoker));

        this.EntryPoint = mapper.Reverse<PipelineTarget>(type);

        this.rendererID = invoker.CreateShader(type);
        invoker.ShaderSource(this.rendererID, sourceCode);
        invoker.CompileShader(this.rendererID);

        string? log = invoker.GetShaderInfoLog(this.rendererID);

        if (!string.IsNullOrWhiteSpace(log))
        {
            throw new ShaderCompilationErrorException($"The {nameof(OpenGLShader)} failed to compile.", log);
        }
    }

    ~OpenGLShader()
    {
        this.Dispose(false);
    }

    public PipelineTarget EntryPoint { get; }

    protected bool IsDisposed { get; private set; }

    public void Attach(int program)
    {
        ObjectDisposedException.ThrowIf(this.IsDisposed, this);
        this.invoker.AttachShader(program, this.rendererID);
    }

    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (this.IsDisposed)
        {
            return;
        }

        if (disposing && this.rendererID != -1)
        {
            this.invoker.DeleteShader(this.rendererID);
            this.rendererID = -1;
        }

        this.IsDisposed = true;
    }
}
