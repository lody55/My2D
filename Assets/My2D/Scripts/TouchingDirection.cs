using UnityEngine;
namespace My2D
{
    //Collider Cast�� �̿��Ͽ� �ٴ�, õ��, ��üũ
    public class TouchingDirection : MonoBehaviour
    {
        #region Variables
        private CapsuleCollider2D touchingCollider;
        public Animator animator;
        //ĳ���� ����
        [SerializeField]private float groundDistance = 0.05f;   // �׶��� üũ ����
        [SerializeField] private float cellingDistance = 0.05f; // õ�� üũ ����
        [SerializeField] private float wallDistance = 0.2f;    // �� üũ ����

        //ĳ���� ���� ����
        [SerializeField]private ContactFilter2D contactFilter;

        //ĳ���õ� RaycastHit2D ����Ʈ(�迭)
        private RaycastHit2D[] groundHits = new RaycastHit2D[5];
        private RaycastHit2D[] cellingHits = new RaycastHit2D[5];
        private RaycastHit2D[] wallHits = new RaycastHit2D[5];

        //�׶��� üũ
        [SerializeField] private bool isGround = false; //�׶���
        [SerializeField] private bool isCelling = false;    // õ��
        [SerializeField] private bool isWall = false;   //��

        #endregion

        #region Property
        public bool IsGround
        {
            get { return isGround; }
            private set
            {
                isGround = value;
                animator.SetBool(AnimationString.isGround, value);
            }
        }

        public bool IsCelling
        {
            get { return isCelling; }
            private set
            {
                isCelling = value;
                animator.SetBool(AnimationString.isCelling, value);
            }
        }
        public bool IsWall
        {
            get { return isWall; }
            set
            {
                isWall = value;
                animator.SetBool(AnimationString.isWall, value);
            }
        }
        private Vector2 WallCheckDirection => (this.transform.localScale.x > 0) ? Vector2.right : Vector2.left;



        #endregion

        #region Unity Event Method
        private void Awake()
        {
            //����
            touchingCollider = this.GetComponent<CapsuleCollider2D>();
        }
        private void FixedUpdate()
        {
            //�ٴ� üũ
            IsGround = (touchingCollider.Cast(Vector2.down, contactFilter, groundHits, groundDistance) > 0);

            //õ�� üũ
            IsCelling = (touchingCollider.Cast(Vector2.up, contactFilter, cellingHits, cellingDistance) > 0);

            //�� üũ
            IsWall = (touchingCollider.Cast( WallCheckDirection , contactFilter, wallHits, wallDistance) > 0);
        }

        #endregion
    }


}
