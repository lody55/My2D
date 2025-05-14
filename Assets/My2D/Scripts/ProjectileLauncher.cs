using UnityEngine;
namespace My2D
{
    //�߻�ü�� �߻��ϴ� Ŭ����

    public class ProjectileLauncher : MonoBehaviour
    {
        #region Variables
        //�߻�ü ������ 
        public GameObject projectilePrefab;

        //�߻� ��ġ
        public Transform firePoint;

        
        #endregion

        private void Awake()
        {
            
        }

        #region Custom Method
        //�߻�ü �߻�
        public void Fireprojectile()
        {
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, projectilePrefab.transform.rotation);

            Vector3 originScale = projectile.transform.localScale;

            projectile.transform.localScale = new Vector3(originScale.x * transform.localScale.x > 0 ? 1 : -1, originScale.y, originScale.z);

            Destroy(projectile, 3f);
        }
        #endregion
    }
}