using UnityEngine;

namespace Vhrtz
{
    [RequireComponent(typeof(Rigidbody))]
    public class Player : MonoBehaviour
    {
        [SerializeField]
        private bool _canAttack = true;

        [SerializeField, EnableIf(nameof(_canAttack))]
        private float _attackRange = 4.0f;

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
