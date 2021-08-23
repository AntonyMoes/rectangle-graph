using Entities;
using UnityEngine;
using Utils;

namespace Commands {
    public class DeleteCommand : ICommand {
        readonly GameObject _target;

        public DeleteCommand(GameObject target) {
            _target = target;
        }
        
        public void OnButtonDown(Vector2 position) {
            if (!_target) {
                return;
            }
            
            var adapter = _target.GetComponent<ObjectAdapter>();
            if (!adapter) {
                return;
            }

            if (adapter.UsedBy is Rectangle rectangle) {
                foreach (var connection in rectangle.Connections) {
                    var otherRectangle = connection.Target1 == rectangle ? connection.Target2 : connection.Target1;
                    otherRectangle.Connections.Remove(connection);
                    connection.CleanUp();
                }
                
                rectangle.Connections.Clear();
                rectangle.CleanUp();
            } else if (adapter.UsedBy is Connection connection) {
                connection.Target1.Connections.Remove(connection);
                connection.Target2.Connections.Remove(connection);
                
                connection.CleanUp();
            }
        }

        public void OnButtonUp(Vector2 position) { }

        public void OnDrag(Vector2 position) { }
    }
}
