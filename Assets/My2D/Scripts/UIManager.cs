using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;
namespace My2D
{
    //UI�� �����ϴ� Ŭ����
    public class UIManager : MonoBehaviour
    {
        #region Variables
        //������ �ؽ�Ʈ ������
        public GameObject damagerTextPrefab;
        public GameObject healTextPrefab;
        //ĵ�ٽ�
        public Canvas gameCanvas;

        //ĵ�ٽ����� ���� ��ġ ��������
        private Camera camera;

        [SerializeField] private Vector3 offset;    //ĳ���� �Ӹ����� ��ġ ������
        #endregion

        #region Unity Event Method
        private void Awake()
        {
            //����
            camera = Camera.main;
        }
        private void OnEnable()
        {
            //�̺�Ʈ �Լ��� �Լ� ���
            CharacterEvents.characterDamage += CharacterTakeDamage;
            CharacterEvents.characterHeal += CharacterHeal;
        }
        private void OnDisable()
        {
            //�̺�Ʈ �Լ��� ��ϵ� �Լ� ����
            CharacterEvents.characterDamage -= CharacterTakeDamage;
            CharacterEvents.characterHeal -= CharacterHeal;
        }
        #endregion
        //������ �ؽ�Ʈ ������ ����
        #region Custom Method
        public void CharacterTakeDamage(GameObject character , float damage)
        {
            //������ ���� - ������ �������� �θ� Canvas�� ����
            //�ؽ�Ʈ�� �Ű������� ���� �������� ����
            //Debug.Log($"������ ������ ���� : {damage}");
            Vector3 spawnPosition = camera.WorldToScreenPoint(character.transform.position);
            GameObject textGo = Instantiate(damagerTextPrefab, spawnPosition + offset, Quaternion.identity, gameCanvas.transform);

            //�ؽ�Ʈ ��ü
            TextMeshProUGUI damageText = textGo.GetComponent<TextMeshProUGUI>();
            if(damageText)
            {
                damageText.text = damage.ToString();
            }
             
        }

        //�� �ؽ�Ʈ ������ ����, Character : �� �� ĳ����
        public void CharacterHeal(GameObject character, float healAmount)
        {
            //������ ���� - ������ �������� �θ� Canvas�� ����
            //�ؽ�Ʈ�� �Ű������� ���� ���� ����.
            Vector3 spawnPosition = camera.WorldToScreenPoint(character.transform.position);
            GameObject textGo = Instantiate(healTextPrefab, spawnPosition + offset, Quaternion.identity, gameCanvas.transform);

            TextMeshProUGUI healText = textGo.GetComponent<TextMeshProUGUI>();
            if(healText)
            {
                healText.text = healAmount.ToString();
            }
        }
        #endregion
    }
}