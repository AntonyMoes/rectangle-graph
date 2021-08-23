using UnityEngine;

namespace Commands {
    public interface ICommand {
        void OnButtonDown(Vector2 position);
        void OnButtonUp(Vector2 position);
        void OnDrag(Vector2 position);
    }
}
