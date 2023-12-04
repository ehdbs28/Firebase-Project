using UnityEngine;

public class SnakeRotateModule : BaseModule<SnakeController>
{
    private Vector2 _rotateDir;
    private Transform[] _eyeTransforms;
    private Transform _tankTransform;

    public SnakeRotateModule(SnakeController controller) : base(controller)
    {
        _eyeTransforms = new Transform[2];
        _eyeTransforms[0] = controller.transform.Find("Visual/RightEye");
        _eyeTransforms[1] = controller.transform.Find("Visual/LeftEye");
        _tankTransform = controller.transform.Find("Visual/Tank");
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
        var destPos = Controller.IsHead ? Controller.InputControl.MouseWorldPosition : Controller.Parent.transform.position;
        _rotateDir = (destPos - Controller.transform.position).normalized;
    }

    private void Rotate()
    {
        var angle = Mathf.Atan2(_rotateDir.y, _rotateDir.x) * Mathf.Rad2Deg - 90f;
        var destRotate = Quaternion.AngleAxis(angle, Vector3.forward);
        var lerpRotate = Quaternion.Lerp(Controller.transform.rotation, destRotate, Time.deltaTime * Controller.Data.rotateSpeed);

        if (Controller.IsHead)
        {
            foreach (var eyeTrm in _eyeTransforms)
            {
                eyeTrm.rotation = destRotate;
            }
        }
        else
        {
            var dir = Controller.GetModule<SnakeAttackModule>().AttackDir;
            angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;
            _tankTransform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        
        Controller.transform.rotation = lerpRotate;
    }
}