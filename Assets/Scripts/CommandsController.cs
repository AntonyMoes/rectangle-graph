using Commands;
using UnityEngine;

public class CommandsController {
    readonly Pool _rectanglePool;
    readonly Pool _connectionPool;
    readonly EnvironmentProbe _environmentProbe;

    const float DoubleClickTime = 0.4f;
    const float DoubleClickPositionDelta = 0.1f;
    
    float _timeSinceLastClick = DoubleClickTime;
    Vector2 _lastClickPosition;
    
    ICommand _currentCommand;

    public CommandsController(Pool rectanglePool, Pool connectionPool, EnvironmentProbe environmentProbe) {
        _rectanglePool = rectanglePool;
        _connectionPool = connectionPool;
        _environmentProbe = environmentProbe;
    }

    public void OnButtonDown(Vector2 position, GameObject target) {
        // Debug.Log($"Target is null: {target is null}; position: {position}\nSince last click: {_timeSinceLastClick}, {_timeSinceLastClick < DoubleClickTime}; Last click position: {_lastClickPosition}");
        if (!target) {
            _currentCommand = new CreateRectangleCommand(_rectanglePool, _environmentProbe);
        } else if (_timeSinceLastClick < DoubleClickTime &&
                   (position - _lastClickPosition).magnitude < DoubleClickPositionDelta) {
            _currentCommand = new DeleteCommand(target, _rectanglePool, _connectionPool);
            _timeSinceLastClick = DoubleClickTime;
        } else {
            if (target.TryGetComponent(out Rectangle rectangle)) {
                _currentCommand = new DragRectangleCommand(rectangle, _connectionPool, _environmentProbe);
            }
            _timeSinceLastClick = 0;
        }

        _lastClickPosition = position;
        _currentCommand?.OnButtonDown(position);
    }

    public void OnDrag(Vector2 position) {
        _currentCommand?.OnDrag(position);
    }

    public void OnButtonUp(Vector2 position) {
        _currentCommand?.OnButtonUp(position);
        _currentCommand = null;
    }

    public void UpdateTime(float deltaTime) {
        _timeSinceLastClick = Mathf.Min(DoubleClickTime, _timeSinceLastClick + deltaTime);
    }
}
