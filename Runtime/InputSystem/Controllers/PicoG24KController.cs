// Copyright (c) Reality Collective. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using XRTK.Definitions.Controllers;
using XRTK.Definitions.Devices;
using XRTK.Definitions.Utilities;
using XRTK.Interfaces.Providers.Controllers;

namespace RealityToolkit.Pico.InputSystem.Controllers
{
    [System.Runtime.InteropServices.Guid("07b23b0b-1be1-4ac5-942e-0fe3bc65a260")]
    public class PicoG24KController : PicoController
    {
        /// <inheritdoc />
        public PicoG24KController() { }

        /// <inheritdoc />
        public PicoG24KController(IMixedRealityControllerDataProvider controllerDataProvider, TrackingState trackingState, Handedness controllerHandedness, MixedRealityControllerMappingProfile controllerMappingProfile)
            : base(controllerDataProvider, trackingState, controllerHandedness, controllerMappingProfile) { }

        private const string menuButtonInputName = "Menu";
        private const string triggerInputName = "Trigger";
        private const string triggerPressInputName = "Trigger Press";
        private const string touchpadInputName = "Touchpad";
        private const string touchpadPressInputName = "Touchpad Press";
        private const string spatialPointerPoseInputName = "Spatial Pointer Pose";

        /// <inheritdoc />
        public override MixedRealityInteractionMapping[] DefaultInteractions => new[]
        {
            new MixedRealityInteractionMapping(spatialPointerPoseInputName, AxisType.SixDof, spatialPointerPoseInputName, DeviceInputType.SpatialPointer),
            new MixedRealityInteractionMapping(menuButtonInputName, AxisType.Digital, menuButtonInputName, DeviceInputType.ButtonPress),
            new MixedRealityInteractionMapping(triggerInputName, AxisType.SingleAxis, triggerInputName, DeviceInputType.Trigger),
            new MixedRealityInteractionMapping(triggerPressInputName, AxisType.Digital, triggerPressInputName, DeviceInputType.ButtonPress),
            new MixedRealityInteractionMapping(touchpadInputName, AxisType.DualAxis, touchpadInputName, DeviceInputType.ThumbStick),
            new MixedRealityInteractionMapping(touchpadPressInputName, AxisType.Digital, touchpadPressInputName, DeviceInputType.ButtonPress)
        };

        /// <inheritdoc />
        protected override IReadOnlyDictionary<string, InputFeatureUsage<bool>> DigitalInputFeatureUsageMap { get; set; } = new Dictionary<string, InputFeatureUsage<bool>>
        {
            { menuButtonInputName, CommonUsages.menuButton },
            { triggerPressInputName, CommonUsages.triggerButton },
            { touchpadPressInputName, CommonUsages.primary2DAxisClick }
        };

        /// <inheritdoc />
        protected override IReadOnlyDictionary<string, InputFeatureUsage<float>> SingleAxisInputFeatureUsageMap { get; set; } = new Dictionary<string, InputFeatureUsage<float>>
        {
            { triggerInputName, CommonUsages.trigger }
        };

        /// <inheritdoc />
        protected override IReadOnlyDictionary<string, InputFeatureUsage<Vector2>> DualAxisInputFeatureUsageMap { get; set; } = new Dictionary<string, InputFeatureUsage<Vector2>>
        {
            { touchpadInputName, CommonUsages.primary2DAxis }
        };
    }
}