using UnityEditor;
using UnityEngine;

namespace Vhrtz.EditorOnly
{
    [CustomEditor(typeof(Player))]
    public class PlayerEditor : Editor
    {
        override public void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GUI.enabled = Application.isPlaying;
            GUILayout.Space(5);
            if (GUILayout.Button("Make player JUMP !!"))
            {
                Player player = target as Player;
                player.Jump();
            }
            GUILayout.Space(5);
            GUI.enabled = true;
        }
    }
}
