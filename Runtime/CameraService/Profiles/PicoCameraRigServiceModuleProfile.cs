// Copyright (c) Reality Collective. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using RealityToolkit.CameraService.Definitions;
using Unity.XR.PXR;
using UnityEngine;

namespace RealityToolkit.Pico.CameraService.Profiles
{
    /// <summary>
    /// Configuration profile for <see cref="PicoCameraRigServiceModule"/>.
    /// </summary>
    public class PicoCameraRigServiceModuleProfile : BaseCameraRigServiceModuleProfile
    {
        [SerializeField]
        [Tooltip("The rendering foveation level to use.")]
        private FoveationLevel foveationLevel = FoveationLevel.None;

        /// <summary>
        /// The rendering foveation level to use.
        /// </summary>
        public FoveationLevel FoveationLevel => foveationLevel;
    }
}
