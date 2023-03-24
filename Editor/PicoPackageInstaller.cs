// Copyright (c) Reality Collective. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using RealityCollective.Editor.Utilities;
using RealityCollective.Extensions;
using RealityCollective.ServiceFramework.Editor;
using RealityCollective.ServiceFramework.Editor.Packages;
using RealityToolkit.Editor;
using System.IO;
using UnityEditor;

namespace RealityToolkit.Pico.Editor
{
    [InitializeOnLoad]
    internal static class PicoPackageInstaller
    {
        private static readonly string destinationPath = $"{MixedRealityPreferences.ProfileGenerationPath}Pico";
        private static readonly string sourcePath = Path.GetFullPath($"{PathFinderUtility.ResolvePath<IPathFinder>(typeof(PicoPackagePathFinder)).ForwardSlashes()}{Path.DirectorySeparatorChar}{MixedRealityPreferences.HIDDEN_PACKAGE_ASSETS_PATH}");

        static PicoPackageInstaller()
        {
            EditorApplication.delayCall += CheckPackage;
        }

        [MenuItem(MixedRealityPreferences.Editor_Menu_Keyword + "/Packages/Install Pico Package Assets...", true)]
        private static bool ImportPackageAssetsValidation()
        {
            return !Directory.Exists($"{destinationPath}{Path.DirectorySeparatorChar}");
        }

        [MenuItem(MixedRealityPreferences.Editor_Menu_Keyword + "/Packages/Install Pico Package Assets...")]
        private static void ImportPackageAssets()
        {
            EditorPreferences.Set($"{nameof(PicoPackageInstaller)}.Assets", false);
            EditorApplication.delayCall += CheckPackage;
        }

        private static void CheckPackage()
        {
            if (!EditorPreferences.Get($"{nameof(PicoPackageInstaller)}.Assets", false))
            {
                EditorPreferences.Set($"{nameof(PicoPackageInstaller)}.Assets", AssetsInstaller.TryInstallAssets(sourcePath, destinationPath));
            }
        }
    }
}
