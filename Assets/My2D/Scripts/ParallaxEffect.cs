using UnityEngine;
namespace My2D
{
    //������ ���� ����� �̵��ϴ� �Ÿ� ���ϱ�
    public class ParallaxEffect : MonoBehaviour
    {
        #region Variables
        //�÷��̾� ������Ʈ
        public Transform followTarget;
        //ī�޶� ������Ʈ
        public Camera cam;
        //���� ���� ����� ó�� ������ġ(�̵��ϱ� ��)
        private Vector2 startingPosition;
        //���� ���� ����� ó�� Z ��ġ��(�̵��ϱ� ��)
        private float startingZ;
        #endregion

        #region Property
        //ī�޶� ���� �������κ��� �̵� �Ÿ�
        Vector2 camMoveSinceStrart => startingPosition - (Vector2)cam.transform.position;

        //�÷��̾�� �������� �Ÿ�
        float zDistanceFromTarget => transform.position.z - followTarget.position.z;

        //�����ġ�� ���� �÷��̾���� �Ÿ�
        float clippingPlane => cam.transform.position.z + ((zDistanceFromTarget > 0) ? cam.farClipPlane :cam.nearClipPlane) ;

        //�÷��̾� �̵��� ���� ��� �̵� ����
        float parallaxFctor => Mathf.Abs (zDistanceFromTarget) / clippingPlane;

        #endregion

        #region Unity Event Method
        private void Start()
        {
            //�ʱ�ȭ (��� ���� ��ġ ����)
            startingPosition = this.transform.position;
            startingZ = this.transform.position.z;
        }
        private void Update()
        {
            //�÷��̾� �̵��� ���� ����� ���ο� ��ġ ���ϱ�
            Vector2 newPosition = startingPosition + camMoveSinceStrart * parallaxFctor;

            //����� ��ġ ����
            this.transform.position = new Vector3(newPosition.x, newPosition.y, startingZ);
        }
        #endregion
    }
}