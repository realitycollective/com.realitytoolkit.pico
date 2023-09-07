// Copyright (c) Reality Collective. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using RealityToolkit.Input.Controllers;
using Unity.XR.PXR;
using UnityEngine;

namespace RealityToolkit.Pico.Input
{
    /// <summary>
    /// Visualizers Pico controllers using the integration package's prefabs.
    /// </summary>
    [System.Runtime.InteropServices.Guid("ad2f586e-edc9-4876-b70d-a20c7038d73c")]
    [RequireComponent(typeof(PXR_ControllerLoader))]
    public class PicoControllerVisualizer : BaseControllerVisualizer { }
}
