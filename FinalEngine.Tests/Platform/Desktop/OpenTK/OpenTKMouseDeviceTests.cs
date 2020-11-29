﻿// <copyright file="OpenTKMouseDeviceTests.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Platform.Desktop.OpenTK
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Drawing;
    using FinalEngine.Input.Mouse;
    using FinalEngine.Platform.Desktop.OpenTK;
    using FinalEngine.Platform.Desktop.OpenTK.Invocation;
    using global::OpenTK.Mathematics;
    using Moq;
    using NUnit.Framework;
    using TKInputAction = global::OpenTK.Windowing.GraphicsLibraryFramework.InputAction;
    using TKKeyModifiers = global::OpenTK.Windowing.GraphicsLibraryFramework.KeyModifiers;
    using TKMouseButton = global::OpenTK.Windowing.GraphicsLibraryFramework.MouseButton;
    using TKMouseButtonEventArgs = global::OpenTK.Windowing.Common.MouseButtonEventArgs;
    using TKMouseMoveEventArgs = global::OpenTK.Windowing.Common.MouseMoveEventArgs;
    using TKMouseWheelEventArgs = global::OpenTK.Windowing.Common.MouseWheelEventArgs;

    [ExcludeFromCodeCoverage]
    public class OpenTKMouseDeviceTests
    {
        private OpenTKMouseDevice mouseDevice;

        private Mock<INativeWindowInvoker> nativeWindow;

        [Test]
        public void ConstructorShouldNotThrowExceptionWhenNativeWindowIsNotNull()
        {
            // Arrange, act and assert
            Assert.DoesNotThrow(() => new OpenTKMouseDevice(new Mock<INativeWindowInvoker>().Object));
        }

        [Test]
        public void ConstructorShouldThrowArgumentNullExceptionWhenNativeWindowIsNull()
        {
            // Arrange, act and assert
            Assert.Throws<ArgumentNullException>(() => new OpenTKMouseDevice(null));
        }

        [Test]
        public void ConstructorTestShouldHookOntoNativeWindowMouseDownEventWhenNativeWindowIsNotNull()
        {
            // Assert
            this.nativeWindow.VerifyAdd(x => x.MouseDown += It.IsAny<Action<TKMouseButtonEventArgs>>(), Times.Once);
        }

        [Test]
        public void ConstructorTestShouldHookOntoNativeWindowMouseMoveEventWhenNativeWindowIsNotNull()
        {
            // Assert
            this.nativeWindow.VerifyAdd(x => x.MouseMove += It.IsAny<Action<TKMouseMoveEventArgs>>(), Times.Once);
        }

        [Test]
        public void ConstructorTestShouldHookOntoNativeWindowMouseUpEventWhenNativeWindowIsNotNull()
        {
            // Assert
            this.nativeWindow.VerifyAdd(x => x.MouseUp += It.IsAny<Action<TKMouseButtonEventArgs>>(), Times.Once);
        }

        [Test]
        public void ConstructorTestShouldHookOntoNativeWindowMouseWheelEventWhenNativeWindowIsNotNull()
        {
            // Assert
            this.nativeWindow.VerifyAdd(x => x.MouseWheel += It.IsAny<Action<TKMouseWheelEventArgs>>(), Times.Once);
        }

        [Test]
        public void NativeWindowMouseDownEventShouldRaiseButtonDownWhenRaised()
        {
            // Assert
            this.mouseDevice.ButtonDown += (s, e) =>
            {
                Assert.AreSame(this.mouseDevice, s);
                Assert.AreEqual(MouseButton.Button4, e.Button);
            };

            // Act
            this.nativeWindow.Raise(x => x.MouseDown += null, new TKMouseButtonEventArgs(TKMouseButton.Button4, TKInputAction.Press, TKKeyModifiers.CapsLock));
        }

        [Test]
        public void NativeWindowMouseMoveEventShouldRaiseMoveWhenRaised()
        {
            // Assert
            this.mouseDevice.Move += (s, e) =>
            {
                Assert.AreSame(this.mouseDevice, s);
                Assert.AreEqual(new PointF(10, 20), e.Location);
            };

            // Act
            this.nativeWindow.Raise(x => x.MouseMove += null, new TKMouseMoveEventArgs(10, 20, 0, 0));
        }

        [Test]
        public void NativeWindowMouseUpEventShouldRaiseButtonUpWhenRaised()
        {
            this.mouseDevice.ButtonUp += (s, e) =>
            {
                // Assert
                Assert.AreSame(this.mouseDevice, s);
                Assert.AreEqual(MouseButton.Button6, e.Button);
            };

            // Act
            this.nativeWindow.Raise(x => x.MouseUp += null, new TKMouseButtonEventArgs(TKMouseButton.Button6, TKInputAction.Press, TKKeyModifiers.CapsLock));
        }

        [Test]
        public void NativeWindowMouseWheelEventShouldRaiseScrollWhenRaised()
        {
            // Assert
            this.mouseDevice.Scroll += (s, e) =>
            {
                Assert.AreSame(this.mouseDevice, s);
                Assert.AreEqual(10, e.Offset);
            };

            // Act
            this.nativeWindow.Raise(x => x.MouseWheel += null, new TKMouseWheelEventArgs(0, 10));
        }

        [Test]
        public void SetCursorLocationShouldSetNativeWindowMousePosition()
        {
            // Act
            this.mouseDevice.SetCursorLocation(new PointF(100, 3400));

            // Assert
            this.nativeWindow.VerifySet(x => x.MousePosition = new Vector2(100, 3400), Times.Once);
        }

        [SetUp]
        public void Setup()
        {
            // Arrange
            this.nativeWindow = new Mock<INativeWindowInvoker>();
            this.mouseDevice = new OpenTKMouseDevice(this.nativeWindow.Object);
        }
    }
}