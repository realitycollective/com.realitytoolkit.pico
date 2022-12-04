// Copyright (c) Reality Collective. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using RealityCollective.Editor.Extensions;
using RealityToolkit.Editor.Profiles.CameraSystem;
using RealityToolkit.Pico.CameraService.Profiles;
using UnityEditor;
using UnityEngine;

namespace RealityToolkit.Pico.Editor
{
    /// <summary>
    /// Default inspector for <see cref="PicoCameraServiceModuleProfile"/>.
    /// </summary>
    [CustomEditor(typeof(PicoCameraServiceModuleProfile))]
    public class PicoCameraServiceModuleProfileInspector : BaseMixedRealityCameraServiceModuleProfileInspector
    {
        private SerializedProperty foveationLevel;

        private bool showPicoSettings = true;
        private static readonly GUIContent picoSettingsFoldoutHeader = new GUIContent("Pico Platform Settings");

        protected override void OnEnable()
        {
            base.OnEnable();

            foveationLevel = serializedObject.FindProperty(nameof(foveationLevel));
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            serializedObject.Update();

            showPicoSettings = EditorGUILayoutExtensions.FoldoutWithBoldLabel(showPicoSettings, picoSettingsFoldoutHeader, true);
            if (showPicoSettings)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(foveationLevel);
                EditorGUI.indentLevel--;
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}