using UnityEngine;
namespace My2D
{
    public class Attack : MonoBehaviour
    {
        //���ݽ� �浹ü���� �������� �ش�
        #region Variables
        //���ݷ�
        [SerializeField]private float attackDamage = 10f;

        //�˹� ȿ�� - �ڷ� �̵�
        [SerializeField] private Vector2 knockback = Vector2.zero;
        #endregion

        private void OnTriggerEnter2D(Collider2D collision)
        {
            //Debug.Log($"�÷��̾� ������ {attackDamage}�ش�");
            //collision���� Damageable ������Ʈ�� ã�Ƽ� TakeDamage�� �ּ���
            Damageable damageable = collision.GetComponent<Damageable>();

            if(damageable != null)
            {
                //�����ϴ� ĳ������ ���⿡ ���� �и��� ���⼳��
                Vector2 deliveredKnockback = this.transform.parent.parent.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);


                bool isHit = damageable.TakeDamage(attackDamage, deliveredKnockback);

                if(isHit)
                {
                    Debug.Log(collision.name + "hit for" + attackDamage);
                }
            }

        }
        
    }
}