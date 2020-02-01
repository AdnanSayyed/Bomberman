using UnityEngine;
using LevelSystem;
using Common;

/// <summary>
/// Controls destruction by bomb explosion
/// </summary>
public class BombController : MonoBehaviour
{
    #region Visible in Inspector fields

    [Tooltip("Area affecting by explosion")]
    [SerializeField] private int explosionArea = 0;

    [Tooltip("Explosion effect gameobject")]
    [SerializeField] private GameObject explosionObj;

    [Tooltip("Time to explosion")]
    [SerializeField] private int explosionTime = 3;

    #endregion

    LevelManager levelManager;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("ExplodeBomb", explosionTime);
        GameManager.Instance.restartGame += RestartGame; 
    }

    private void OnDisable()
    {
        if(GameManager.Instance!=null)
            GameManager.Instance.restartGame -= RestartGame;
    }

    /// <summary>
    /// Destroys if present at the time of restart game
    /// </summary>
    void RestartGame()
    {
        Destroy(gameObject);
    }

    public void SetLevelManager(LevelManager levelManager)
    {
        this.levelManager = levelManager;
        this.levelManager.FillGrid(transform.position, this.gameObject);
    }

    /// <summary>
    /// areas where explosion will affect
    /// </summary>
    void ExplodeBomb()
    {
        ExplodeCell(transform.position, 5, Vector3.zero);
        ExplodeCell(transform.position, 0, Vector3.up);
        ExplodeCell(transform.position, 0, Vector3.left);
        ExplodeCell(transform.position, 0, Vector3.right);
        ExplodeCell(transform.position, 0, Vector3.down);

        levelManager.EmptyGrid(transform.position);
        Destroy(gameObject);
    }

    /// <summary>
    /// Cell to explode
    /// </summary>
    /// <param name="position">start pos</param>
    /// <param name="areaCovered">area to be covered</param>
    /// <param name="explosionDirection">direction of explosion</param>
    void ExplodeCell(Vector3 position, int areaCovered , Vector3 explosionDirection)
    {
        int area = areaCovered;
        Vector2 targetPos = position + explosionDirection;
        GameObject obj = levelManager.GetObjAtGrid(targetPos); //gives obj at that pos

        area++;
        if (obj != null)
        {
            if (obj.GetComponent<FixedBlock>() != null) return;
            else
            {
                Instantiate(explosionObj, targetPos, Quaternion.identity); //explosion prefab at target pos
                levelManager.EmptyGrid(position); //remove grid from that pos

                if (area < explosionArea)
                {
                    ExplodeCell(transform.position + explosionDirection, area, explosionDirection);
                }
            }
        }
        else
        {
            //explosion at empty area, wont affect any object
            Instantiate(explosionObj, targetPos, Quaternion.identity);
            if (area < explosionArea)
            {
                ExplodeCell(transform.position + explosionDirection, area, explosionDirection);
            }
        }

    }

}
