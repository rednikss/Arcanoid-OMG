using App.Scripts.UI.AnimatedViews.Basic.Int;
using UnityEngine;

namespace App.Scripts.UI.AnimatedViews.Game.Score
{
    public class ComboLabelView : ScoreLabelView
    {
        [SerializeField] private AnimatedIntView[] additionalLabels;

        public override void Init()
        {
            base.Init();
            
            foreach (var additionalLabel in additionalLabels)
            {
                additionalLabel.Init();
            }
        }
        
        public new void SetValue(int value)
        {
            base.SetValue(value);

            foreach (var additionalLabel in additionalLabels)
            {
                additionalLabel.SetValue(value);
            }
        }
    }
}