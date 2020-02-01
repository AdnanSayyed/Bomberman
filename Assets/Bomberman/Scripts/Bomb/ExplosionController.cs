using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

/// <summary>
/// Explosion damage to player if present in range
/// </summary>
public class ExplosionController : MonoBehaviour
{
    #region Visible in Inspector fields

    [Tooltip("Explosion collider ref")]
    [SerializeField] private Collider2D colliderRef;

    [Tooltip ("Timer after which explosion destroys")]
    [SerializeField] private float ExplosionObjDestroyTimer = 0.4f;

    #endregion

    private void Start()
    {
        Invoke("DestroyObj", ExplosionObjDestroyTimer);
    }

    /// <summary>
    /// Removes explosion
    /// </summary>
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
