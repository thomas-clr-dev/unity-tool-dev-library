using UnityEditor;
using UnityEngine;

namespace Vhrtz.EditorOnly
{
    public class RandomSpawnerEditorWindow : EditorWindow
    {
        [SerializeField]
        private GameObject _prefabToSpawn = null;

        [SerializeField]
        private int _quantity = 100;

        [SerializeField]
        private Vector3 _spawnArea = Vector3.one * 100;

        [MenuItem("Tools/Random Spawner")]
        public static void OpenWindow()
        {
            RandomSpawnerEditorWindow window = GetWindow<RandomSpawnerEditorWindow>("Random Spawner");
            window.Show();
        }

        private void OnGUI()
        {
            _prefabToSpawn = EditorGUILayout.ObjectField("Prefab To Spawn", _prefabToSpawn, typeof(GameObject), false) as GameObject;
            _quantity = EditorGUILayout.IntField("Quantity", _quantity);
            _spawnArea = EditorGUILayout.Vector3Field("Spawn Area", _spawnArea);

            GUILayout.Space(5);
            if (GUILayout.Button("Spawn Objects Randomly"))
            {
                for(int i = 0; i < _quantity; i++)
                {
                    Vector3 randomPosition = new Vector3
                    (
                        Random.Range(-_spawnArea.x / 2, _spawnArea.x / 2),
                        Random.Range(-_spawnArea.y / 2, _spawnArea.y / 2),
                        Random.Range(-_spawnArea.z / 2, _spawnArea.z / 2)
                    );
                    Instantiate(_prefabToSpawn, randomPosition, Random.rotation);
                }
            }
        }
    }
}
