using UnityEngine;

public class InputController : MonoBehaviour {
    [SerializeField] new Camera camera;
    [SerializeField] Pool rectanglePool; 
    [SerializeField] Pool connectionPool;
    [SerializeField] LayerMask rectangleMask;

    CommandsController _commandsController;
    EnvironmentProbe _environmentProbe;

    void Start() {
        _environmentProbe = new EnvironmentProbe(rectangleMask);
        _commandsController = new CommandsController(rectanglePool, connectionPool, _environmentProbe);
    }

    void Update() {
        _commandsController.UpdateTime(Time.deltaTime);
        
        var position = camera.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0)) {
            var target = _environmentProbe.GetTarget(camera.ScreenToWorldPoint(Input.mousePosition));
            _commandsController.OnButtonDown(position, target);
            return;
        }
        
        if (Input.GetMouseButtonUp(0)) {
            _commandsController.OnButtonUp(position);
            return;
        }

        if (Input.GetMouseButton(0)) {
            _commandsController.OnDrag(position);
        }
    }
}
