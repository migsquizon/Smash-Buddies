using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/EnemyStats")]
public class EnemyStats : ScriptableObject
{
    public float health = 100f;
    public int attackDamage = 1;
    public float attackRange = 1f;
    public float attackRate = 1f;
    public float lookRange = 5f;
    public float lookSphereCastRadius = 1f;
    public float moveSpeed = 10f;
}
