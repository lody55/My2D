using UnityEngine;
namespace My2D
{
    //발사체를 발사하는 클래스

    public class ProjectileLauncher : MonoBehaviour
    {
        #region Variables
        //발사체 프리펩 
        public GameObject projectilePrefab;

        //발사 위치
        public Transform firePoint;

        
        #endregion

        private void Awake()
        {
            
        }

        #region Custom Method
        //발사체 발사
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