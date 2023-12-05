// <copyright file="OpenGLRenderContext.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.OpenGL;

using System;
using System.Diagnostics.CodeAnalysis;
using FinalEngine.Rendering.Exceptions;
using FinalEngine.Rendering.OpenGL.Invocation;
using OpenTK;
using OpenTK.Windowing.Common;

public class OpenGLRenderContext : IRenderContext
{
    private readonly IGraphicsContext context;

    private readonly IOpenGLInvoker invoker;

    [ExcludeFromCodeCoverage]
    public OpenGLRenderContext(IBindingsContext bindings, IGraphicsContext context)
        : this(new OpenGLInvoker(), bindings, context)
    {
    }

    public OpenGLRenderContext(IOpenGLInvoker invoker, IBindingsContext bindings, IGraphicsContext context)
    {
        ArgumentNullException.ThrowIfNull(bindings, nameof(bindings));

        this.invoker = invoker ?? throw new ArgumentNullException(nameof(invoker));
        this.context = context ?? throw new ArgumentNullException(nameof(context));

        context.MakeCurrent();
        invoker.LoadBindings(bindings);
    }

    public void SwapBuffers()
    {
        if (!this.context.IsCurrent)
        {
            throw new RenderContextException($"This {nameof(OpenGLRenderContext)} is not current on the calling thread.");
        }

        this.context.SwapBuffers();
    }
}
