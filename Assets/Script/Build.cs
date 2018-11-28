using System;
using System.IO;
using UnityEditor;
using UnityEngine;


[ExecuteInEditMode]
public class AutoBuilder
{
#if UNITY_EDITOR
    [MenuItem("File/AutoBuilder/Android")]
    static void PerformBuild()
    {
        string[] scenes = { "Assets/Scenes/Home.unity", "Assets/Scenes/InGame.unity" };

        string buildPath = "./Build/Android/";
        string buildFileName = "build.apk";

        // Create build folder if not yet exists
        Directory.CreateDirectory(buildPath);
        
        EditorPrefs.SetString("JdkPath", "/usr/lib/jdk1.8.0_191/");
        EditorPrefs.SetString("AndroidSdkRoot", "/home/moreal/Android/Sdk");
        EditorPrefs.SetString("AndroidNdkRoot", "/home/moreal/Android/android-ndk-r13b");

        PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.Android, "com.dsmgg.exchickit");
        PlayerSettings.statusBarHidden = true;

        BuildPipeline.BuildPlayer(scenes, buildPath + buildFileName, BuildTarget.Android, BuildOptions.None);
        
        
    }
#endif
}