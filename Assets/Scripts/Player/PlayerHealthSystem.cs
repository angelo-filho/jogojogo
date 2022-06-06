using System.Collections;
using UnityEngine;

public class PlayerHealthSystem : HealthSystem
{
    [SerializeField] private float invencibleTime = 1f;
    private bool invencible;
    private HealthBar healthBar;

    protected override void Awake() 
    {
        base.Awake();

        healthBar = FindObjectOfType<HealthBar>();    
        healthBar.StartHealthBar(maxLife);
    }

    public override void TakeDamage(float damage)
    {
        if (invencible)
        {
            return;
        }

        base.TakeDamage(damage);

        healthBar.UpdateHealthBar(life);

        StartCoroutine(TurnVulnerable());
    }

    private IEnumerator TurnVulnerable()
    {
        invencible = true;

        yield return new WaitForSeconds(invencibleTime);

        invencible = false;
    }
}
