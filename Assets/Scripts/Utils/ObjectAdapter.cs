using UnityEngine;

namespace Utils {
    [RequireComponent(typeof(SpriteRenderer), typeof(Collider2D))]
    public class ObjectAdapter : MonoBehaviour {
        [SerializeField] new SpriteRenderer renderer;

        public Vector2 Position {
            get => transform.position;
            set => transform.position = new Vector3(value.x, value.y, transform.position.z);
        }

        public Vector2 Right {
            get => transform.right;
            set => transform.right = new Vector3(value.x, value.y, transform.right.z);
        }

        public Vector2 Size {
            get => transform.localScale;
            set => transform.localScale = new Vector3(value.x, value.y, transform.localScale.z);
        }

        public Color Color {
            get => renderer.color;
            set => renderer.color = value;
        }

        public object UsedBy;
    }
}
