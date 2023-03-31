// Copyright (c) Reality Collective. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using RealityCollective.Definitions.Utilities;
using RealityToolkit.Definitions.Controllers;
using RealityToolkit.Definitions.Devices;
using RealityToolkit.InputSystem.Extensions;
using RealityToolkit.InputSystem.Interfaces.Modules;
using UnityEngine;

namespace RealityToolkit.Pico.InputService
{
    /// <summary>
    /// The <see cref="PicoAllInOneHeadsetButtonsController"/> is located on the Pico headset itself
    /// and has only two buttons. One for selecting and one for going back. It's a common feature of all
    /// Pico devices and operates using the user's gaze.
    /// </summary>
    [System.Runtime.InteropServices.Guid("f661ead7-06fc-422b-918c-84c711bf9b6b")]
    public class PicoAllInOneHeadsetButtonsController : PicoController
    {
        /// <inheritdoc />
        public PicoAllInOneHeadsetButtonsController() { }

        /// <inheritdoc />
        public PicoAllInOneHeadsetButtonsController(IMixedRealityControllerServiceModule controllerDataProvider, TrackingState trackingState, Handedness controllerHandedness, MixedRealityControllerMappingProfile controllerMappingProfile)
            : base(controllerDataProvider, trackingState, controllerHandedness, controllerMappingProfile) { }

        private const string triggerButtonInputName = "Trigger";
        private const string menuButtonInputName = "Menu";

        /// <inheritdoc />
        public override MixedRealityInteractionMapping[] DefaultInteractions => new[]
        {
            new MixedRealityInteractionMapping(triggerButtonInputName, AxisType.Digital, triggerButtonInputName, DeviceInputType.ButtonPress),
            new MixedRealityInteractionMapping(menuButtonInputName, AxisType.Digital, menuButtonInputName, DeviceInputType.ButtonPress)
        };

        /// <inheritdoc />
        protected override void UpdateTrackingState()
        {
            if (TrackingState != TrackingState.NotApplicable)
            {
                TrackingState = TrackingState.NotApplicable;
                InputSystem?.RaiseSourceTrackingStateChanged(InputSource, this, TrackingState);
            }
        }

        /// <inheritdoc />
        protected override void UpdateInteractionMappings()
        {
            Debug.Assert(Interactions != null && Interactions.Length > 0, $"Interaction mappings must be defined for {GetType().Name} - {ControllerHandedness}.");

            for (var i = 0; i < Interactions.Length; i++)
            {
                var interactionMapping = Interactions[i];
                switch (interactionMapping.InputType)
                {
                    case DeviceInputType.ButtonPress:
                        UpdateButtonPress(interactionMapping);
                        break;
                    default:
                        Debug.LogError($"Input {interactionMapping.InputType} is not handled for controller {GetType().Name} - {ControllerHandedness}.");
                        break;
                }

                interactionMapping.RaiseInputAction(InputSource, ControllerHandedness);
            }
        }

        private void UpdateButtonPress(MixedRealityInteractionMapping interactionMapping)
        {
            Debug.Assert(interactionMapping.AxisType == AxisType.Digital);

            if (interactionMapping.InputName.Equals(triggerButtonInputName))
            {
                interactionMapping.BoolData = Input.GetKey(KeyCode.JoystickButton0);
            }
            else if (interactionMapping.InputName.Equals(menuButtonInputName))
            {
                interactionMapping.BoolData = Input.GetKey(KeyCode.Escape);
            }
        }
    }
}