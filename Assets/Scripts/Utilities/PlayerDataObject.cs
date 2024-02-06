using UnityEditor;
using UnityEngine;

public class PlayerDataObject : ScriptableObject
{
    public string PlayerName;
    public float[] RecordTime ;
    public int SceneIndex;

    public void UpdateData(PlayerData.Data data)
    {
        PlayerName = data.PlayerName; 
        RecordTime = data.RecordTime;
    }

#if UNITY_EDITOR
    [MenuItem("Assets/Create/Player Data")]
    public static void CreateMyAsset()
    {
        PlayerDataObject asset = CreateInstance<PlayerDataObject>();

        ProjectWindowUtil.CreateAsset(asset, "Player Data.asset");
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();

        Selection.activeObject = asset;
    }
#endif
    public string ToJson()
    {
        return JsonUtility.ToJson(this, true);
    }
}
