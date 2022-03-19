// Copyright (c) Reality Collective. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using XRTK.Definitions.Controllers;
using XRTK.Definitions.Devices;
using XRTK.Definitions.Utilities;
using XRTK.Interfaces.InputSystem.Providers.Controllers;
using XRTK.Services.InputSystem.Controllers.UnityXR;

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
