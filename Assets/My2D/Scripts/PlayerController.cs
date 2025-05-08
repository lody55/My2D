using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;
namespace My2D
{
    public class PlayerController : MonoBehaviour
    {
        #region Variables
        //��ü
        private Rigidbody2D rb2D;
        //�ִϸ�����
        public Animator animator;
        //�׶���, �� üũ
        private TouchingDirection touchingDirection;

        //�̵�
        //�̵� �Է� ��
        private Vector2 inputMove;

        private bool isMoving = false;
        private bool isRunning = false;
        

        //����
        //ĳ���� �̹����� �ٶ󺸴� ���� ���� : �������� �ٶ󺸸� true
        private bool isFacingRight = true;

        //����Ű�� ������ �� ���� �ö󰡴� �ӵ� ��
        [SerializeField]private float jumpForce = 5f;

        //�ȴ¼ӵ� - �¿�� �ȴ´�
        [SerializeField] private float walkSpeed = 4f;

        //�ٴ¼ӵ� - �¿�� �ڴ�
        [SerializeField] private float runSpeed = 7f;

        //������ �¿� �̵� �ӵ�
        [SerializeField] private float airSpeed = 3f; 
        #endregion

        #region Property
        //�̵� Ű �Է�


        public bool IsMoving
        {
            get { return isMoving; }
            set 
            { 
                isMoving = value;
                animator.SetBool(AnimationString.isMoving, value);
            }
        }

        public bool IsRunning
        {
            get { return isRunning; }
            set
            {
                isRunning = value;
                animator.SetBool(AnimationString.isRunning, value);
            }
        }    

        //���� �̵� �ӵ� - �б� ����
        public float CurrentSpeed
        {
            get
            {
                //��ǲ���� �������� and ���� �ε����� ������
                if (IsMoving && touchingDirection.IsWall == false)
                {
                    if (touchingDirection.IsGround)
                    {

                        if (IsRunning)
                        {
                            return runSpeed;
                        }
                        else
                        {
                            return walkSpeed;
                        }
                    }

                    else //���߿� ��������
                    {
                        return airSpeed;
                    }
                }
                else
                {
                    return 0f;
                }
                
            }
        }

        public bool IsFacingRight
        {
            get
            {
                return isFacingRight;
            }
            set
            {
                //��������
                if(isFacingRight != value)
                {
                    transform.localScale *= new Vector2(-1, 1);
                }
                isFacingRight = value;
            }
        }
        #endregion

        private void Awake()
        {
            //���� ��
            rb2D = GetComponent<Rigidbody2D>();
            touchingDirection = GetComponent<TouchingDirection>();
        }

        private void FixedUpdate()
        {
            //��ǲ ���� ���� �÷��̾� �¿� �̵�
            rb2D.linearVelocity = new Vector2(inputMove.x * CurrentSpeed, rb2D.linearVelocity.y);

            //�ִϸ����� �ӵ� �� ����
            animator.SetFloat(AnimationString.yVelocity, rb2D.linearVelocity.y);
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            inputMove = context.ReadValue<Vector2>();
            //��ǲ ���� ������ IsMoving �Ķ���� ����

            //�Է°��� ���� ����
            SetFactingDirection(inputMove);
            IsMoving = (inputMove != Vector2.zero);


        }

        public void OnRun(InputAction.CallbackContext context)
        {
            


            if (context.started)
            {
                
                IsRunning = true;
            }
            else if(context.canceled)
            {
                
                IsRunning = false;
            }
        }

        //����, �ٶ󺸴� ���� ��ȯ - �Է°��� ����
        void SetFactingDirection(Vector2 moveInput)
        {
            //�·� �̵�, ��� �̵�
            if (moveInput.x > 0f && IsFacingRight == false) //������ �ٶ󺸰� �ְ� ��� �̵� �Է�
            {
                IsFacingRight = true;
            }
            else if(moveInput.x <0f && IsFacingRight == true) //�������� �ٶ󺸰� �ִµ� �·� �̵�
            {
                IsFacingRight = false;
            }
        }
        public void OnJump(InputAction.CallbackContext context)
        {
            if (context.started && touchingDirection.IsGround)
            {
                //�ӵ� ���� - ���� �̵��ϴ� �ӵ� �� ����
                rb2D.linearVelocity = new Vector2(rb2D.linearVelocity.x,jumpForce);

                //�ִϸ��̼� 
                Debug.Log("������ �մϴ�");
                animator.SetTrigger(AnimationString.jumpTrigger);
            }
        }
        public void OnAttack(InputAction.CallbackContext context)
        {
            if(context.started)
            {
                animator.SetTrigger(AnimationString.isAttack);
            }
        }
    }
}