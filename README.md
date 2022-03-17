# Reality Toolkit - pico Platform Module

The pico platform components for the [Reality Toolkit](https://github.com/realitycollective/com.realitytoolkit.core). This package enables your Reality Toolkit based project to run on pico devices.

## What's included?

- Data Providers for the Camera System
- Data Providers for the Input System

## Supported Devices

- Pico G2 4K
- Pico Neo 2
- Pico Neo 3

## Requirements

- [RealityToolkit.Core](https://github.com/realitycollective/com.realitytoolkit.core)
- [RealityToolkit.SDK](https://github.com/realitycollective/com.realitytoolkit.sdk)
- [Unity 2020.3 and above](https://unity.com/)
- [Legacy PicoXR Plugin for Unity](https://developer.pico-interactive.com/sdk/index?id=8&device_id=1&platform_id=1)

## Getting Started

> *Note, the PICO SDK (Aquired separately from the Pico developers, see link above) is REQUIRED to use this platform.  This needs to be installed prior to downloading the Pico platform for the Reality Toolkit

To get started, install the PICO SDK and then install this package.  If you already have an instance of the Reality Toolkit in your AR/VR scene, then installing this package will automatically add the Pico elements in to your configuration profile.  If not, then when you have configured your scene you can select the "Platform Service Configurations" asset in the "RealityToolkit.Generated\Pico\Profiles" folder and click the "Install Platform Service COnfiguration" button to register the Pico configuration with your project.

For more details, check the ["Getting Started" documentation](http://realitycollective.github.io/) for the Reality Toolkit (coming soon)

### OpenUPM
<!-- Check openUPM links and details -->

[![openupm](https://img.shields.io/npm/v/com.realitytoolkit.pico?label=openupm&registry_uri=https://package.openupm.com)](https://openupm.com/packages/com.realitytoolkit.pico/)

The simplest way to getting started using the pico platform package in your project is via OpenUPM. Visit [OpenUPM](https://openupm.com/docs/) to learn more about it. Once you have the OpenUPM CLI set up use the following command to add the package to your project:

```
`openupm add com.realitytoolkit.pico`
```

> For more details on using [OpenUPM CLI, check the docs here](https://github.com/openupm/openupm-cli#installation).

# Build Status
<!-- Check build status links and details -->

| branch | build status |
| --- | --- |
| main | [![main](https://github.com/realitycollective/com.realitytoolkit.pico/actions/workflows/buildupmpackages.yml/badge.svg?branch=main)](https://github.com/realitycollective/com.realitytoolkit.pico/actions/workflows/buildupmpackages.yml) |
| development | [![development](https://github.com/realitycollective/com.realitytoolkit.pico/actions/workflows/buildupmpackages.yml/badge.svg?branch=development)](https://github.com/realitycollective/com.realitytoolkit.pico/actions/workflows/buildupmpackages.yml) |

