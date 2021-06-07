﻿// <copyright file="IReadOnlyEntity.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.ECS
{
    using System;

    public interface IReadOnlyEntity
    {
        bool ContainsComponent(IComponent component);

        bool ContainsComponent(Type type);

        bool ContainsComponent<TComponent>()
            where TComponent : IComponent;
    }
}