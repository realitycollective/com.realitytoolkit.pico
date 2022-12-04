// Copyright (c) Reality Collective. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using RealityCollective.Definitions.Utilities;
using RealityToolkit.Definitions.Controllers;
using RealityToolkit.Definitions.Devices;
using RealityToolkit.InputSystem.Interfaces.Modules;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

namespace RealityToolkit.Pico.InputService
{
    [System.Runtime.InteropServices.Guid("7cd3ce74-425f-40b2-944a-7d575da7ec74")]
    public class Pico4Controller : PicoController
    {
        /// <inheritdoc />
        public Pico4Controller() { }

        /// <inheritdoc />
        public Pico4Controller(IMixedRealityControllerServiceModule controllerServiceModule, TrackingState trackingState, Handedness controllerHandedness, MixedRealityControllerMappingProfile controllerMappingProfile)
            : base(controllerServiceModule, trackingState, controllerHandedness, controllerMappingProfile) { }

        private const string menuButtonInputName = "Menu";
        private const string triggerInputName = "Trigger";
        private const string triggerPressInputName = "Trigger Press";
        private const string gripInputName = "Grip";
        private const string gripPressInputName = "Grip Press";
        private const string thumbstickInputName = "Thumbstick";
        private const string thumbstickPressInputName = "Thumbstick Press";
        private const string spatialPointerPoseInputName = "Spatial Pointer Pose";
        private const string xAButtonInputName = "X/A";
        private const string yBButtonInputName = "Y/B";

        /// <inheritdoc />
        public override MixedRealityInteractionMapping[] DefaultInteractions => new[]
        {
            new MixedRealityInteractionMapping(spatialPointerPoseInputName, AxisType.SixDof, spatialPointerPoseInputName, DeviceInputType.SpatialPointer),
            new MixedRealityInteractionMapping(menuButtonInputName, AxisType.Digital, menuButtonInputName, DeviceInputType.ButtonPress),
            new MixedRealityInteractionMapping(triggerInputName, AxisType.SingleAxis, triggerInputName, DeviceInputType.Trigger),
            new MixedRealityInteractionMapping(triggerPressInputName, AxisType.Digital, triggerPressInputName, DeviceInputType.ButtonPress),
            new MixedRealityInteractionMapping(gripInputName, AxisType.SingleAxis, gripInputName, DeviceInputType.Trigger),
            new MixedRealityInteractionMapping(gripPressInputName, AxisType.Digital, gripPressInputName, DeviceInputType.ButtonPress),
            new MixedRealityInteractionMapping(xAButtonInputName, AxisType.Digital, xAButtonInputName, DeviceInputType.ButtonPress),
            new MixedRealityInteractionMapping(yBButtonInputName, AxisType.Digital, yBButtonInputName, DeviceInputType.ButtonPress),
            new MixedRealityInteractionMapping(thumbstickInputName, AxisType.DualAxis, thumbstickInputName, DeviceInputType.ThumbStick),
            new MixedRealityInteractionMapping(thumbstickPressInputName, AxisType.Digital, thumbstickPressInputName, DeviceInputType.ButtonPress)
        };

        /// <inheritdoc />
        protected override IReadOnlyDictionary<string, InputFeatureUsage<bool>> DigitalInputFeatureUsageMap { get; set; } = new Dictionary<string, InputFeatureUsage<bool>>
        {
            { menuButtonInputName, CommonUsages.menuButton },
            { triggerPressInputName, CommonUsages.triggerButton },
            { gripPressInputName, CommonUsages.gripButton },
            { thumbstickPressInputName, CommonUsages.primary2DAxisClick },
            { xAButtonInputName, CommonUsages.primaryButton },
            { yBButtonInputName, CommonUsages.secondaryButton }
        };

        /// <inheritdoc />
        protected override IReadOnlyDictionary<string, InputFeatureUsage<float>> SingleAxisInputFeatureUsageMap { get; set; } = new Dictionary<string, InputFeatureUsage<float>>
        {
            { triggerInputName, CommonUsages.trigger },
            { gripInputName, CommonUsages.grip }
        };

        /// <inheritdoc />
        protected override IReadOnlyDictionary<string, InputFeatureUsage<Vector2>> DualAxisInputFeatureUsageMap { get; set; } = new Dictionary<string, InputFeatureUsage<Vector2>>
        {
            { thumbstickInputName, CommonUsages.primary2DAxis }
        };
    }
}
