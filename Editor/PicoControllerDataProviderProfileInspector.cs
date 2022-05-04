// Copyright (c) Reality Collective. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using RealityToolkit.Pico.InputSystem.Profiles;
using UnityEditor;
using RealityToolkit.Editor.Profiles.InputSystem.Controllers;

namespace RealityToolkit.Pico.Editor
{
    /// <summary>
    /// Default inspector for <see cref="PicoControllerDataProviderProfile"/>.
    /// </summary>
    [CustomEditor(typeof(PicoControllerDataProviderProfile))]
    public class PicoControllerDataProviderProfileInspector : BaseMixedRealityControllerDataProviderProfileInspector { }
}