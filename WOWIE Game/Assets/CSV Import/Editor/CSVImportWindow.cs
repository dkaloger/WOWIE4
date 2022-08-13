using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;


public class CSVImportWindow : EditorWindow
{
    private string _csvPath;
    private string[] _csvRows;

    private string _dialogueFolder;
    private void OnGUI()
    {
        if (GUILayout.Button("Import CSV File"))
        {
            _csvPath = EditorUtility.OpenFilePanel("Import Dialogue CSV", "", "csv");
        }

        if (_csvPath == null)
        {
            EditorGUILayout.LabelField("No CSV Loaded, please grab a CSV file");
            return;
        }
        
        EditorGUILayout.LabelField($"Using {_csvPath}");
        
        if (GUILayout.Button("Export to folder"))
        {
            _dialogueFolder = EditorUtility.OpenFolderPanel("Output", "", "");

            _csvRows = File.ReadAllText(_csvPath).Split('\n', StringSplitOptions.RemoveEmptyEntries);

            var folder = _dialogueFolder.Replace(Application.dataPath, "Assets");
            Debug.Log(folder);
            
            foreach (var row in _csvRows)
            {
                var data = Regex.Matches(row, @"(?<=^|,)((""[^""]*"")|([^,]*))(?=$|,)").Select(d => d.Value).ToList();
                var name = data.First();
                data.RemoveAt(0);

                var found = AssetDatabase.LoadAssetAtPath<DialogueScriptableObject>(folder + Path.DirectorySeparatorChar + name + ".asset");
                if (found != null)
                    found.Pages = data;
                else
                {
                    var newObj = CreateInstance<DialogueScriptableObject>();
                    newObj.name = name;
                    newObj.Pages = data;
                    AssetDatabase.CreateAsset(newObj, folder + Path.DirectorySeparatorChar + name + ".asset");
                }

            }
        }
    }

    [MenuItem("Window/Dialogue Import")]
    public static void ShowWindow()
    {
        GetWindow<CSVImportWindow>(false, "Dialogue Import").Show();
    }

}
