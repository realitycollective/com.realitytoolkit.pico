// Copyright (c) Reality Collective. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using RealityToolkit.CameraSystem.Definitions;
using Unity.XR.PXR;
using UnityEngine;

namespace RealityToolkit.Pico.CameraSystem.Profiles
{
    /// <summary>
    /// Configuration profile for <see cref="Providers.PicoCameraDataProvider"/>.
    /// </summary>
    public class PicoCameraDataProviderProfile : BaseMixedRealityCameraServiceModuleProfile
    {
        [SerializeField]
        [Tooltip("The rendering foveation level to use.")]
        private FoveationLevel foveationLevel = FoveationLevel.None;

        /// <summary>
        /// The rendering foveation level to use.
        /// </summary>
        public FoveationLevel FoveationLevel => foveationLevel;

        [SerializeField]
        [Tooltip("Should the Pico recommended anti aliasing level be appplied?")]
        private bool useRecommendedAntiAliasingLevel = true;

        /// <summary>
        /// Should the Pico recommended anti aliasing level be appplied?
        /// </summary>
        public bool UseRecommendedAntiAliasingLevel => useRecommendedAntiAliasingLevel;
    }
}
