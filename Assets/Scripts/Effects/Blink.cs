using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Effects
{
    public class Blink
    {
        ///<param name = "material">Material must be transparent.</param>
        ///<param name = "blinkCount">If this is passed as -1, blinks infinite time.</param>
        public static void BlinkMaterial(Material material, float duration, int blinkCount, out Tween tween)
        {
            tween = material.DOFade(0f, duration).SetLoops(blinkCount ,LoopType.Yoyo);
        }

        ///<param name = "blinkCount">If this is passed as -1, blinks infinite time.</param>
        public static void BlinkText(Text text, float duration, int blinkCount, out Tween tween)
        {
            Color col = text.color;
            
            float alpha = text.color.a;

            tween = DOTween.To(() => alpha, x => alpha = x, 0f, duration).OnUpdate(() => 
            {
                col.a = alpha;
                text.color = col;
            })
            .SetLoops(blinkCount, LoopType.Yoyo)
            .OnComplete(() => 
            {
                alpha = 1f;
                col.a = alpha;
                text.color = col;
            })
            .OnKill(() => 
            {
                alpha = 1f;
                col.a = alpha;
                text.color = col;
            });
        }

        ///<param name = "blinkCount">If this is passed as -1, blinks infinite time.</param>
        public static void BlinkImage(Image image, float duration, int blinkCount, out Tween tween)
        {
            Color col = image.color;
            
            float alpha = image.color.a;

            tween = DOTween.To(() => alpha, x => alpha = x, 0f, duration).OnUpdate(() => 
            {
                col.a = alpha;
                image.color = col;
            })
            .SetLoops(blinkCount, LoopType.Yoyo)
            .OnComplete(() => 
            {
                alpha = 1f;
                col.a = alpha;
                image.color = col;
            })
            .OnKill(() => 
            {
                alpha = 1f;
                col.a = alpha;
                image.color = col;
            });
        }
    }
}
