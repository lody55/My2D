using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Experimental.GlobalIllumination;
namespace My2D
{
    //ĳ���Ϳ��� ����ϴ� �̺�Ʈ �Լ� ���� Ŭ����
    public class CharacterEvents
    {
        public static UnityAction<GameObject , float> characterDamage;
        public static UnityAction<GameObject, float> charactetHeal;
        
    }
}