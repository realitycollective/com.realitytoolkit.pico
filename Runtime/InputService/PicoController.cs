// Copyright (c) Reality Collective. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using RealityCollective.Definitions.Utilities;
using RealityToolkit.Definitions.Controllers;
using RealityToolkit.Definitions.Devices;
using RealityToolkit.InputSystem.Controllers;
using RealityToolkit.InputSystem.Extensions;
using RealityToolkit.InputSystem.Interfaces.Modules;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

namespace RealityToolkit.Pico.InputService
{
    /// <summary>
    /// Base implementation for controllers on the <see cref="PicoPlatform"/>.
    /// </summary>
    public abstract class PicoController : BaseController
    {
        /// <inheritdoc />
        public PicoController() { }

        /// <inheritdoc />
        public PicoController(IMixedRealityControllerServiceModule controllerServiceModule, TrackingState trackingState, Handedness controllerHandedness, MixedRealityControllerMappingProfile controllerMappingProfile)
            : base(controllerServiceModule, trackingState, controllerHandedness, controllerMappingProfile) { }

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

        ///// <summary>
        ///// The controller's pose in world space.
        ///// </summary>
        //protected MixedRealityPose ControllerPose { get; set; }

        /// <summary>
        /// The controller's pointer pose in world space.
        /// </summary>
        protected Pose SpatialPointerPose { get; set; }

        /// <inheritdoc />
        public override void UpdateController()
        {
            if (!Enabled)
            {
                return;
            }

            UpdateTrackingState();
            UpdateControllerPose();
            UpdateSpatialPointerPose();
            UpdateInteractionMappings();
        }

        /// <summary>
        /// Updates the controller's <see cref="TrackingState"/>.
        /// </summary>
        protected virtual void UpdateTrackingState()
        {
            if (!TryGetInputDevice(out var inputDevice))
            {
                Debug.LogError($"Cannot find input device for {GetType().Name} - {ControllerHandedness}");
                return;
            }

            var currentTrackingState = TrackingState;
            if (inputDevice.TryGetFeatureValue(CommonUsages.isTracked, out var isTracked))
            {
                TrackingState = isTracked ? TrackingState.Tracked : TrackingState.NotTracked;
            }

            if (TrackingState != currentTrackingState)
            {
                InputSystem?.RaiseSourceTrackingStateChanged(InputSource, this, TrackingState);
            }
        }

        /// <summary>
        /// Updates the controller's pose.
        /// </summary>
        protected virtual void UpdateControllerPose()
        {
            if (TrackingState != TrackingState.Tracked)
            {
                IsPositionAvailable = false;
                IsPositionApproximate = false;
                IsRotationAvailable = false;
                return;
            }

            if (!TryGetInputDevice(out var inputDevice))
            {
                Debug.LogError($"Cannot find input device for {GetType().Name} - {ControllerHandedness}");
                return;
            }

            IsPositionAvailable = inputDevice.TryGetFeatureValue(CommonUsages.devicePosition, out var position);
            IsRotationAvailable = inputDevice.TryGetFeatureValue(CommonUsages.deviceRotation, out var rotation);
            IsPositionApproximate = false;

            var updatedControllerPose = new Pose(position, rotation);
            if (updatedControllerPose != Pose)
            {
                Pose = updatedControllerPose;
                InputSystem?.RaiseSourcePoseChanged(InputSource, this, Pose);
            }
        }

        /// <summary>
        /// Updates the controller's spatial pointer pose.
        /// </summary>
        protected virtual void UpdateSpatialPointerPose()
        {
            SpatialPointerPose = Pose;
        }

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
        /// Updates the spatial pointer pose interaction mapping value.
        /// </summary>
        /// <param name="interactionMapping">The spatial pointer pose mapping.</param>
        protected void UpdateSpatialPointer(MixedRealityInteractionMapping interactionMapping)
        {
            Debug.Assert(interactionMapping.AxisType == AxisType.SixDof);
            interactionMapping.PoseData = SpatialPointerPose;
        }

        /// <summary>
        /// Reads controller input and updates mappings.
        /// </summary>
        protected virtual void UpdateInteractionMappings()
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

        /// <summary>
        /// Gets the input device for this controller.
        /// </summary>
        /// <param name="inputDevice"><see cref="InputDevice"/> providing controller data.</param>
        /// <returns><c>True</c>, if device found.</returns>
        protected bool TryGetInputDevice(out InputDevice inputDevice)
        {
            switch (ControllerHandedness)
            {
                case Handedness.Left:
                    inputDevice = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
                    return inputDevice != default;
                case Handedness.Right:
                    inputDevice = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
                    return inputDevice != default;
                case Handedness.None:
                case Handedness.Both:
                case Handedness.Other:
                case Handedness.Any:
                default:
                    inputDevice = default;
                    return false;
            }
        }
    }
}
