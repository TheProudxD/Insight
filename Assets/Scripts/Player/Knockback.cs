using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    private float _thrust = 4f;
    private float _knockTime = 0.4f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Enemy enemy))
        {
            var enemyRB = enemy.gameObject.GetComponent<Rigidbody2D>();
            try
            {
                enemy.CurrentState = EnemyState.Stagger;

                Vector2 difference = enemyRB.transform.position - transform.position;
                difference = difference.normalized * _thrust;
                enemyRB.AddForce(difference, ForceMode2D.Impulse);

                StartCoroutine(KnockCO(enemyRB));

                //print($"Thrust {_thrust}");
            }
            catch (System.Exception ex)
            {
                throw new System.NullReferenceException(ex.Message);
            }
        }
    }
    private IEnumerator KnockCO(Rigidbody2D enemyRB)
    {
        if (enemyRB.TryGetComponent(out Enemy enemy))
        {
            yield return new WaitForSeconds(_knockTime);
            enemyRB.velocity = Vector2.zero;
            enemy.CurrentState = EnemyState.Idle;
        }
    }
}
