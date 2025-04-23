using UnityEngine;
using UnityEditor;

public class ReplaceEmpties : EditorWindow
{
    public GameObject prefab;
    private GameObject[] empties;

    [MenuItem("Tools/Replace Empties with Prefab")]
    public static void ShowWindow()
    {
        GetWindow<ReplaceEmpties>("Replace Empties with Prefab");
    }

    private void OnGUI()
    {
        prefab = (GameObject)EditorGUILayout.ObjectField("Prefab", prefab, typeof(GameObject), false);

        if (GUILayout.Button("Replace Selected Empties"))
        {
            ReplaceSelectedEmpties();
        }
    }

    private void ReplaceSelectedEmpties()
    {
        if (prefab == null)
        {
            Debug.LogError("Please assign a prefab to replace the empty objects.");
            return;
        }

        empties = Selection.gameObjects;

        foreach (GameObject empty in empties)
        {
            if (empty != null && empty.transform.childCount == 0)
            {
                Transform emptyTransform = empty.transform;

                //Instantiate the prefab as a sibling (keeping hierarchy)
                GameObject newObject = (GameObject)PrefabUtility.InstantiatePrefab(prefab, empty.scene);
                if (newObject != null)
                {
                    Transform newTransform = newObject.transform;

                    newTransform.SetParent(emptyTransform.parent, false);
                    newTransform.position = emptyTransform.position;
                    newTransform.rotation = emptyTransform.rotation;
                    newTransform.localScale = emptyTransform.localScale;

                    newObject.name = prefab.name;
                    Undo.RegisterCreatedObjectUndo(newObject, "Replace Empty With Prefab");
                    Undo.DestroyObjectImmediate(empty);
                }
            }
            else
            {
                Debug.LogWarning($"{empty.name} is not an empty GameObject or has children. Skipped.");
            }
        }
    }
}