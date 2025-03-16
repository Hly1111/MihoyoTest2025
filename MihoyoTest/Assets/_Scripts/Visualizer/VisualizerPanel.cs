using Core;
using UnityEngine;
using UnityEngine.UI;

public class VisualizerPanel : BasePanel
{
    private readonly int _isShrink = Animator.StringToHash("IsShrink");
    public Image OuterCircle => GetController<Image>("VisualCircleOut");
    public Image InnerCircle => GetController<Image>("VisualCircleIn");
    private Animator _animator;

    public override void OnShow()
    {
        base.OnShow();
        _animator = GetComponent<Animator>();
    }
    
    public void SetVisualizer(bool isShrink)
    {
        _animator.SetBool(_isShrink, isShrink);
    }
}
