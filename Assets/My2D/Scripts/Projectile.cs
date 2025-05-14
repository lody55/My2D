using UnityEngine;
namespace My2D
{
    //발사체를 관리하는 클래스
    //기능 : 이동하기 (rb2D, LinearVelocity), 충돌하기, 데미지 입히기
    public class Projectile : MonoBehaviour
    {
        #region Variables
        //참조
        private Rigidbody2D rb2D;

        private float attackDamage = 20f;
        [SerializeField] private Vector2 knockback = Vector2.zero;
        //화살의 이동속도 - 좌 - 우로만 이동가능
        [SerializeField]private Vector2 moveVelocity;

        //데미지 효과 Sfx, Vfx
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
            //초기화
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