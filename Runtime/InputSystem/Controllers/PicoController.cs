// Copyright (c) Reality Collective. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using RealityToolkit.Services.InputSystem.Controllers.UnityXR;
using RealityToolkit.Definitions.Controllers;
using RealityToolkit.Definitions.Devices;
using RealityToolkit.Definitions.Utilities;
using RealityToolkit.Interfaces.InputSystem.Providers.Controllers;

namespace RealityToolkit.Pico.InputSystem.Controllers
{
    /// <summary>
    /// Base implementation for controllers on the <see cref="PicoPlatform"/>.
    /// </summary>
    public abstract class PicoController : UnityXRController
    {
        /// <inheritdoc />
        public PicoController() { }

        /// <inheritdoc />
        public PicoController(IMixedRealityControllerDataProvider controllerDataProvider, TrackingState trackingState, Handedness controllerHandedness, MixedRealityControllerMappingProfile controllerMappingProfile)
            : base(controllerDataProvider, trackingState, controllerHandedness, controllerMappingProfile) { }
    }
}
