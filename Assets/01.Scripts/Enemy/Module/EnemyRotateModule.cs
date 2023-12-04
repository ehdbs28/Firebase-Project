using UnityEngine;

public class EnemyRotateModule : BaseModule<EnemyController>
{
    public EnemyRotateModule(EnemyController controller) : base(controller)
    {
    }

    public override void UpdateModule()
    {
        Rotate();
    }

    public override void FixedUpdateModule()
    {
    }

    private void Rotate()
    {
        var lookDir = (Controller.Target.transform.position - Controller.transform.position).normalized;
        var angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        Controller.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}