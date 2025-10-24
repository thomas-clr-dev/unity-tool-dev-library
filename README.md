# Vahartzia Unity Tool Dev Library

### Enable If
Allow to disable a field on certain conditions
```cs
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Vhrtz.EditorOnly
{
    [CustomPropertyDrawer(typeof(EnableIfAttribute))]
    public class EnableIfPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EnableIfAttribute enableIfAttribute = attribute as EnableIfAttribute;
            SerializedProperty activatorProperty = property.serializedObject.FindProperty(enableIfAttribute.FieldName);
            bool isEnabled = false;

            if (activatorProperty != null)
            {
                isEnabled = activatorProperty.boolValue;
            }
            else
            {
                bool didFindValue = false;
                foreach (FieldInfo fieldInfo in property.serializedObject.targetObject.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance))
                {
                    if (fieldInfo.Name == enableIfAttribute.FieldName)
                    {
                        isEnabled = (bool)fieldInfo.GetValue(property.serializedObject.targetObject);
                        didFindValue = true;
                        break;
                    }
                }

                if (!didFindValue)
                {
                    foreach (PropertyInfo propertyInfo in property.serializedObject.targetObject.GetType().GetProperties(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance))
                    {
                        if (propertyInfo.Name == enableIfAttribute.FieldName)
                        {
                            isEnabled = (bool)propertyInfo.GetValue(property.serializedObject.targetObject);
                            didFindValue = true;
                            break;
                        }
                    }
                }

                if (!didFindValue)
                {
                    Debug.LogWarning("Missing value " + enableIfAttribute.FieldName);
                }
            }

            GUI.enabled = isEnabled;
            EditorGUI.PropertyField(position, property, label);
            GUI.enabled = true;
        }
    }
}

```

### Random Spawner In Editor Box
Allaow to have a box window to spawn a prefab in a certain quantity in a certain zone
```cs
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

```

### In Player GUI Debug Display
Allow to display a debug box with data in player.
```cs
using UnityEngine;

namespace Vhrtz
{
    [RequireComponent(typeof(Rigidbody))]
    [..............]
            [Header("Debug")]

        [SerializeField]
        private bool _showDebugGUI = false;

        private Rigidbody _rigidbody = null;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();

        }
        public void Jump()
        {
            Debug.Log("SAUUUTE !!!!");
        }

        private void OnGUI()
        {
            if (!_showDebugGUI)
            {
                return;
            }

            float speed = _rigidbody.linearVelocity.magnitude;
            using (new GUILayout.VerticalScope(GUI.skin.box))
            {
                using (new GUILayout.HorizontalScope())
                {
                    GUILayout.Label("Player speed :");
                    GUILayout.Label(speed.ToString("0.000"));
                }
            }
        }
    }
}
```
