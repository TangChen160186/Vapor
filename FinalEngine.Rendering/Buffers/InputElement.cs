﻿// <copyright file="InputElement.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Buffers
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    [SuppressMessage("Naming", "CA1720:Identifier contains type name", Justification = "Required for API")]
    public enum InputElementType
    {
        Int,

        Byte,

        Short,

        Float,

        Double,
    }

    public struct InputElement : IEquatable<InputElement>
    {
        public InputElement(int index, int size, InputElementType type, int relativeOffset)
        {
            this.Index = index;
            this.Size = size;
            this.Type = type;
            this.RelativeOffset = relativeOffset;
        }

        public int Index { get; set; }

        public int RelativeOffset { get; set; }

        public int Size { get; set; }

        public InputElementType Type { get; set; }

        public static bool operator !=(InputElement left, InputElement right)
        {
            return !(left == right);
        }

        public static bool operator ==(InputElement left, InputElement right)
        {
            return left.Equals(right);
        }

        public bool Equals(InputElement other)
        {
            return this.Index == other.Index &&
                   this.RelativeOffset == other.RelativeOffset &&
                   this.Size == other.Size &&
                   this.Type == other.Type;
        }

        public override bool Equals(object? obj)
        {
            return obj is InputElement description && this.Equals(description);
        }

        public override int GetHashCode()
        {
            const int Accumulator = 17;

            return (this.Index.GetHashCode() * Accumulator) +
                   (this.RelativeOffset.GetHashCode() * Accumulator) +
                   (this.Size.GetHashCode() * Accumulator) +
                   (this.Type.GetHashCode() * Accumulator);
        }
    }
}