using UnityEngine;
namespace My2D
{
    //�߻�ü�� �����ϴ� Ŭ����
    //��� : �̵��ϱ� (rb2D, LinearVelocity), �浹�ϱ�, ������ ������
    public class Projectile : MonoBehaviour
    {
        #region Variables
        //����
        private Rigidbody2D rb2D;

        private float attackDamage = 20f;
        [SerializeField] private Vector2 knockback = Vector2.zero;
        //ȭ���� �̵��ӵ� - �� - ��θ� �̵�����
        [SerializeField]private Vector2 moveVelocity;

        //������ ȿ�� Sfx, Vfx
        public GameObject projectilEffectPrefab;
        public Transform effectPos;

        private TouchingDirection touchingDirection;
        #endregion

        private void Awake()
        {
            rb2D = this.GetComponent<Rigidbody2D>();
        }
        private void Start()
        {
            //�ʱ�ȭ
            rb2D.linearVelocity = new Vector2(moveVelocity.x * this.transform.localScale.x, moveVelocity.y);

            touchingDirection = GetComponent<TouchingDirection>();
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            Damageable damageable = collision.GetComponent<Damageable>();
            if (damageable != null)
            {

                Vector2 deliveredKnockback = this.transform.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);

                bool isHit = damageable.TakeDamage(attackDamage, deliveredKnockback);
                if (isHit)
                {
                    GameObject effectGo = Instantiate(projectilEffectPrefab, this.effectPos.position, Quaternion.identity);
                    Destroy(effectGo, 0.4f);
                }
                else if(touchingDirection.IsWall)
                {
                    Destroy(gameObject);
                }

            }


            Destroy(gameObject);
        }
        
    }
}