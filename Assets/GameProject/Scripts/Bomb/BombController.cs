using UnityEngine;
using LevelSystem;
using Common;

public class BombController : MonoBehaviour
{
    LevelManager levelManager;

    [SerializeField] private int explosionArea = 0;
    [SerializeField] private GameObject explosionObj;
    [SerializeField] private int explosionTime = 3;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("Explode", explosionTime);
        GameManager.Instance.restartGame += RestartGame;
    }

    private void OnDisable()
    {
        GameManager.Instance.restartGame -= RestartGame;
    }

    void RestartGame()
    {
        Destroy(gameObject);
    }

    public void SetLevelManager(LevelManager levelManager)
    {
        this.levelManager = levelManager;
        this.levelManager.FillGrid(transform.position, this.gameObject);
    }

    void Explode()
    {
        ExplodeCell(transform.position, 5, Vector3.zero);
        ExplodeCell(transform.position, 0, Vector3.up);
        ExplodeCell(transform.position, 0, Vector3.left);
        ExplodeCell(transform.position, 0, Vector3.right);
        ExplodeCell(transform.position, 0, Vector3.down);

        levelManager.EmptyGrid(transform.position);
        Destroy(gameObject);
    }

    void ExplodeCell(Vector3 position, int areaCovered , Vector3 explosionDirection)
    {
        int area = areaCovered;
        Vector2 targetPos = position + explosionDirection;
        GameObject obj = levelManager.GetObjAtGrid(targetPos);

        area++;
        if (obj != null)
        {
            if (obj.GetComponent<FixedBlock>() != null) return;
            else
            {
                Instantiate(explosionObj, targetPos, Quaternion.identity);
                levelManager.EmptyGrid(position);

                if (area < explosionArea)
                {
                    ExplodeCell(transform.position + explosionDirection, area, explosionDirection);
                }
            }
        }
        else
        {
            Instantiate(explosionObj, targetPos, Quaternion.identity);
            if (area < explosionArea)
            {
                ExplodeCell(transform.position + explosionDirection, area, explosionDirection);
            }
        }

    }

}
