using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;
namespace My2D
{
    public class PlayerController : MonoBehaviour
    {
        #region Variables
        //강체
        private Rigidbody2D rb2D;
        //애니메이터
        public Animator animator;
        //그라운드, 벽 체크
        private TouchingDirection touchingDirection;

        //이동
        //이동 입력 값
        private Vector2 inputMove;

        private bool isMoving = false;
        private bool isRunning = false;
        

        //반전
        //캐릭터 이미지가 바라보는 방향 상태 : 오른쪽을 바라보면 true
        private bool isFacingRight = true;

        //점프키를 눌렀을 때 위로 올라가는 속도 값
        [SerializeField]private float jumpForce = 5f;

        //걷는속도 - 좌우로 걷는다
        [SerializeField] private float walkSpeed = 4f;

        //뛰는속도 - 좌우로 뛴다
        [SerializeField] private float runSpeed = 7f;

        //점프시 좌우 이동 속도
        [SerializeField] private float airSpeed = 3f; 
        #endregion

        #region Property
        //이동 키 입력


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

        //현재 이동 속도 - 읽기 전용
        public float CurrentSpeed
        {
            get
            {
                //인풋값이 들어왔을때 and 벽에 부딪히지 않을때
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

                    else //공중에 떠있을때
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
                //반전구현
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
            //참조 값
            rb2D = GetComponent<Rigidbody2D>();
            touchingDirection = GetComponent<TouchingDirection>();
        }

        private void FixedUpdate()
        {
            //인풋 값에 따라 플레이어 좌우 이동
            rb2D.linearVelocity = new Vector2(inputMove.x * CurrentSpeed, rb2D.linearVelocity.y);

            //애니메이터 속도 값 세팅
            animator.SetFloat(AnimationString.yVelocity, rb2D.linearVelocity.y);
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            inputMove = context.ReadValue<Vector2>();
            //인풋 값이 들어오면 IsMoving 파라미터 셋팅

            //입력값에 따른 반전
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

        //반전, 바라보는 방향 전환 - 입력값에 따라
        void SetFactingDirection(Vector2 moveInput)
        {
            //좌로 이동, 우로 이동
            if (moveInput.x > 0f && IsFacingRight == false) //왼쪽을 바라보고 있고 우로 이동 입력
            {
                IsFacingRight = true;
            }
            else if(moveInput.x <0f && IsFacingRight == true) //오른쪽을 바라보고 있는데 좌로 이동
            {
                IsFacingRight = false;
            }
        }
        public void OnJump(InputAction.CallbackContext context)
        {
            if (context.started && touchingDirection.IsGround)
            {
                //속도 연산 - 위로 이동하는 속도 값 세팅
                rb2D.linearVelocity = new Vector2(rb2D.linearVelocity.x,jumpForce);

                //애니메이션 
                Debug.Log("점프를 합니다");
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