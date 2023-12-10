using UnityEngine;

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
            var destroyEffect = PoolManager.Instance.Pop("DestroyEffect") as PoolableParticle;
            destroyEffect.SetPositionAndRotation(Controller.transform.position);
            destroyEffect.Play();
            
            ScoreManager.Instance.ScoreUp(Random.Range(3, 6));
            StageManager.Instance.EnemyBuilder.RemoveEnemy(Controller);
        }
    }
}