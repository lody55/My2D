using UnityEngine;
using UnityEngine.InputSystem;
namespace My2D
{
    public class PlayerController : MonoBehaviour
    {
        #region Variables
        //��ü
        private Rigidbody2D rb2D;

        //�̵�
        //�̵� �Է� ��
        private Vector2 inputMove;


        //�ȴ¼ӵ� - �¿�� �ȴ´�
        [SerializeField] private float walkSpeed = 4f;
        #endregion

        private void Awake()
        {
            //���� ��
            rb2D = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            //��ǲ ���� ���� �÷��̾� �¿� �̵�
            rb2D.linearVelocity = new Vector2(inputMove.x * walkSpeed, rb2D.linearVelocity.y);
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            inputMove = context.ReadValue<Vector2>();
        }
    }
}