using UnityEngine;
namespace My2D
{
    //떨어진 아이템을 픽업하면 아이템 효과를 나타낸다
    //Health 충전
    //필드에서 아이템이 회전한다
    public class PickupItem : MonoBehaviour
    {
        #region Variables

        
        //회전속도 - y축 기준으로 회전
        private Vector3 spinRotateSpeed = new Vector3(0f, 180f, 0f);

        //Health 충전
        private float restoreHealth = 30f;

        //사운드 효과
        private AudioSource pickupSource;
        #endregion

        #region Unity Event Method
        private void Awake()
        {
            //참조
            pickupSource = this.GetComponent<AudioSource>();
        }
        private void Update()
        {
            //회전
            transform.eulerAngles += spinRotateSpeed * Time.deltaTime;

        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            Damageable damageable = collision.GetComponent<Damageable>();
            Debug.Log("아이템 획득");
            if (damageable)
            {
                bool isHeal = damageable.Heal(restoreHealth);

                //아이템 제거
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