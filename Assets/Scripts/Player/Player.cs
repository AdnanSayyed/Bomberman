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
        [SerializeField] private float moveSpeed;
        [SerializeField] private Rigidbody2D myBody;
        [SerializeField] private Transform playerMainTransform;
        [SerializeField] private Animator playerAnimator;

        private PlayerController playerController;
        private float horizontalSpeed, verticalSpeed;

        private bool dead;

        /// <summary>
        /// getting and setting player status value where necessary
        /// </summary>
        public bool PlayerDied
        {
            get
            {
                return dead;
            }
            set
            {
                dead = value;
            }
        }

        private void Update()
        {
            MoveDirection();
            if (Input.GetKeyDown(KeyCode.Space)) SpawnBomb();
        }

       
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
                        playerAnimator.Play("PlayerSideWalk_Right", -1, 0f);
                    }
                }
                else if (horizontalSpeed < 0) 
                {
                    playerAnimator.Play("PlayerSideWalk_Left");
                    if (!isAnimatorPlaying())
                    {
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
                        playerAnimator.Play("PlayerWalk_Back", -1, 0f);
                    }
                }
                else if (verticalSpeed < 0 )
                {
                    playerAnimator.Play("PlayerWalk_Front");
                    if (!isAnimatorPlaying())
                    {
                        playerAnimator.Play("PlayerWalk_Front", -1, 0f);
                    }
                }
                horizontalSpeed = 0;
            }
        }

        //check if animator is playing animation
        bool isAnimatorPlaying()
        {
            return playerAnimator.GetCurrentAnimatorStateInfo(0).length > playerAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime;
        }

    
        void SpawnBomb() => playerController.SpawnBomb();

        void FixedUpdate()
        {
            myBody.velocity = new Vector2(horizontalSpeed, verticalSpeed) * moveSpeed;
        }

        public void Damage() => playerController.PlayerKilled();


        public void SetController(PlayerController playerController) =>
            this.playerController = playerController;

        public void DoDamage()
        {
            playerController.PlayerKilled();
        }

        /// <summary>
        /// checking collision for enemy only
        /// </summary>
        /// <param name="other"></param>
        void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.GetComponent<Enemy>() != null)
            {
                dead = true;
                Damage();
            }
        }

    }
}