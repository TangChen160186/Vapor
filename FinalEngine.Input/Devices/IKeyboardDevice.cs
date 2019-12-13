﻿namespace FinalEngine.Input.Devices
{
    using System;
    using FinalEngine.Input.Events;

    /// <summary>
    ///   Defines an interface that represents a physical or virtual keyboard device.
    /// </summary>
    public interface IKeyboardDevice
    {
        /// <summary>
        ///   Occurs when a key of this <see cref="IKeyboardDevice"/> has been pressed.
        /// </summary>
        event EventHandler<KeyEventArgs> KeyPressed;

        /// <summary>
        ///   Occurs when a key of this <see cref="IKeyboardDevice"/> has been released.
        /// </summary>
        event EventHandler<KeyEventArgs> KeyReleased;
    }
}