using UnityEngine;

namespace Utils {
    [RequireComponent(typeof(SpriteRenderer), typeof(Collider2D))]
    public class ObjectAdapter : MonoBehaviour {
        [SerializeField] new SpriteRenderer renderer;
        [SerializeField] new BoxCollider2D collider;

        public Vector2 Position {
            get => transform.position;
            set => transform.position = new Vector3(value.x, value.y, transform.position.z);
        }

        public Vector2 Right {
            get => transform.right;
            set => transform.right = new Vector3(value.x, value.y, transform.right.z);
        }

        public Vector2 Size {
            get => renderer.size;
            set {
                renderer.size = value;
                collider.size = value;
            }
        }

        public Color Color {
            get => renderer.color;
            set => renderer.color = value;
        }

        public object UsedBy;
    }
}
