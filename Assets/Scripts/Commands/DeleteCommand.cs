using UnityEngine;

namespace Commands {
    public class DeleteCommand : ICommand {
        readonly GameObject _target;
        readonly Pool _rectanglePool;
        readonly Pool _connectionPool;

        public DeleteCommand(GameObject target, Pool rectanglePool, Pool connectionPool) {
            _target = target;
            _rectanglePool = rectanglePool;
            _connectionPool = connectionPool;
        }
        
        public void OnButtonDown(Vector2 position) {
            if (_target.TryGetComponent(out Rectangle rectangle)) {
                foreach (var connection in rectangle.Connections) {
                    var otherRectangle = connection.target1 == rectangle ? connection.target2 : connection.target1;
                    otherRectangle.Connections.Remove(connection);
                    _connectionPool.Release(connection.gameObject);
                }
                
                rectangle.Connections.Clear();
                _rectanglePool.Release(_target);
            } else if (_target.TryGetComponent(out Connection connection)) {
                connection.target1.Connections.Remove(connection);
                connection.target2.Connections.Remove(connection);
                
                _connectionPool.Release(_target);
            }
        }

        public void OnButtonUp(Vector2 position) { }

        public void OnDrag(Vector2 position) { }
    }
}
