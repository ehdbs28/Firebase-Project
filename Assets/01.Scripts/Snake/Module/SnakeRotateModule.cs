using UnityEngine;

public class SnakeRotateModule : BaseModule<SnakeController>
{
    private Vector2 _mouseDir;
    private Transform[] _eyeTransforms;

    public SnakeRotateModule(SnakeController controller) : base(controller)
    {
        _eyeTransforms = new Transform[2];
        _eyeTransforms[0] = controller.transform.Find("Visual/RightEye");
        _eyeTransforms[1] = controller.transform.Find("Visual/LeftEye");
    }

    public override void UpdateModule()
    {
        CalcDir();
        Rotate();
    }

    public override void FixedUpdateModule()
    {
    }

    private void CalcDir()
    {
        var mousePos = Controller.InputControl.MouseWorldPosition;
        _mouseDir = (mousePos - Controller.transform.position).normalized;
    }

    private void Rotate()
    {
        var angle = Mathf.Atan2(_mouseDir.y, _mouseDir.x) * Mathf.Rad2Deg - 90f;
        var destRotate = Quaternion.AngleAxis(angle, Vector3.forward);
        var lerpRotate = Quaternion.Lerp(Controller.transform.rotation, destRotate, Time.deltaTime * Controller.Data.rotateSpeed);

        foreach (var eyeTrm in _eyeTransforms)
        {
            eyeTrm.rotation = destRotate;
        }
        
        Controller.transform.rotation = lerpRotate;
    }
}