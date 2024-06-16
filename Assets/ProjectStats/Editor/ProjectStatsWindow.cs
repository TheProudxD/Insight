using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace ProjectStats
{
    public class ProjectStatsWindow : EditorWindow
    {
        private string folderPath = "Assets";
        private int scriptCount = 0;
        private int totalLines = 0;
        private int totalNamespaces = 0;
        private int classCount = 0;
        private int interfaceCount = 0;
        private int enumCount = 0;
        private int structCount = 0;
        private int prefabCount = 0;
        private int materialCount = 0;
        private int sceneCount = 0;
        private int textureCount = 0;
        private int audioClipCount = 0;
        private int shaderCount = 0;
        private int animationClipCount = 0;
        private long totalAssetSize = 0;
        private string projectName = "";
        private string projectVersion = "";
        private string targetPlatform = "";

        private string logFilePath = Path.Combine(Application.dataPath, "ProjectStatsLog.txt");

        [MenuItem("Window/ProjectStats")]
        public static void ShowWindow()
        {
            GetWindow<ProjectStatsWindow>("Project Info");
        }

        private void OnGUI()
        {
            GUILayout.Label("Folder Path", EditorStyles.boldLabel);
            folderPath = EditorGUILayout.TextField("Folder Path", folderPath);

            if (GUILayout.Button("Analyze"))
            {
                AnalyzeProject();
            }

            GUILayout.Space(20);

            GUILayout.Label("Project Metadata", EditorStyles.boldLabel);
            EditorGUILayout.LabelField("Project Name:", projectName);
            EditorGUILayout.LabelField("Project Version:", projectVersion);
            EditorGUILayout.LabelField("Target Platform:", targetPlatform);

            GUILayout.Space(20);

            GUILayout.Label("Code Analysis", EditorStyles.boldLabel);
            EditorGUILayout.LabelField("Total .cs Scripts:", scriptCount.ToString());
            EditorGUILayout.LabelField("Total Lines:", totalLines.ToString());
            EditorGUILayout.LabelField("Total Namespaces:", totalNamespaces.ToString());
            EditorGUILayout.LabelField("Total Classes:", classCount.ToString());
            EditorGUILayout.LabelField("Total Interfaces:", interfaceCount.ToString());
            EditorGUILayout.LabelField("Total Enums:", enumCount.ToString());
            EditorGUILayout.LabelField("Total Structs:", structCount.ToString());

            GUILayout.Space(20);

            GUILayout.Label("Asset Analysis", EditorStyles.boldLabel);
            EditorGUILayout.LabelField("Total Prefabs:", prefabCount.ToString());
            EditorGUILayout.LabelField("Total Materials:", materialCount.ToString());
            EditorGUILayout.LabelField("Total Scenes:", sceneCount.ToString());
            EditorGUILayout.LabelField("Total Textures:", textureCount.ToString());
            EditorGUILayout.LabelField("Total Audio Clips:", audioClipCount.ToString());
            EditorGUILayout.LabelField("Total Shaders:", shaderCount.ToString());
            EditorGUILayout.LabelField("Total Animation Clips:", animationClipCount.ToString());
            EditorGUILayout.LabelField("Total Asset Size (KB):", (totalAssetSize / 1024).ToString());

            GUILayout.Space(20);
            GUILayout.Label("Results saved to ProjectAnalysisLog.txt:", EditorStyles.boldLabel);
            if (GUILayout.Button("Open Log File"))
            {
                EditorUtility.RevealInFinder(logFilePath);
            }
        }

        private void AnalyzeProject()
        {
            // Reset counts
            scriptCount = 0;
            totalLines = 0;
            classCount = 0;
            interfaceCount = 0;
            enumCount = 0;
            structCount = 0;
            prefabCount = 0;
            materialCount = 0;
            sceneCount = 0;
            textureCount = 0;
            audioClipCount = 0;
            shaderCount = 0;
            animationClipCount = 0;
            totalAssetSize = 0;

            // Fetch project metadata
            projectName = PlayerSettings.productName;
            projectVersion = PlayerSettings.bundleVersion;
            targetPlatform = EditorUserBuildSettings.activeBuildTarget.ToString();

            StringBuilder logBuilder = new StringBuilder();

            logBuilder.AppendLine("Project Metadata:");
            logBuilder.AppendLine($"Project Name: {projectName}");
            logBuilder.AppendLine($"Project Version: {projectVersion}");
            logBuilder.AppendLine($"Target Platform: {targetPlatform}");
            logBuilder.AppendLine();

            // Get all .cs files in the folder and subfolders
            string[] csFiles = Directory.GetFiles(folderPath, "*.cs", SearchOption.AllDirectories);
            scriptCount = csFiles.Length;
            logBuilder.AppendLine("Scripts:");
            var csFilesWithSizes = csFiles.Select(file => new FileInfo(file)).OrderByDescending(fi => fi.Length);
            foreach (var fileInfo in csFilesWithSizes)
            {
                string[] lines = File.ReadAllLines(fileInfo.FullName);
                totalLines += lines.Length;

                foreach (string line in lines)
                {
                    string trimmedLine = line.Trim();

                    if (trimmedLine.StartsWith("namespace "))
                    {
                        totalNamespaces++;
                    }
                    else
                    if (trimmedLine.StartsWith("public class ") || trimmedLine.Contains(" class "))
                    {
                        classCount++;
                    }
                    else if (trimmedLine.StartsWith("public interface ") || trimmedLine.Contains(" interface "))
                    {
                        interfaceCount++;
                    }
                    else if (trimmedLine.StartsWith("public enum ") || trimmedLine.Contains(" enum "))
                    {
                        enumCount++;
                    }
                    else if (trimmedLine.StartsWith("public struct ") || trimmedLine.Contains(" struct "))
                    {
                        structCount++;
                    }
                }

                logBuilder.AppendLine($"{fileInfo.Name} - {fileInfo.Length / 1024f:0.00} KB");
            }

            // Get all asset files in the folder and subfolders
            logBuilder.AppendLine();
            logBuilder.AppendLine("Assets:");

            string[] prefabs = Directory.GetFiles(folderPath, "*.prefab", SearchOption.AllDirectories);
            prefabCount = prefabs.Length;
            logBuilder.AppendLine("Prefabs:");
            var prefabFilesWithSizes = prefabs.Select(file => new FileInfo(file)).OrderByDescending(fi => fi.Length);
            foreach (var fileInfo in prefabFilesWithSizes)
            {
                logBuilder.AppendLine($"{fileInfo.Name} - {fileInfo.Length / 1024f:0.00} KB");
            }

            string[] materials = Directory.GetFiles(folderPath, "*.mat", SearchOption.AllDirectories);
            materialCount = materials.Length;
            logBuilder.AppendLine();
            logBuilder.AppendLine("Materials:");
            var materialFilesWithSizes = materials.Select(file => new FileInfo(file)).OrderByDescending(fi => fi.Length);
            foreach (var fileInfo in materialFilesWithSizes)
            {
                logBuilder.AppendLine($"{fileInfo.Name} - {fileInfo.Length / 1024f:0.00} KB");
            }

            string[] scenes = Directory.GetFiles(folderPath, "*.unity", SearchOption.AllDirectories);
            sceneCount = scenes.Length;
            logBuilder.AppendLine();
            logBuilder.AppendLine("Scenes:");
            var sceneFilesWithSizes = scenes.Select(file => new FileInfo(file)).OrderByDescending(fi => fi.Length);
            foreach (var fileInfo in sceneFilesWithSizes)
            {
                logBuilder.AppendLine($"{fileInfo.Name} - {fileInfo.Length / 1024f:0.00} KB");
            }

            string[] textures = Directory.GetFiles(folderPath, "*.png", SearchOption.AllDirectories);
            textureCount += textures.Length;
            textures = textures.Concat(Directory.GetFiles(folderPath, "*.jpg", SearchOption.AllDirectories)).ToArray();
            textureCount += textures.Length;
            logBuilder.AppendLine();
            logBuilder.AppendLine("Textures:");
            var textureFilesWithSizes = textures.Select(file => new FileInfo(file)).OrderByDescending(fi => fi.Length);
            foreach (var fileInfo in textureFilesWithSizes)
            {
                logBuilder.AppendLine($"{fileInfo.Name} - {fileInfo.Length / 1024f:0.00} KB");
            }

            string[] audioClips = Directory.GetFiles(folderPath, "*.wav", SearchOption.AllDirectories);
            audioClipCount += audioClips.Length;
            audioClips = audioClips.Concat(Directory.GetFiles(folderPath, "*.mp3", SearchOption.AllDirectories)).ToArray();
            audioClipCount += audioClips.Length;
            logBuilder.AppendLine();
            logBuilder.AppendLine("Audio Clips:");
            var audioFilesWithSizes = audioClips.Select(file => new FileInfo(file)).OrderByDescending(fi => fi.Length);
            foreach (var fileInfo in audioFilesWithSizes)
            {
                logBuilder.AppendLine($"{fileInfo.Name} - {fileInfo.Length / 1024f:0.00} KB");
            }

            string[] shaders = Directory.GetFiles(folderPath, "*.shader", SearchOption.AllDirectories);
            shaderCount = shaders.Length;
            logBuilder.AppendLine();
            logBuilder.AppendLine("Shaders:");
            var shaderFilesWithSizes = shaders.Select(file => new FileInfo(file)).OrderByDescending(fi => fi.Length);
            foreach (var fileInfo in shaderFilesWithSizes)
            {
                logBuilder.AppendLine($"{fileInfo.Name} - {fileInfo.Length / 1024f:0.00} KB");
            }

            string[] animationClips = Directory.GetFiles(folderPath, "*.anim", SearchOption.AllDirectories);
            animationClipCount = animationClips.Length;
            logBuilder.AppendLine();
            logBuilder.AppendLine("Animation Clips:");
            var animationFilesWithSizes = animationClips.Select(file => new FileInfo(file)).OrderByDescending(fi => fi.Length);
            foreach (var fileInfo in animationFilesWithSizes)
            {
                logBuilder.AppendLine($"{fileInfo.Name} - {fileInfo.Length / 1024f:0.00} KB");
            }

            string[] allAssets = Directory.GetFiles(folderPath, "*.*", SearchOption.AllDirectories);
            foreach (string asset in allAssets)
            {
                FileInfo fileInfo = new FileInfo(asset);
                totalAssetSize += fileInfo.Length;
            }

            string logFilePath = Path.Combine(Application.dataPath, "ProjectAnalysisLog.txt");
            File.WriteAllText(logFilePath, logBuilder.ToString());

            Repaint();
        }
    }
}