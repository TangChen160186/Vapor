﻿// <copyright file="MouseMoveEventArgs.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Input.Mouse
{
    using System;
    using System.Drawing;

    public class MouseMoveEventArgs : EventArgs
    {
        public PointF Location { get; init; }
    }
}