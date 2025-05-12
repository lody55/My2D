using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
namespace My2D
{
    // Health를 관리하는 클래스, takedamage, die 구현
    public class Damageable : MonoBehaviour
    {
        #region Variables
        //참조
        public Animator animator;
        //public PlayerController playerController;
        //public EnemyController enemyController;
        //체력
        private float health;
        //초기 체력(최대 체력
        [SerializeField] private float maxHealth = 100f;

        //죽음체크
        private bool isDeath = false;

        //무적 타이머
        private bool isInvincible = false;      //true이면 데미지를 입지 않는다
        [SerializeField] private float invincibleTime = 3f; //무적타임
        private float countdown = 0f;

        //델리게이트 이벤트 함수 - 매개변수로 float , vector2가 있는 함수 등록 가능
        public UnityAction<float, Vector2> hitAction;
        #endregion

        #region Property
        public float Health
        {
            get
            {
                return health;
            }
           private set
            {
                health = value;
                if (Health <= 0f && IsDeath == false)
                {
                    Die();
                }
            }
        }

        //최대 체력
        public float MaxHealth
        {
            get
            {
                return maxHealth;
            }
            private set
            {
                maxHealth = value;
            }
        }

        public bool IsDeath
        {
            get
            {
                return isDeath;
            }
            private set
            {
                isDeath = value;
            }
        }

        //이동속도 잠그기
        public bool LockVelocity
        {
            get { return animator.GetBool(AnimationString.lockVelocity); }
            set
            {
                animator.SetBool(AnimationString.lockVelocity, value);
            }
        }
        #endregion

        #region Unity Evenet Method
        private void Start()
        {
            //초기화
            Health = MaxHealth;
        }
        private void Update()
        {
            if(isInvincible)
            {
                if (countdown >= invincibleTime)
                {
                    //타이머 기능
                    isInvincible = false;

                    //타이머 초기화
                    countdown = 0;
                }
            }
            //무적 타이머
            countdown += Time.deltaTime;
            
        }
        #endregion

        #region Custum Mesthod

        public bool TakeDamage(float damage , Vector2 knockback)
        {

            if (IsDeath || isInvincible)
            {
                return false;
            }

            Health -= damage;
            Debug.Log($"Health : {Health}");

            //데미지 입으면
            isInvincible = true;
            countdown = 0;

            //애니메이션
            animator.SetTrigger(AnimationString.hitTrigger);
            LockVelocity = true;

            //델리게이트 함수에 등록된 함수 호출
            //if (hitAction != null)
            //{
            //    hitAction.Invoke(damage, knockback);
            //}
            hitAction?.Invoke(damage, knockback);
            
            return true;
        }

        void Die()
        {
            IsDeath = true;
            animator.SetBool(AnimationString.isDeath, true);
        }
        #endregion

    }
}