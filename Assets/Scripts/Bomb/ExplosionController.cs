using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

/// <summary>
/// Explosion damage to player if present in range
/// </summary>
public class ExplosionController : MonoBehaviour
{
    [SerializeField] private Collider2D colliderRef;
    float spawnTime = 0;

    private void Start()
    {
        Invoke("DestroyObj", 0.4f);
    }

    void DestroyObj()
    {
        Destroy(gameObject);
    }

    /// <summary>
    /// give damage to player if is in range
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<IDamage>() != null)
        {
            GameManager.Instance.playerPrefab.PlayerDied = true;
            collision.GetComponent<IDamage>().Damage();
        }
    }
}
