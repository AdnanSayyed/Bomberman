using System.Collections.Generic;
using UnityEngine;
using LevelSystem;
using Common;
using System.Collections;

namespace EnemySystem
{
    /// <summary>
    /// Responsible for enemy movement,animations
    /// </summary>
    public class Enemy : MonoBehaviour, IDamage
    {
        #region Visible in Inspector fields

        [Tooltip("Enemy move speed")]
        [SerializeField] private float moveSpeed = 5f;

        [Tooltip("Enemy animator ref")]
        [SerializeField] private Animator enemyAnimator;

        #endregion

        #region private fields

        LevelManager levelManager;
        Vector3 currentGrid, nextGrid;
        List<Vector3> gridPositions;
        float startTime, currentTime, totalDistance;
        EnemyManager enemyManager;
        bool canMove, isStuck = true;
        WaitForSeconds waitTime = new WaitForSeconds(1f);

        #endregion


        // Start is called before the first frame update
        void Start()
        {
            startTime = Time.time;
            currentGrid = nextGrid = transform.position;
            canMove = CanMove();
            if (canMove == true)
            {
                isStuck = false;
                GetNextGrid();
                totalDistance = Vector3.Distance(currentGrid, nextGrid);
            }
            else
            {
                StartCoroutine(CheckForStuck());
            }
        }

        public void SetData(LevelManager levelManager, EnemyManager enemyManager)
        {
            this.enemyManager = enemyManager;
            this.levelManager = levelManager;
        }

        // Update is called once per frame
        void Update()
        {
            if (isStuck == false)
                Move();
        }

        /// <summary>
        /// check for movement in all directions
        /// </summary>
        /// <returns></returns>
        bool CanMove()
        {
            gridPositions = new List<Vector3>();

            CheckAvailableDirection(Vector3.up);
            CheckAvailableDirection(Vector3.down);
            CheckAvailableDirection(Vector3.left);
            CheckAvailableDirection(Vector3.right);

        
            if (gridPositions.Count > 0)
            {
                return true;
            }
            return false;
        }


        void GetNextGrid()
        {
            int val = Random.Range(0, gridPositions.Count);
            nextGrid = gridPositions[val];
            UpdateSpriteDirection(currentGrid, nextGrid);
        }

        /// <summary>
        /// check for available movement direction
        /// 
        /// </summary>
        /// <param name="direction">direction to check</param>
        private void CheckAvailableDirection(Vector3 direction)
        {
            GameObject obj = levelManager.GetObjAtGrid(transform.position + direction);
            if (obj == null || obj.GetComponent<Enemy>() != null)
            {
                //if direction is available add that pos in gridpos list
                gridPositions.Add(transform.position + direction);
            }
        }

        /// <summary>
        /// enemy movement
        /// continuously calling from update if can move
        /// </summary>
        void Move()
        {
            if (Vector3.Distance(transform.position, nextGrid) > 0.1f)
            {
                currentTime = Time.time;
                float distanceCovered = (currentTime - startTime) * moveSpeed;
                float fraction = distanceCovered / totalDistance;
                transform.position = Vector3.Lerp(currentGrid, nextGrid, fraction);
            }
            else
            {
                transform.position = nextGrid;
                startTime = Time.time;
                currentGrid = nextGrid;
                canMove = CanMove();
                if (canMove)
                {
                    GetNextGrid();
                    totalDistance = Vector3.Distance(currentGrid, nextGrid);
                }
            }
        }

        /// <summary>
        /// Running continueos to check for stuck
        /// </summary>
        /// <returns></returns>
        IEnumerator CheckForStuck()
        {
            yield return waitTime;

            if (CanMove())
            {
                isStuck = false;
                yield return null;
            }

            //again starting same coroutine
            StartCoroutine(CheckForStuck());
        }

        public void Damage()
        {
            levelManager.EmptyGrid(currentGrid);
            enemyManager.RemoveEnemy(this);
            Destroy(gameObject);
        }

        /// <summary>
        /// updates the enemy sprite directions
        /// </summary>
        /// <param name="startPos">current position</param>
        /// <param name="endPos">next position</param>
        void UpdateSpriteDirection(Vector2 startPos, Vector2 endPos)
        {
            Vector2 diff = endPos - startPos;
            float zRot = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            switch (zRot + 90)
            {
                case 0:
                    enemyAnimator.Play("EnemyWalk_Front");
                    break;
                case 90:
                    enemyAnimator.Play("EnemySideWalk_Right");
                    break;
                case 180:
                    enemyAnimator.Play("EnemyWalk_Back");
                    break;
                case 270:
                    enemyAnimator.Play("EnemySideWalk_Left");
                    break;
            }
        }

    }
}