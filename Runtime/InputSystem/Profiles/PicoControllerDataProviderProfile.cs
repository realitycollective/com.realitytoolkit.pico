// Copyright (c) Reality Collective. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using RealityToolkit.Pico.InputSystem.Controllers;
using RealityToolkit.Definitions.Controllers;
using RealityCollective.Definitions.Utilities;

namespace RealityToolkit.Pico.InputSystem.Profiles
{
    /// <summary>
    /// Configuration profile for <see cref="PicoControllerDataProvider"/>.
    /// </summary>
    public class PicoControllerDataProviderProfile : BaseMixedRealityControllerDataProviderProfile
    {
        /// <inheritdoc />
        public override ControllerDefinition[] GetDefaultControllerOptions()
        {
            return new[]
            {
                new ControllerDefinition(typeof(PicoAllInOneHeadsetButtonsController), Handedness.None),
                new ControllerDefinition(typeof(PicoG24KController), Handedness.Left),
                new ControllerDefinition(typeof(PicoG24KController), Handedness.Right),
                new ControllerDefinition(typeof(PicoNeo2Controller), Handedness.Left),
                new ControllerDefinition(typeof(PicoNeo2Controller), Handedness.Right),
                new ControllerDefinition(typeof(PicoNeo3Controller), Handedness.Left),
                new ControllerDefinition(typeof(PicoNeo3Controller), Handedness.Right)
            };
        }
    }
}
