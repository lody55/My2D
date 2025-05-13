using UnityEditor.Tilemaps;
using UnityEngine;
namespace My2D
{
    //�̵� ����
    public enum WalkableDirection
    {
        Left,
        Right
    }

    //�� ĳ���͸� �����ϴ� Ŭ����
    [RequireComponent(typeof(Rigidbody2D),typeof(TouchingDirection))]
    public class EnemyController : MonoBehaviour
    {
        
        #region Variables
        //����
        private Rigidbody2D rb2D;
        private TouchingDirection touchingDirection;
        //�ִϸ�����
        public Animator animator;
        public DetectionZone detectionZone; //�÷��̾� ����
        private Damageable damageable;
        public DetectionZone cliffeDetection;


        //�̵� ���ǵ�
        [SerializeField] float moveSpeed = 4f;


        //�̵�
        //�̵� �Է� ��
        private Vector2 directionVector = Vector2.right;

        //���� �̵� ������ ����
        private WalkableDirection walkDirection = WalkableDirection.Right;

        //���� ���ǵ�
        private float stopRate = 0.2f;

        //�� ����
        private bool hasTarget = false;

        #endregion
        #region Property
        public float MoveSpeed
        {
            get
            {
                return moveSpeed;
            }
            
        }
        public bool CannotMove
        {
            get
            {
                return animator.GetBool(AnimationString.cannotMove);
            }
        }
        public WalkableDirection WalkDirection
        {
            get
            {
                return walkDirection;
            }
            set
            {
                //�ݴ������� ��ġ �̵��϶�, �ݴ������ �ٶ󺸵��� �Ѵ�
                //������ȯ�� �Ͼ�� ����
                if (walkDirection != value)
                {
                    //�ݴ������ �ٶ󺸵��� �Ѵ� - �̹��� �ø�
                    this.transform.localScale *= new Vector2(-1,1);

                    //�ݴ������� ��ġ �̵��϶�
                    if(value == WalkableDirection.Left)
                    {
                        directionVector = Vector2.left;
                    }
                    else if(value == WalkableDirection.Right)
                    {
                        directionVector = Vector2.right;
                    }

                }
                walkDirection = value;
            }

        }
        public bool HasTarget
        {
            get { return hasTarget; }
            set
            {
                hasTarget = value;
                animator.SetBool(AnimationString.hasTarget, value);
            }
        }

        //���� ��Ÿ�� : �о�鿩�� 0���� ũ�� 3�� Ÿ�̸Ӹ� ���� 0���� �ٽ� �Ķ���� ���� ����
        public float CooldownTime
        {
            get
            {
                return animator.GetFloat(AnimationString.cooldownTime);
            }
            set
            {
                animator.SetFloat(AnimationString.cooldownTime, value);
            }
        }
        #endregion


        #region Unity Event Method
        private void Awake()
        {
            //���� ��
            rb2D = GetComponent<Rigidbody2D>();
            touchingDirection = GetComponent<TouchingDirection>();
            damageable = this.GetComponent<Damageable>();
            damageable.hitAction += OnHit;

            //cliffeDetection �̺�Ʈ �Լ� ���
            cliffeDetection.noColliderRamain += Flip;
        }
        private void Update()
        {
            //������
            HasTarget = detectionZone.detectedColliders.Count > 0;

            //CooldownTimer
            if (CooldownTime > 0)
            {
                CooldownTime -= Time.deltaTime;
            }
        }
        private void FixedUpdate()
        {
            //���� �������� ������ȯ�Ͽ� �̵��Ѵ�
            if(touchingDirection.IsWall && touchingDirection.IsGround)
            {
                Flip();
            }
            if (damageable.LockVelocity == false)
            {
                if (CannotMove)
                {
                    rb2D.linearVelocity = new Vector2(Mathf.Lerp(rb2D.linearVelocityX, 0f, stopRate) * 0f, rb2D.linearVelocityY);
                }
                else
                {
                    //�¿� �̵�
                    rb2D.linearVelocity = new Vector2(directionVector.x * moveSpeed, rb2D.linearVelocityY);
                }
            }

        }

        #endregion

        #region Custom Method

        //���� ��ȯ
        private void Flip()
        {
            //Debug.Log("������ȯ");
            if (WalkDirection == WalkableDirection.Left)
            {
                WalkDirection = WalkableDirection.Right;
            }
            else if (WalkDirection == WalkableDirection.Right)
            {
                WalkDirection = WalkableDirection.Left;
            }
            else
            {
                Debug.Log("������ȯ ����");
            }
        }
        public void OnHit(float damage, Vector2 knockback)
        {
            rb2D.linearVelocity = new Vector2(knockback.x, rb2D.linearVelocity.y + knockback.y);
            
        }
        #endregion
    }
}