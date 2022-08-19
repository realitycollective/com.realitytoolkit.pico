// Copyright (c) Reality Collective. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using RealityCollective.ServiceFramework.Definitions.Platforms;
using RealityCollective.ServiceFramework.Interfaces;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

namespace RealityToolkit.Pico
{
    /// <summary>
    /// Used by the toolkit to signal that a feature is available on the Pico platform.
    /// </summary>
    [System.Runtime.InteropServices.Guid("91d05795-d44e-4a4d-8055-e770b592137f")]
    public class PicoPlatform : BasePlatform
    {
        private const string xrDisplaySubsystemDescriptorId = "PicoXR Display";
        private const string xrInputSubsystemDescriptorId = "PicoXR Input";

        /// <inheritdoc />
        public override IPlatform[] PlatformOverrides { get; } =
        {
            new AndroidPlatform()
        };

        /// <inheritdoc />
        public override bool IsAvailable
        {
            get
            {
                var displaySubsystems = new List<XRDisplaySubsystem>();
                SubsystemManager.GetSubsystems(displaySubsystems);
                var xrDisplaySubsystemDescriptorFound = false;

                for (var i = 0; i < displaySubsystems.Count; i++)
                {
                    var displaySubsystem = displaySubsystems[i];
                    if (displaySubsystem.SubsystemDescriptor.id.Equals(xrDisplaySubsystemDescriptorId) &&
                        displaySubsystem.running)
                    {
                        xrDisplaySubsystemDescriptorFound = true;
                    }
                }

                // The XR Display Subsystem is not available / running,
                // the platform doesn't seem to be available.
                if (!xrDisplaySubsystemDescriptorFound)
                {
                    return false;
                }

                var inputSubsystems = new List<XRInputSubsystem>();
                SubsystemManager.GetSubsystems(inputSubsystems);
                var xrInputSubsystemDescriptorFound = false;

                for (var i = 0; i < inputSubsystems.Count; i++)
                {
                    var inputSubsystem = inputSubsystems[i];
                    if (inputSubsystem.SubsystemDescriptor.id.Equals(xrInputSubsystemDescriptorId) &&
                        inputSubsystem.running)
                    {
                        xrInputSubsystemDescriptorFound = true;
                    }
                }

                // The XR Input Subsystem is not available / running,
                // the platform doesn't seem to be available.
                if (!xrInputSubsystemDescriptorFound)
                {
                    return false;
                }

                // Only if both, Display and Input XR Subsystems are available
                // and running, the platform is considered available.
                return true;
            }
        }

#if UNITY_EDITOR
        /// <inheritdoc />
        public override UnityEditor.BuildTarget[] ValidBuildTargets { get; } =
        {
            UnityEditor.BuildTarget.Android
        };
#endif
    }
}