using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Experimental.GlobalIllumination;
namespace My2D
{
    //캐릭터에서 사용하는 이벤트 함수 정의 클래스
    public class CharacterEvents
    {
        public static UnityAction<GameObject , float> characterDamage;
        public static UnityAction<GameObject, float> charactetHeal;
        
    }
}