using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
namespace My2D
{
    // Health�� �����ϴ� Ŭ����, takedamage, die ����
    public class Damageable : MonoBehaviour
    {
        #region Variables
        //����
        public Animator animator;
        //public PlayerController playerController;
        //public EnemyController enemyController;
        //ü��
        private float health;
        //�ʱ� ü��(�ִ� ü��
        [SerializeField] private float maxHealth = 100f;

        //����üũ
        private bool isDeath = false;

        //���� Ÿ�̸�
        private bool isInvincible = false;      //true�̸� �������� ���� �ʴ´�
        [SerializeField] private float invincibleTime = 3f; //����Ÿ��
        private float countdown = 0f;

        //��������Ʈ �̺�Ʈ �Լ� - �Ű������� float , vector2�� �ִ� �Լ� ��� ����
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
        
        //�ִ� ü��
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

        //�̵��ӵ� ��ױ�
        public bool LockVelocity
        {
            get { return animator.GetBool(AnimationString.lockVelocity); }
            set
            {
                animator.SetBool(AnimationString.lockVelocity, value);
            }
        }

        //hp Ǯ üũ
        public bool IsHealthFull => Health >= maxHealth;
        
        #endregion

        #region Unity Evenet Method
        private void Start()
        {
            //�ʱ�ȭ
            Health = MaxHealth;
            
        }
        private void Update()
        {
            if(isInvincible)
            {
                if (countdown >= invincibleTime)
                {
                    //Ÿ�̸� ���
                    isInvincible = false;

                    //Ÿ�̸� �ʱ�ȭ
                    countdown = 0;
                }
            }
            //���� Ÿ�̸�
            countdown += Time.deltaTime;
            
        }
        #endregion

        #region Custum Mesthod
        //Health ����
        public bool TakeDamage(float damage , Vector2 knockback)
        {

            if (IsDeath || isInvincible)
            {
                return false;
            }

            Health -= damage;
            Debug.Log($"Health : {Health}");

            //������ ������
            isInvincible = true;
            countdown = 0;

            //�ִϸ��̼�
            animator.SetTrigger(AnimationString.hitTrigger);
            LockVelocity = true;

            //ȿ�� : SFX, VFX , �˹�ȿ��, UIȿ��
            //��������Ʈ �Լ��� ��ϵ� �Լ� ȣ�� : ȿ�� ���⿡ �ʿ��� �Լ� ���
            //if (hitAction != null)
            //{
            //    hitAction.Invoke(damage, knockback);
            //}
            hitAction?.Invoke(damage, knockback);

            //UI ȿ�� - ������text ������ �����ϴ� �̺�Ʈ �Լ� ȣ��
            CharacterEvents.characterDamage?.Invoke(gameObject, damage);
            return true;
        }

        void Die()
        {
            IsDeath = true;
            animator.SetBool(AnimationString.isDeath, true);
        }

        //Health ���� - �Ű� ������ŭ Health ����
        //���� ��ȯ�ϸ� health�� ���������� ����, ���� ��ȯ�ϸ� �������� �ʾҴ�
        public bool Heal(float healAmount)
        {
            if (IsDeath || IsHealthFull) return false;

            float beforeHealth = Health;

            

            Health += healAmount;
            if (Health > maxHealth)
            {
                Health = maxHealth;
            }
            float actualHealth = Health - beforeHealth;
            Debug.Log($"Health : {Health}");

            //UIȿ�� - �� Text ������ �����ϴ� �Լ��� ��ϵ� �̺�Ʈ �Լ� ȣ��
            //Health Text�� Health Bar ���� �Լ� ȣ��
            CharacterEvents.characterHeal?.Invoke(gameObject, actualHealth);

            
            return true;
        }
        #endregion

    }
}