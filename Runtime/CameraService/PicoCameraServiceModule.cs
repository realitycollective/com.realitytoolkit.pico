// Copyright (c) Reality Collective. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using RealityCollective.ServiceFramework.Attributes;
using RealityToolkit.CameraSystem.Interfaces;
using RealityToolkit.CameraSystem.Providers;
using RealityToolkit.Pico.CameraService.Profiles;
using Unity.XR.PXR;
using UnityEngine;

namespace RealityToolkit.Pico.CameraService
{
    /// <summary>
    /// <see cref="IMixedRealityCameraSystem"/> service module used when running on the <see cref="PicoPlatform"/>.
    /// </summary>
    [RuntimePlatform(typeof(PicoPlatform))]
    [System.Runtime.InteropServices.Guid("01f7685f-40a4-49c1-b0cf-8d17dee1fb2b")]
    public class PicoCameraServiceModule : BaseCameraServiceModule, IPicoCameraServiceModule
    {
        /// <inheritdoc />
        public PicoCameraServiceModule(string name, uint priority, PicoCameraServiceModuleProfile profile, IMixedRealityCameraSystem parentService)
            : base(name, priority, profile, parentService)
        {
            foveationLevel = profile.FoveationLevel;
            cameraSystem = parentService;
        }

        private readonly FoveationLevel foveationLevel;
        private readonly IMixedRealityCameraSystem cameraSystem;

        /// <inheritdoc />
        public override void Initialize()
        {
            base.Initialize();

            PXR_Plugin.System.UPxr_SetSecure(PXR_ProjectSetting.GetProjectConfig().useContentProtect);
            PXR_Plugin.PlatformSetting.UPxr_BindVerifyService(cameraSystem.MainCameraRig.RigTransform.gameObject.name);
            cameraSystem.MainCameraRig.PlayerCamera.depthTextureMode = DepthTextureMode.Depth;

            PXR_FoveationRendering.SetFoveationLevel(foveationLevel);
        }
    }
}