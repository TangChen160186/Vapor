﻿// <copyright file="OpenTKWindow.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Platform.Desktop.OpenTK
{
    using System;
    using FinalEngine.Platform.Desktop.OpenTK.Invocation;

    /// <summary>
    ///   Provides an OpenTK implementation of an <see cref="IWindow"/> and <see cref="IEventsProcessor"/>.
    /// </summary>
    /// <seealso cref="FinalEngine.Platform.IWindow"/>
    /// <seealso cref="FinalEngine.Platform.IEventsProcessor"/>
    public class OpenTKWindow : IWindow, IEventsProcessor
    {
        /// <summary>
        ///   The native window.
        /// </summary>
        private readonly INativeWindowInvoker nativeWindow;

        /// <summary>
        ///   Initializes a new instance of the <see cref="OpenTKWindow"/> class.
        /// </summary>
        /// <param name="nativeWindow">
        ///   Specifies a <see cref="INativeWindowInvoker"/> that represents the underlying native window to be used.
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        ///   The specified <paramref name="nativeWindow"/> parameter is null.
        /// </exception>
        public OpenTKWindow(INativeWindowInvoker nativeWindow)
        {
            this.nativeWindow = nativeWindow ?? throw new ArgumentNullException(nameof(nativeWindow), $"The specified {nameof(nativeWindow)} parameter cannot be null.");
        }

        /// <summary>
        ///   Finalizes an instance of the <see cref="OpenTKWindow"/> class.
        /// </summary>
        ~OpenTKWindow()
        {
            this.Dispose(false);
        }

        /// <inheritdoc/>
        public bool IsExiting
        {
            get { return this.nativeWindow.IsExiting; }
        }

        /// <inheritdoc/>
        /// <exception cref="System.ObjectDisposedException">
        ///   The underlying native window is dispsoed.
        /// </exception>
        public string Title
        {
            get
            {
                return this.nativeWindow.Title;
            }

            set
            {
                if (this.nativeWindow.IsDisposed)
                {
                    throw new ObjectDisposedException(nameof(this.nativeWindow), $"The underlying {nameof(this.nativeWindow)} has been disposed.");
                }

                this.nativeWindow.Title = value;
            }
        }

        /// <inheritdoc/>
        /// <exception cref="System.ObjectDisposedException">
        ///   The underlying native window is dispsoed.
        /// </exception>
        public bool Visible
        {
            get
            {
                return this.nativeWindow.IsVisible;
            }

            set
            {
                if (this.nativeWindow.IsDisposed)
                {
                    throw new ObjectDisposedException(nameof(this.nativeWindow), $"The underlying {nameof(this.nativeWindow)} has been disposed.");
                }

                this.nativeWindow.IsVisible = value;
            }
        }

        /// <summary>
        ///   Gets a value indicating whether this <see cref="OpenTKWindow"/> is disposed.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this <see cref="OpenTKWindow"/> is disposed; otherwise, <c>false</c>.
        /// </value>
        protected bool IsDisposed { get; private set; }

        /// <inheritdoc/>
        /// <exception cref="System.ObjectDisposedException">
        ///   The underlying native window is dispsoed.
        /// </exception>
        public void Close()
        {
            if (this.nativeWindow.IsDisposed)
            {
                throw new ObjectDisposedException(nameof(this.nativeWindow), $"The underlying {nameof(this.nativeWindow)} has been disposed.");
            }

            this.nativeWindow.Close();
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <inheritdoc/>
        /// <remarks>
        ///   This method is not thread safe and should only be executed on the main thread.
        /// </remarks>
        /// <exception cref="System.ObjectDisposedException">
        ///   The underlying native window is dispsoed.
        /// </exception>
        public void ProcessEvents()
        {
            if (this.nativeWindow.IsDisposed)
            {
                throw new ObjectDisposedException(nameof(this.nativeWindow), $"The underlying {nameof(this.nativeWindow)} has been disposed.");
            }

            this.nativeWindow.ProcessEvents();
        }

        /// <summary>
        ///   Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing">
        ///   <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (this.IsDisposed)
            {
                return;
            }

            if (disposing && !this.nativeWindow.IsDisposed)
            {
                this.nativeWindow.Dispose();
            }

            this.IsDisposed = true;
        }
    }
}