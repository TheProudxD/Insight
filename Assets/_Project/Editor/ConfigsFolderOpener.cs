using UnityEditor;
using UnityEngine;

namespace _Project.Editor
{
    public class ConfigsFolderOpener : EditorWindow
    {
        [MenuItem("Insight/ConfigsOpener")]
        private static void ConfigOpener()
        {
            Application.OpenURL(Application.persistentDataPath);
        }

        [MenuItem("Insight/WebServer")]
        private static void WebServerOpener()
        {
            Application.OpenURL(@"http://game.ispu.ru/insight");
        }

        /*
        private static void ShowWindow()
        {
            var window = GetWindow<ConfigsFolderOpener>();
            window.titleContent = new GUIContent("TITLE");
            window.Show();
        }*/
    }
}