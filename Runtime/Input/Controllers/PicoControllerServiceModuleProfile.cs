// Copyright (c) Reality Collective. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using RealityCollective.Definitions.Utilities;
using RealityToolkit.Definitions.Controllers;

namespace RealityToolkit.Pico.Input.Controllers
{
    /// <summary>
    /// Configuration profile for <see cref="InputService.PicoControllerServiceModule"/>.
    /// </summary>
    public class PicoControllerServiceModuleProfile : BaseControllerServiceModuleProfile
    {
        /// <inheritdoc />
        public override ControllerDefinition[] GetDefaultControllerOptions()
        {
            return new[]
            {
                new ControllerDefinition(typeof(PicoNeo3Controller), Handedness.Left),
                new ControllerDefinition(typeof(PicoNeo3Controller), Handedness.Right),
                new ControllerDefinition(typeof(Pico4Controller), Handedness.Left),
                new ControllerDefinition(typeof(Pico4Controller), Handedness.Right)
            };
        }
    }
}
