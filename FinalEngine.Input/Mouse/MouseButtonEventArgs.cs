﻿// <copyright file="MouseButtonEventArgs.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Input.Mouse
{
    using System;

    public class MouseButtonEventArgs : EventArgs
    {
        public MouseButton Button { get; init; }
    }
}