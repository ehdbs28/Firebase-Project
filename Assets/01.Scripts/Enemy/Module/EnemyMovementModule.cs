using UnityEngine;

public class EnemyMovementModule : BaseModule<EnemyController>
{
    private Rigidbody2D _rigidbody;
    
    public EnemyMovementModule(EnemyController controller) : base(controller)
    {
        _rigidbody = Controller.GetComponent<Rigidbody2D>();
    }

    public override void UpdateModule()
    {
        Movement();
    }

    public override void FixedUpdateModule()
    {
    }

    private void Movement()
    {
        var movementDir = (Controller.Target.transform.position - Controller.transform.position).normalized;
        _rigidbody.velocity = movementDir * Controller.Data.movementSpeed;
    }
}