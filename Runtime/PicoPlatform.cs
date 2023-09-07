// Copyright (c) Reality Collective. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using RealityCollective.Extensions;
using RealityCollective.ServiceFramework.Definitions.Platforms;
using RealityCollective.ServiceFramework.Interfaces;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Management;

#if UNITY_EDITOR && PICO_XR_LP
using Unity.XR.PICO.LivePreview;
#else
using Unity.XR.PXR;
#endif

namespace RealityToolkit.Pico
{
    /// <summary>
    /// Used to signal that a feature is available on the Pico platform.
    /// </summary>
    [System.Runtime.InteropServices.Guid("91d05795-d44e-4a4d-8055-e770b592137f")]
    public class PicoPlatform : BasePlatform
    {
#if !UNITY_EDITOR || !PICO_XR_LP
        private const string xrDisplaySubsystemDescriptorId = "PICO Display";
        private const string xrInputSubsystemDescriptorId = "PICO Input";
#endif

        // When in editor and the live preview package is available, we are looking for a different
        // loader type and also different descriptors.
#if UNITY_EDITOR && PICO_XR_LP
        private const string xrLivePreviewDisplaySubsystemDescriptorId = "PICO LP Display";
        private const string xrLivePreviewInputSubsystemDescriptorId = "PICO LP Input";

        private bool IsXRLoaderActive => XRGeneralSettings.Instance.IsNotNull() &&
            ((XRGeneralSettings.Instance.Manager.activeLoader != null && XRGeneralSettings.Instance.Manager.activeLoader.GetType() == typeof(PXR_PTLoader)) ||
            (XRGeneralSettings.Instance.Manager.activeLoaders != null && XRGeneralSettings.Instance.Manager.activeLoaders.Any(l => l.GetType() == typeof(PXR_PTLoader))));
#else
        private bool IsXRLoaderActive => XRGeneralSettings.Instance.IsNotNull() &&
            ((XRGeneralSettings.Instance.Manager.activeLoader != null && XRGeneralSettings.Instance.Manager.activeLoader.GetType() == typeof(PXR_Loader)) ||
            (XRGeneralSettings.Instance.Manager.activeLoaders != null && XRGeneralSettings.Instance.Manager.activeLoaders.Any(l => l.GetType() == typeof(PXR_Loader))));
#endif

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
                if (!IsXRLoaderActive)
                {
                    // The platform XR loader is not active.
                    return false;
                }

                // When in editor and the live preview package is available,
                // we must look for different descriptor IDs to determine platfrom availability.
                // If not in editor or no live preview package available, just use the runtime player build descriptors.
#if UNITY_EDITOR && PICO_XR_LP
                var displaySubsystemDescriptorId = xrLivePreviewDisplaySubsystemDescriptorId;
                var inputSubsystemDescriptorId = xrLivePreviewInputSubsystemDescriptorId;
#else
                var displaySubsystemDescriptorId = xrDisplaySubsystemDescriptorId;
                var inputSubsystemDescriptorId = xrInputSubsystemDescriptorId;
#endif

                var displaySubsystems = new List<XRDisplaySubsystem>();
                SubsystemManager.GetSubsystems(displaySubsystems);
                var xrDisplaySubsystemDescriptorFound = false;

                for (var i = 0; i < displaySubsystems.Count; i++)
                {
                    var displaySubsystem = displaySubsystems[i];
                    if (displaySubsystem.SubsystemDescriptor.id.Equals(displaySubsystemDescriptorId) &&
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
                    if (inputSubsystem.SubsystemDescriptor.id.Equals(inputSubsystemDescriptorId) &&
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
        public override bool IsBuildTargetAvailable => IsXRLoaderActive && base.IsBuildTargetAvailable;

        /// <inheritdoc />
        public override UnityEditor.BuildTarget[] ValidBuildTargets { get; } =
        {
            UnityEditor.BuildTarget.Android
        };
#endif
    }
}