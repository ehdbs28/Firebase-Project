public class EnemyHealthModule : BaseModule<EnemyController>
{
    private int _currentHealth;
    
    public EnemyHealthModule(EnemyController controller) : base(controller)
    {
        _currentHealth = Controller.Data.maxHealth;
    }

    public override void UpdateModule()
    {
    }

    public override void FixedUpdateModule()
    {
    }

    public void OnDamage()
    {
        _currentHealth -= 1;
        
        if (_currentHealth <= 0)
        {
            StageManager.Instance.Builder.RemoveEnemy(Controller);
            PoolManager.Instance.Push(Controller);
        }
    }
}