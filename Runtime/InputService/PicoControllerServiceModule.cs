// Copyright (c) Reality Collective. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using RealityCollective.Definitions.Utilities;
using RealityCollective.ServiceFramework.Attributes;
using RealityToolkit.Definitions.Devices;
using RealityToolkit.InputSystem.Controllers;
using RealityToolkit.InputSystem.Interfaces;
using RealityToolkit.Pico.InputService.Profiles;
using System;
using System.Collections.Generic;
using Unity.XR.PXR;
using UnityEngine;

namespace RealityToolkit.Pico.InputService
{
    /// <summary>
    /// Manages active controllers when running on the <see cref="PicoPlatform"/>.
    /// </summary>
    [RuntimePlatform(typeof(PicoPlatform))]
    [System.Runtime.InteropServices.Guid("ab929180-f710-4f29-966b-08be77135020")]
    public class PicoControllerServiceModule : BaseControllerServiceModule, IPicoControllerServiceModule
    {
        /// <inheritdoc />
        public PicoControllerServiceModule(string name, uint priority, PicoControllerServiceModuleProfile profile, IMixedRealityInputSystem parentService)
            : base(name, priority, profile, parentService) { }

        private readonly Dictionary<Handedness, PicoController> activeControllers = new Dictionary<Handedness, PicoController>();

#if PICO_XR_LEGACY
        private bool allInOneHeadsetButtonsControllerEnabled;
#endif

#if PICO_XR_LEGACY
        /// <inheritdoc />
        public override void Initialize()
        {
            if (!Application.isPlaying)
            {
                return;
            }

            allInOneHeadsetButtonsControllerEnabled = TryGetControllerMappingProfile(typeof(PicoAllInOneHeadsetButtonsController), Handedness.None, out _);
        }
#endif

        /// <inheritdoc />
        public override void Update()
        {
#if PICO_XR_LEGACY
            if (allInOneHeadsetButtonsControllerEnabled)
            {
                // The all-in-one controller is located on the Pico headset itself and always available.
                // It also doesn't have a handedness associated.
                var headsetController = GetOrAddController(Handedness.None, typeof(PicoAllInOneHeadsetButtonsController));
                if (headsetController != null)
                {
                    headsetController.UpdateController();
                }
            }
#endif

            // We allow one type of controller per hand, check if there is a controller connected
            // for the left hand and update accordingly.
            if (PXR_Input.IsControllerConnected(PXR_Input.Controller.LeftController))
            {
                UpdateController(Handedness.Left);
            }
            else
            {
                RemoveController(Handedness.Left);
            }

            // We allow one type of controller per hand, check if there is a controller connected
            // for the right hand and update accordingly.
            if (PXR_Input.IsControllerConnected(PXR_Input.Controller.RightController))
            {
                UpdateController(Handedness.Right);
            }
            else
            {
                RemoveController(Handedness.Right);
            }
        }

        /// <inheritdoc />
        public override void Disable() => RemoveAllControllers();

        private void UpdateController(Handedness handedness)
        {
            var activeControllerDeviceType = PXR_Input.GetControllerDeviceType();
            PicoController controller;
            switch (activeControllerDeviceType)
            {
#if PICO_XR_LEGACY
                case PXR_Input.ControllerDevice.G2:
                    controller = GetOrAddController(handedness, typeof(PicoG24KController));
                    break;
                case PXR_Input.ControllerDevice.Neo2:
                    controller = GetOrAddController(handedness, typeof(PicoNeo2Controller));
                    break;
#endif
                case PXR_Input.ControllerDevice.Neo3:
                    controller = GetOrAddController(handedness, typeof(PicoNeo3Controller));
                    break;
                case PXR_Input.ControllerDevice.PICO_4:
                    controller = GetOrAddController(handedness, typeof(Pico4Controller));
                    break;
                case PXR_Input.ControllerDevice.NewController:
                default:
                    controller = null;
                    break;
            }

            if (controller != null)
            {
                controller.UpdateController();
            }
        }

        private void RemoveAllControllers()
        {
            foreach (var activeController in activeControllers)
            {
                RemoveController(activeController.Key, false);
            }

            activeControllers.Clear();
        }

        private bool TryGetController(Handedness handedness, out PicoController controller)
        {
            if (activeControllers.ContainsKey(handedness))
            {
                var existingController = activeControllers[handedness];
                Debug.Assert(existingController != null, $"{nameof(PicoController)} {handedness} has been destroyed but remains in the active controller registry.");
                controller = existingController;
                return true;
            }

            controller = null;
            return false;
        }

        private PicoController GetOrAddController(Handedness handedness, Type type)
        {
            // Check if a controller already exists.
            if (TryGetController(handedness, out var existingController))
            {
                if (existingController.GetType() == type)
                {
                    // Handedness and type match.
                    return existingController;
                }
                else
                {
                    // If a controller is registered for the right handedness but it is
                    // the wrong type of controller, remove it and continue.
                    RemoveController(handedness);
                }
            }

            try
            {
                var detectedController = (PicoController)Activator.CreateInstance(type, this, TrackingState.Tracked, handedness, GetControllerMappingProfile(type, handedness));
                detectedController.TryRenderControllerModel();
                AddController(detectedController);
                activeControllers.Add(handedness, detectedController);
                InputSystem?.RaiseSourceDetected(detectedController.InputSource, detectedController);

                return detectedController;
            }
            catch (Exception ex)
            {
                Debug.LogError($"Failed to create {type.Name}!\n{ex}");
                return null;
            }
        }

        private void RemoveController(Handedness handedness, bool removeFromRegistry = true)
        {
            if (TryGetController(handedness, out var controller))
            {
                InputSystem?.RaiseSourceLost(controller.InputSource, controller);

                if (removeFromRegistry)
                {
                    RemoveController(controller);
                    activeControllers.Remove(handedness);
                }
            }
        }
    }
}
