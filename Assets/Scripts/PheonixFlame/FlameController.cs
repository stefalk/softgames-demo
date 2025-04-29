using UnityEngine;

public class FlameController : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;

    public void TriggerNextColor()
    {
        _animator.SetTrigger("NextTrigger");
    }
}
