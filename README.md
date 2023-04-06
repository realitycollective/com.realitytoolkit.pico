# Reality Toolkit - PICO

![GitHub](https://user-images.githubusercontent.com/9565734/198220057-d90bcfc5-9b5e-43e4-a9a6-c5ac3ba946a5.png)

The [PICO](https://www.picoxr.com/) platform components for the [Reality Toolkit](https://github.com/realitycollective/com.realitytoolkit.core).
This package enables your Reality Toolkit based project to run on PICO devices.

[![openupm](https://img.shields.io/npm/v/com.realitytoolkit.pico?label=openupm&registry_uri=https://package.openupm.com)](https://openupm.com/packages/com.realitytoolkit.pico/)
[![Discord](https://img.shields.io/discord/597064584980987924.svg?label=&logo=discord&logoColor=ffffff&color=7389D8&labelColor=6A7EC2)](https://discord.gg/hF7TtRCFmB)
[![Publish main branch and increment version](https://github.com/realitycollective/com.realitytoolkit.pico/actions/workflows/main-publish.yml/badge.svg)](https://github.com/realitycollective/com.realitytoolkit.pico/actions/workflows/main-publish.yml)
[![Publish development branch on Merge](https://github.com/realitycollective/com.realitytoolkit.pico/actions/workflows/development-publish.yml/badge.svg)](https://github.com/realitycollective/com.realitytoolkit.pico/actions/workflows/development-publish.yml)
[![Build and test UPM packages for platforms, all branches except main](https://github.com/realitycollective/com.realitytoolkit.pico/actions/workflows/development-buildandtestupmrelease.yml/badge.svg)](https://github.com/realitycollective/com.realitytoolkit.pico/actions/workflows/development-buildandtestupmrelease.yml)

## What is included?

- Service modules for the Reality Toolkit's Camera Service
- Service modules for the Reality Toolkit's Input Service

## Supported Devices

- PICO G24K*
- PICO Neo 2*
- PICO Neo 3 Pro
- PICO Neo 3 Link
- PICO Neo 3 Pro Eye
- PICO 4
- PICO 4 Pro
- PICO 4 Enterprise

*Requires the legacy v1 Pico integration SDK package as a dependency

## Requirements

- [RealityToolkit.Core](https://github.com/realitycollective/com.realitytoolkit.core)
- [RealityToolkit.Camera](https://github.com/realitycollective/com.realitytoolkit.camera)
- [Unity 2020.3 and above](https://unity.com/)
- [PICO Unity Integration SDK](https://developer-global.pico-interactive.com/sdk?deviceId=1&platformId=1&itemId=12)
- [OpenUPM](https://openupm.com/docs/)

## Getting Started

Import the [PICO Unity Integration SDK](https://developer-global.pico-interactive.com/sdk?deviceId=1&platformId=1&itemId=12) to your project and then install the PICO module for the toolkit using:

```text
    openupm add com.realitytoolkit.pico
```

If you already have an instance of the Reality Toolkit in your scene, then installing this package will automatically add the PICO modules to your configuration profile.
