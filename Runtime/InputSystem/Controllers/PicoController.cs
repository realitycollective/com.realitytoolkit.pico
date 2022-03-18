// Copyright (c) Reality Collective. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using XRTK.Definitions.Controllers;
using XRTK.Definitions.Devices;
using XRTK.Definitions.Utilities;
using XRTK.Extensions;
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

        /// <summary>
        /// This dictionary contains <see cref="AxisType.Digital"/> mappings to their respective <see cref="InputFeatureUsage"/> equivalent
        /// used to read the buttons state.
        /// </summary>
        protected virtual IReadOnlyDictionary<string, InputFeatureUsage<bool>> DigitalInputFeatureUsageMap { get; set; } = new Dictionary<string, InputFeatureUsage<bool>>();

        /// <summary>
        /// This dictionary contains <see cref="AxisType.SingleAxis"/> mappings to their respective <see cref="InputFeatureUsage"/> equivalent
        /// used to read the buttons state.
        /// </summary>
        protected virtual IReadOnlyDictionary<string, InputFeatureUsage<float>> SingleAxisInputFeatureUsageMap { get; set; } = new Dictionary<string, InputFeatureUsage<float>>();

        /// <summary>
        /// This dictionary contains <see cref="AxisType.DualAxis"/> mappings to their respective <see cref="InputFeatureUsage"/> equivalent
        /// used to read the buttons state.
        /// </summary>
        protected virtual IReadOnlyDictionary<string, InputFeatureUsage<Vector2>> DualAxisInputFeatureUsageMap { get; set; } = new Dictionary<string, InputFeatureUsage<Vector2>>();

        /// <summary>
        /// Updates the controller's <see cref="DeviceInputType.ButtonPress"/> mappings.
        /// </summary>
        /// <param name="interactionMapping">The <see cref="MixedRealityInteractionMapping"/> to update.</param>
        /// <param name="inputDevice">The <see cref="InputDevice"/> data is read from.</param>
        protected virtual void UpdateDigitalInteractionMapping(MixedRealityInteractionMapping interactionMapping, InputDevice inputDevice)
        {
            Debug.Assert(interactionMapping.AxisType == AxisType.Digital);

            if (!DigitalInputFeatureUsageMap.ContainsKey(interactionMapping.InputName))
            {
                Debug.LogError($"Interaction mapping {interactionMapping.InputName} is not handled for controller {GetType().Name} - {ControllerHandedness}.");
                return;
            }

            interactionMapping.BoolData = inputDevice.TryGetFeatureValue(DigitalInputFeatureUsageMap[interactionMapping.InputName], out bool value) && value;
        }

        /// <summary>
        /// Updates the controller's <see cref="DeviceInputType.ThumbStick"/> mappings.
        /// </summary>
        /// <param name="interactionMapping">The <see cref="MixedRealityInteractionMapping"/> to update.</param>
        /// <param name="inputDevice">The <see cref="InputDevice"/> data is read from.</param>
        protected virtual void UpdateSingleAxisInteractionMapping(MixedRealityInteractionMapping interactionMapping, InputDevice inputDevice)
        {
            Debug.Assert(interactionMapping.AxisType == AxisType.SingleAxis);

            if (!SingleAxisInputFeatureUsageMap.ContainsKey(interactionMapping.InputName))
            {
                Debug.LogError($"Interaction mapping {interactionMapping.InputName} is not handled for controller {GetType().Name} - {ControllerHandedness}.");
                return;
            }

            if (inputDevice.TryGetFeatureValue(SingleAxisInputFeatureUsageMap[interactionMapping.InputName], out float value))
            {
                interactionMapping.FloatData = value;
            }
        }

        /// <summary>
        /// Updates the controller's <see cref="DeviceInputType.ThumbStick"/> mappings.
        /// </summary>
        /// <param name="interactionMapping">The <see cref="MixedRealityInteractionMapping"/> to update.</param>
        /// <param name="inputDevice">The <see cref="InputDevice"/> data is read from.</param>
        protected virtual void UpdateDualAxisInteractionMapping(MixedRealityInteractionMapping interactionMapping, InputDevice inputDevice)
        {
            Debug.Assert(interactionMapping.AxisType == AxisType.DualAxis);

            if (!DualAxisInputFeatureUsageMap.ContainsKey(interactionMapping.InputName))
            {
                Debug.LogError($"Interaction mapping {interactionMapping.InputName} is not handled for controller {GetType().Name} - {ControllerHandedness}.");
                return;
            }

            if (inputDevice.TryGetFeatureValue(DualAxisInputFeatureUsageMap[interactionMapping.InputName], out Vector2 value))
            {
                interactionMapping.Vector2Data = value;
            }
        }

        /// <summary>
        /// Reads controller input and updates mappings.
        /// </summary>
        protected override void UpdateInteractionMappings()
        {
            Debug.Assert(Interactions != null && Interactions.Length > 0, $"Interaction mappings must be defined for {GetType().Name} - {ControllerHandedness}.");

            if (!TryGetInputDevice(out var inputDevice))
            {
                Debug.LogError($"Cannot find input device for {GetType().Name} - {ControllerHandedness}");
                return;
            }

            for (var i = 0; i < Interactions.Length; i++)
            {
                var interactionMapping = Interactions[i];
                switch (interactionMapping.InputType)
                {
                    case DeviceInputType.Trigger:
                        UpdateSingleAxisInteractionMapping(interactionMapping, inputDevice);
                        break;
                    case DeviceInputType.ButtonPress:
                        UpdateDigitalInteractionMapping(interactionMapping, inputDevice);
                        break;
                    case DeviceInputType.ThumbStick:
                        UpdateDualAxisInteractionMapping(interactionMapping, inputDevice);
                        break;
                    case DeviceInputType.SpatialPointer:
                        UpdateSpatialPointer(interactionMapping);
                        break;
                    default:
                        Debug.LogError($"Input {interactionMapping.InputType} is not handled for controller {GetType().Name} - {ControllerHandedness}.");
                        break;
                }

                interactionMapping.RaiseInputAction(InputSource, ControllerHandedness);
            }
        }
    }
}
