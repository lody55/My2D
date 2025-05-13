using TMPro;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.UI;
namespace My2D
{
    //Damage�� �������� �̵��Ѵ�
    //Damage ���̵� �ƿ� ȿ��, ���̵� �ƿ� ȿ�� �� ų - text�� �÷� ������ ���̵� ȿ��
    public class HealthText : MonoBehaviour
    {
        #region Variables
        //�̵� 
        [SerializeField]private float moveSpeed = 10f;
        private RectTransform textTransform;

        //���̵� ȿ��
        private Color startColor;
        private TextMeshProUGUI healthText;
        [SerializeField]private float fadeTime = 1f;
        private float fadeCountdown = 0f;

        
        #endregion

        private void Awake()
        {
            //����
            textTransform = this.GetComponent<RectTransform>();
            healthText = this.GetComponent<TextMeshProUGUI>();

            //�ʱ�ȭ
            startColor = healthText.color;
            
        }
        private void Update()
        {
            //�̵�
            textTransform.position += Vector3.up * Time.deltaTime * moveSpeed;

            //fadeout Ÿ�̸� 
            fadeCountdown += Time.deltaTime;    // 0 -> 1 : (1 -fadeCountdown)
            //startColor.a => 1-> 0
            float newAlpha = startColor.a * (1 - fadeCountdown / fadeTime);
            healthText.color = new Color(startColor.r, startColor.g, startColor.b, newAlpha);
            if (fadeCountdown >= fadeTime)
            {
                Destroy(this.gameObject);
            }
        }
    }
}