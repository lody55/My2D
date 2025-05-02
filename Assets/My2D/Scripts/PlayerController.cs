using UnityEngine;
using UnityEngine.InputSystem;
namespace My2D
{
    public class PlayerController : MonoBehaviour
    {
        #region Variables
        //강체
        private Rigidbody2D rb2D;

        //이동
        //이동 입력 값
        private Vector2 inputMove;


        //걷는속도 - 좌우로 걷는다
        [SerializeField] private float walkSpeed = 4f;
        #endregion

        private void Awake()
        {
            //참조 값
            rb2D = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            //인풋 값에 따라 플레이어 좌우 이동
            rb2D.linearVelocity = new Vector2(inputMove.x * walkSpeed, rb2D.linearVelocity.y);
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            inputMove = context.ReadValue<Vector2>();
        }
    }
}