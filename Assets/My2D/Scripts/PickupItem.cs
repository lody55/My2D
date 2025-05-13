using UnityEngine;
namespace My2D
{
    //������ �������� �Ⱦ��ϸ� ������ ȿ���� ��Ÿ����
    //Health ����
    //�ʵ忡�� �������� ȸ���Ѵ�
    public class PickupItem : MonoBehaviour
    {
        #region Variables

        
        //ȸ���ӵ� - y�� �������� ȸ��
        private Vector3 spinRotateSpeed = new Vector3(0f, 180f, 0f);

        //Health ����
        private float restoreHealth = 30f;

        //���� ȿ��
        private AudioSource pickupSource;
        #endregion

        #region Unity Event Method
        private void Awake()
        {
            //����
            pickupSource = this.GetComponent<AudioSource>();
        }
        private void Update()
        {
            //ȸ��
            transform.eulerAngles += spinRotateSpeed * Time.deltaTime;

        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            Damageable damageable = collision.GetComponent<Damageable>();
            Debug.Log("������ ȹ��");
            if (damageable)
            {
                bool isHeal = damageable.Heal(restoreHealth);

                //������ ����
                if (isHeal)
                {
                    if (pickupSource)
                    {
                        pickupSource.PlayOneShot(pickupSource.clip);
                    }
                    Destroy(gameObject);
                }
            }
        }
        #endregion
    }
}