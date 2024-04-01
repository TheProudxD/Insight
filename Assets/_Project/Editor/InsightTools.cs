using UnityEditor;
using UnityEngine;

namespace _Project.Editor
{
    public class InsightTools : EditorWindow
    {
        [MenuItem("Insight/Local Configs")]
        private static void OpenConfigs() => Application.OpenURL(Application.persistentDataPath);

        [MenuItem("Insight/Web Server")]
        private static void OpenWebServer() => Application.OpenURL(@"http://game.ispu.ru/insight");

        [MenuItem("Insight/Web Server Data")]
        private static void OpenWebServerData() =>
            Application.OpenURL(
                @"https://docs.google.com/document/d/1nuJlcdZBYkC0vmMOuB6agggovgX-mw86mkwvrGa48k8/edit?pli=1#heading=h.3ghstexxheys");

        [MenuItem("Insight/PhpMyAdmin")]
        private static void OpenPhpAdmin() =>
            Application.OpenURL(
                @"http://game.ispu.ru:8080/pma/sql.php?db=16_game1&table=d_data&token=0bc4aceff5c3d91ec953cc8258fea3bf&pos=0");

        [MenuItem("Insight/Google Sheets")]
        private static void OpenGoogleSheets() =>
            Application.OpenURL(
                @"https://docs.google.com/spreadsheets/d/1b5Ak77i6ubJFIcFagXtlwf2mwrYZrXJ3qOPp5c85NgQ");

        [MenuItem("Insight/ToggleUnityDebugger")]
        private static void ToggleUnityDebugger() => Debug.unityLogger.logEnabled = !Debug.unityLogger.logEnabled;

        /*
        private static void ShowWindow()
        {
            var window = GetWindow<InsightTools>();
            window.titleContent = new GUIContent("TITLE");
            window.Show();
        }*/
    }
}