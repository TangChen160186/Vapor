﻿// <copyright file="OpenGLPipeline.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.OpenGL
{
    using System;
    using System.Numerics;
    using FinalEngine.Rendering.OpenGL.Invocation;
    using FinalEngine.Rendering.OpenGL.Pipeline;
    using FinalEngine.Rendering.Pipeline;

    public class OpenGLPipeline : IPipeline
    {
        private readonly IOpenGLInvoker invoker;

        private IOpenGLShaderProgram? boundProgram;

        public OpenGLPipeline(IOpenGLInvoker invoker)
        {
            this.invoker = invoker ?? throw new ArgumentNullException(nameof(invoker));
        }

        public void SetShaderProgram(IShaderProgram? program)
        {
            if (program == null)
            {
                this.boundProgram = null;
                this.invoker.UseProgram(0);

                return;
            }

            if (program is not IOpenGLShaderProgram glProgram)
            {
                throw new ArgumentException($"The specified {nameof(program)} parameter is not of type {nameof(IOpenGLShaderProgram)}.");
            }

            this.boundProgram = glProgram;
            this.boundProgram.Bind();
        }

        public void SetUniform(string name, int value)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name), $"The specified {nameof(name)} parameter cannot be null.");
            }

            if (!this.TryGetUniformLocation(name, out int location))
            {
                return;
            }

            this.invoker.Uniform1(location, value);
        }

        public void SetUniform(string name, float value)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name), $"The specified {nameof(name)} parameter cannot be null.");
            }

            if (!this.TryGetUniformLocation(name, out int location))
            {
                return;
            }

            this.invoker.Uniform1(location, value);
        }

        public void SetUniform(string name, double value)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name), $"The specified {nameof(name)} parameter cannot be null.");
            }

            if (!this.TryGetUniformLocation(name, out int location))
            {
                return;
            }

            this.invoker.Uniform1(location, value);
        }

        public void SetUniform(string name, bool value)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name), $"The specified {nameof(name)} parameter cannot be null.");
            }

            if (!this.TryGetUniformLocation(name, out int location))
            {
                return;
            }

            this.invoker.Uniform1(location, value ? 1 : 0);
        }

        public void SetUniform(string name, Vector2 value)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name), $"The specified {nameof(name)} parameter cannot be null.");
            }

            if (!this.TryGetUniformLocation(name, out int location))
            {
                return;
            }

            this.invoker.Uniform2(location, value.X, value.Y);
        }

        public void SetUniform(string name, Vector3 value)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name), $"The specified {nameof(name)} parameter cannot be null.");
            }

            if (!this.TryGetUniformLocation(name, out int location))
            {
                return;
            }

            this.invoker.Uniform3(location, value.X, value.Y, value.Z);
        }

        public void SetUniform(string name, Vector4 value)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name), $"The specified {nameof(name)} parameter cannot be null.");
            }

            if (!this.TryGetUniformLocation(name, out int location))
            {
                return;
            }

            this.invoker.Uniform4(location, value.X, value.Y, value.Z, value.W);
        }

        public void SetUniform(string name, Matrix4x4 value)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name), $"The specified {nameof(name)} parameter cannot be null.");
            }

            if (!this.TryGetUniformLocation(name, out int location))
            {
                return;
            }

            float[] values =
            {
                value.M11, value.M12, value.M13, value.M14,
                value.M21, value.M22, value.M23, value.M24,
                value.M31, value.M32, value.M33, value.M34,
                value.M41, value.M42, value.M43, value.M44,
            };

            this.invoker.UniformMatrix4(location, 1, false, values);
        }

        private bool TryGetUniformLocation(string name, out int location)
        {
            if (this.boundProgram == null)
            {
                location = 0;
                return false;
            }

            location = this.boundProgram.GetUniformLocation(name);

            if (location == -1)
            {
                // TODO: Use appropriate logging system.
                throw new ArgumentException($"The specified uniform, {name} couldn't be located.", nameof(name));
            }

            return true;
        }
    }
}