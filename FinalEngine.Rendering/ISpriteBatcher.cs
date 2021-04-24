﻿// <copyright file="ISpriteBatcher.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering
{
    using System.Drawing;
    using System.Numerics;
    using FinalEngine.Rendering.Buffers;

    public interface ISpriteBatcher
    {
        int CurrentIndexCount { get; }

        int CurrentVertexCount { get; }

        int MaxIndexCount { get; }

        int MaxVertexCount { get; }

        bool ShouldReset { get; }

        void Batch(float textureSlotIndex, Color color, Vector2 origin, Vector2 position, float rotation, Vector2 scale);

        void Reset();

        void UpdateBatch(IVertexBuffer vertexBuffer);
    }
}