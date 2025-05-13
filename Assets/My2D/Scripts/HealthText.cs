using TMPro;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.UI;
namespace My2D
{
    //Damage가 위쪽으로 이동한다
    //Damage 페이드 아웃 효과, 페이드 아웃 효과 후 킬 - text의 컬러 값으로 페이드 효과
    public class HealthText : MonoBehaviour
    {
        #region Variables
        //이동 
        [SerializeField]private float moveSpeed = 10f;
        private RectTransform textTransform;

        //페이드 효과
        private Color startColor;
        private TextMeshProUGUI healthText;
        [SerializeField]private float fadeTime = 1f;
        private float fadeCountdown = 0f;

        
        #endregion

        private void Awake()
        {
            //참조
            textTransform = this.GetComponent<RectTransform>();
            healthText = this.GetComponent<TextMeshProUGUI>();

            //초기화
            startColor = healthText.color;
            
        }
        private void Update()
        {
            //이동
            textTransform.position += Vector3.up * Time.deltaTime * moveSpeed;

            //fadeout 타이머 
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