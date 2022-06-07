// Copyright (c) Reality Collective. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Unity.XR.PXR;
using RealityCollective.Definitions.Utilities;

namespace RealityToolkit.Pico.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="Handedness"/>.
    /// </summary>
    public static class HandednessExtensions
    {
        /// <summary>
        /// Converts the <see cref="Handedness"/> to a Pico <see cref="PXR_Input.Controller"/> value.
        /// </summary>
        /// <param name="handedness">The <see cref="Handedness"/> to convert.</param>
        /// <returns></returns>
        public static PXR_Input.Controller ToPicoController(this Handedness handedness)
        {
            switch (handedness)
            {
                case Handedness.Left:
                    return PXR_Input.Controller.LeftController;
                case Handedness.Right:
                    return PXR_Input.Controller.RightController;
                case Handedness.None:
                case Handedness.Both:
                case Handedness.Other:
                case Handedness.Any:
                default:
                    return PXR_Input.Controller.RightController;
            }
        }
    }
}