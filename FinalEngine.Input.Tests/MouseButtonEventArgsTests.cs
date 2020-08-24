﻿// <copyright file="MouseButtonEventArgsTests.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Input.Tests
{
    using System;
    using FinalEngine.Input.Mouse;
    using NUnit.Framework;

    public sealed class MouseButtonEventArgsTests
    {
        [Test]
        public void MouseButton_Property_Test()
        {
            // Arrange
            Array values = Enum.GetValues(typeof(MouseButton));
            var expected = (MouseButton)values.GetValue(new Random().Next(values.Length));

            var mouseButtonEventArgs = new MouseButtonEventArgs(expected);

            // Act
            MouseButton actual = mouseButtonEventArgs.Button;

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}