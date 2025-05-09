using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace My2D
{
    //Detection Zone�� ������ �ݶ��̴� �����ϴ� Ŭ����
    public class DetectionZone : MonoBehaviour
    {
        #region Variables
        //Zone�� ���� �ݶ��̴����� �����ϴ� ����Ʈ - ���� Zone �ȿ� �ִ� �ݶ��̴� ��� 
        public List<Collider2D> detectedColliders = new List<Collider2D>();
        #endregion

        private void OnTriggerEnter2D(Collider2D collision)
        {
            //�浹ü�� ���� ������ ����Ʈ�� �߰�
            //Debug.Log($"{collision.name}�浹ü�� ���� ��� �Դ�");
            detectedColliders.Add(collision);
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            //�浹ü�� ������ ������ ����Ʈ���� ����
            //Debug.Log($"{collision.name}�浹ü�� ������ ������");
            detectedColliders.Remove(collision);
        }

    }
}