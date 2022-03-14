// Copyright (c) Reality Collective. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using RealityToolkit.Pico.CameraSystem.Profiles;
using Unity.XR.PXR;
using UnityEngine;
using XRTK.Attributes;
using XRTK.Interfaces.CameraSystem;
using XRTK.Providers.CameraSystem;

namespace RealityToolkit.Pico.CameraSystem.Providers
{
    /// <summary>
    /// <see cref="IMixedRealityCameraSystem"/> data provider used when running on the
    /// <see cref="PicoPlatform"/>.
    /// </summary>
    [RuntimePlatform(typeof(PicoPlatform))]
    [System.Runtime.InteropServices.Guid("01f7685f-40a4-49c1-b0cf-8d17dee1fb2b")]
    public class PicoCameraDataProvider : BaseCameraDataProvider
    {
        /// <inheritdoc />
        public PicoCameraDataProvider(string name, uint priority, PicoCameraDataProviderProfile profile, IMixedRealityCameraSystem parentService)
            : base(name, priority, profile, parentService)
        {
            foveationLevel = profile.FoveationLevel;
            useRecommendedAntiAliasingLevel = profile.UseRecommendedAntiAliasingLevel;
            cameraSystem = parentService;
        }

        private readonly FoveationLevel foveationLevel;
        private readonly IMixedRealityCameraSystem cameraSystem;
        private readonly bool useRecommendedAntiAliasingLevel;

        /// <inheritdoc />
        public override void Initialize()
        {
            base.Initialize();

            PXR_Plugin.UPxr_InitAndroidClass();
            PXR_Plugin.System.UPxr_SetSecure(PXR_ProjectSetting.GetProjectConfig().useContentProtect);
            PXR_Plugin.PlatformSetting.UPxr_BindVerifyService(cameraSystem.MainCameraRig.RigTransform.gameObject.name);
            cameraSystem.MainCameraRig.PlayerCamera.depthTextureMode = DepthTextureMode.Depth;

            var fps = -1;
            var rate = (int)GlobalIntConfigs.TargetFrameRate;
            PXR_Plugin.System.UPxr_GetIntConfig(rate, ref fps);
            float displayRefreshRate = 0.0f;
            int frame = (int)GlobalFloatConfigs.DisplayRefreshRate;
            PXR_Plugin.System.UPxr_GetFloatConfig(frame, ref displayRefreshRate);
            Application.targetFrameRate = fps > 0 ? fps : (int)displayRefreshRate;

            PXR_Plugin.Render.UPxr_EnableFoveation(foveationLevel != FoveationLevel.None);
            if (foveationLevel != FoveationLevel.None)
            {
                PXR_Plugin.Render.UPxr_SetFoveationLevel(foveationLevel);
            }

            PXR_Plugin.System.UPxr_InitKeyEventManager();
            PXR_Plugin.System.UPxr_StartReceiver();

            var recommendedAntiAliasingLevel = 0;
            PXR_Plugin.System.UPxr_GetIntConfig((int)GlobalIntConfigs.AntiAliasingLevelRecommended, ref recommendedAntiAliasingLevel);
            if (useRecommendedAntiAliasingLevel && QualitySettings.antiAliasing != recommendedAntiAliasingLevel)
            {
                QualitySettings.antiAliasing = recommendedAntiAliasingLevel;
            }
        }
    }
}