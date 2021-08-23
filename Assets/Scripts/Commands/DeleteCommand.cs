using Entities;
using UnityEngine;
using Utils;

namespace Commands {
    public class DeleteCommand : ICommand {
        readonly Either<Rectangle, Connection> _target;

        public DeleteCommand(Either<Rectangle, Connection> target) {
            _target = target;
        }
        
        public void OnButtonDown(Vector2 position) {
            _target.Get(
                rectangle => {
                    foreach (var connection in rectangle.Connections) {
                        var otherRectangle = connection.Target1 == rectangle ? connection.Target2 : connection.Target1;
                        otherRectangle.Connections.Remove(connection);
                        connection.CleanUp();
                    }
                    
                    rectangle.Connections.Clear();
                    rectangle.CleanUp(); 
                },
                connection =>  {
                    connection.Target1.Connections.Remove(connection);
                    connection.Target2.Connections.Remove(connection);
                    
                    connection.CleanUp(); 
                });
        }

        public void OnButtonUp(Vector2 position) { }

        public void OnDrag(Vector2 position) { }
    }
}
