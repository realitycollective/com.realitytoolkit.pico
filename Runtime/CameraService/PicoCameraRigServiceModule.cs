// Copyright (c) Reality Collective. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using RealityCollective.ServiceFramework.Attributes;
using RealityToolkit.CameraService.Interfaces;
using RealityToolkit.CameraService.Modules;
using RealityToolkit.Pico.CameraService.Profiles;
using Unity.XR.PXR;
using UnityEngine;
using FoveatedRenderingMode = Unity.XR.PXR.FoveatedRenderingMode;

namespace RealityToolkit.Pico.CameraService
{
    /// <summary>
    /// <see cref="ICameraSystem"/> service module used when running on the <see cref="PicoPlatform"/>.
    /// </summary>
    [RuntimePlatform(typeof(PicoPlatform))]
    [System.Runtime.InteropServices.Guid("01f7685f-40a4-49c1-b0cf-8d17dee1fb2b")]
    public class PicoCameraRigServiceModule : BaseCameraRigServiceModule, IPicoCameraRigServiceModule
    {
        /// <inheritdoc />
        public PicoCameraRigServiceModule(string name, uint priority, PicoCameraRigServiceModuleProfile profile, ICameraService parentService)
            : base(name, priority, profile, parentService)
        {
            foveationLevel = profile.FoveationLevel;
            foveatedRenderingMode = profile.FoveatedRenderingMode;
            cameraService = parentService;
        }

        private readonly FoveationLevel foveationLevel;
        private readonly FoveatedRenderingMode foveatedRenderingMode;
        private readonly ICameraService cameraService;

        /// <inheritdoc />
        public override void Initialize()
        {
            base.Initialize();

            PXR_Plugin.System.UPxr_SetSecure(PXR_ProjectSetting.GetProjectConfig().useContentProtect);
            cameraService.CameraRig.RigCamera.depthTextureMode = DepthTextureMode.Depth;
            PXR_FoveationRendering.SetFoveationLevel(foveationLevel, foveatedRenderingMode == FoveatedRenderingMode.EyeTrackedFoveatedRendering);
        }
    }
}