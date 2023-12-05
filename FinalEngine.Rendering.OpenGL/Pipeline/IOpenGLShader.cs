// <copyright file="IOpenGLShader.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.OpenGL.Pipeline;

using FinalEngine.Rendering.Pipeline;

public interface IOpenGLShader : IShader
{
    void Attach(int program);
}
