using UnityEngine;
using Common;
using EnemySystem;

namespace PlayerSystem
{
    /// <summary>
    /// Responsible for player movement,animations,bombing
    /// </summary>
    public class Player :MonoBehaviour, IDamage
    {
        #region Visible in Inspector fields

        [Tooltip("Player movement speed")]
        [SerializeField] private float moveSpeed;

        [Tooltip("Player rigidbody ref")]
        [SerializeField] private Rigidbody2D playerRigidbody;

        [Tooltip("Player animator ref")]
        [SerializeField] private Animator playerAnimator;

        #endregion

        #region private fields

        private PlayerController playerController;
        private float horizontalSpeed, verticalSpeed;
        private bool isDead;

        #endregion

        /// <summary>
        /// getting and setting player status value where necessary
        /// </summary>
        public bool PlayerDied
        {
            get
            {
                return isDead;
            }
            set
            {
                isDead = value;
            }
        }

        private void Start()
        {
            playerAnimator.Play("Idle");
            horizontalSpeed = verticalSpeed = 0;
        }

        private void Update()
        {
            MoveDirection();
            if (Input.GetKeyDown(KeyCode.Space)) SpawnBomb();
        }

       
        /// <summary>
        /// player movement 
        /// </summary>
        private void MoveDirection()
        {
            horizontalSpeed = Input.GetAxis("Horizontal");
            verticalSpeed = Input.GetAxis("Vertical");

            if (Mathf.Abs(horizontalSpeed) > Mathf.Abs(verticalSpeed))
            {
                if (horizontalSpeed > 0 )
                {
                    playerAnimator.Play("PlayerSideWalk_Right");
                    if (!isAnimatorPlaying())
                    {
                        //restarting animation
                        playerAnimator.Play("PlayerSideWalk_Right", -1, 0f);
                    }
                }
                else if (horizontalSpeed < 0) 
                {
                    playerAnimator.Play("PlayerSideWalk_Left");
                    if (!isAnimatorPlaying())
                    {
                        //restarting animation
                        playerAnimator.Play("PlayerSideWalk_Left", -1, 0f);
                    }
                }
                
                verticalSpeed = 0;
            }
            if (Mathf.Abs(verticalSpeed) > Mathf.Abs(horizontalSpeed))
            {
                if (verticalSpeed > 0 )
                {
                    playerAnimator.Play("PlayerWalk_Back");
                    if (!isAnimatorPlaying())
                    {
                        //restarting animation
                        playerAnimator.Play("PlayerWalk_Back", -1, 0f);
                    }
                }
                else if (verticalSpeed < 0 )
                {
                    playerAnimator.Play("PlayerWalk_Front");
                    if (!isAnimatorPlaying())
                    {
                        //restarting animation
                        playerAnimator.Play("PlayerWalk_Front", -1, 0f);
                    }
                }
                horizontalSpeed = 0;
            }
        }

        /// <summary>
        /// check if animator is playing animation
        /// </summary>
        /// <returns></returns>
        bool isAnimatorPlaying()
        {
            return playerAnimator.GetCurrentAnimatorStateInfo(0).length > playerAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime;
        }

    
        /// <summary>
        /// spawns a bomb
        /// </summary>
        void SpawnBomb() => playerController.SpawnBomb();

        void FixedUpdate()
        {
            playerRigidbody.velocity = new Vector2(horizontalSpeed, verticalSpeed) * moveSpeed;
        }

        /// <summary>
        /// damage to player
        /// </summary>
        public void Damage() => playerController.PlayerKilled();


        public void SetController(PlayerController playerController) =>
            this.playerController = playerController;

     
        /// <summary>
        /// checking collision with enemy only
        /// </summary>
        /// <param name="other"></param>
        void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.GetComponent<Enemy>() != null)
            {
                isDead = true;
                Damage();
            }
        }

    }
}