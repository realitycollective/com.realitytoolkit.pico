// Copyright (c) Reality Collective. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using RealityToolkit.Editor.Profiles.InputSystem.Controllers;
using RealityToolkit.Pico.InputService.Profiles;
using UnityEditor;

namespace RealityToolkit.Pico.Editor
{
    /// <summary>
    /// Default inspector for <see cref="PicoControllerServiceModuleProfile"/>.
    /// </summary>
    [CustomEditor(typeof(PicoControllerServiceModuleProfile))]
    public class PicoControllerServiceModuleProfileInspector : BaseMixedRealityControllerServiceModuleProfileInspector { }
}