using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerPrefsEditor : MonoBehaviour
{
    [MenuItem("Custom Tools/Delete PlayerPrefs", false, 10)]
    static void DeletePlayerPrefs(MenuCommand menuCommand)
    {
        PlayerPrefs.DeleteAll();
    }
}
