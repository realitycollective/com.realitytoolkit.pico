// Copyright (c) Reality Collective. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using RealityToolkit.Pico.CameraSystem.Profiles;
using UnityEditor;
using UnityEngine;
using RealityToolkit.Editor.Extensions;
using RealityToolkit.Editor.Profiles.CameraSystem;

namespace RealityToolkit.Pico.Editor
{
    /// <summary>
    /// Default inspector for <see cref="PicoCameraDataProviderProfile"/>.
    /// </summary>
    [CustomEditor(typeof(PicoCameraDataProviderProfile))]
    public class PicoCameraDataProviderProfileInspector : BaseMixedRealityCameraDataProviderProfileInspector
    {
        private SerializedProperty foveationLevel;
        private SerializedProperty useRecommendedAntiAliasingLevel;

        private bool showPicoSettings = true;
        private static readonly GUIContent picoSettingsFoldoutHeader = new GUIContent("Pico Platform Settings");

        protected override void OnEnable()
        {
            base.OnEnable();

            foveationLevel = serializedObject.FindProperty(nameof(foveationLevel));
            useRecommendedAntiAliasingLevel = serializedObject.FindProperty(nameof(useRecommendedAntiAliasingLevel));
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
                EditorGUILayout.PropertyField(useRecommendedAntiAliasingLevel);
                EditorGUI.indentLevel--;
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}