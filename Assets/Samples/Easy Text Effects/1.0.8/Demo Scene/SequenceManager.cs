using System.Collections.Generic;
using UnityEngine;

namespace EasyTextEffects.Samples
{
    public class SequenceManager : MonoBehaviour
    {
        public List<GameObject> Slides;

        private int currentSlideIndex_ = -1;
        public List<float> SlideDelays;

        void Start()
        {
            for (int i = 0; i < Slides.Count; i++)
            {
                Slides[i].SetActive(false);
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                NextSlide();
            }
        }

        public void NextSlide()
        {
            if (currentSlideIndex_ > -1 && currentSlideIndex_ < Slides.Count)
            {
                StopEffect(currentSlideIndex_);
            }
            currentSlideIndex_++;
            if (currentSlideIndex_ > -1 && currentSlideIndex_ < Slides.Count)
            {
                Invoke(nameof(StartCurrentEffect), SlideDelays[currentSlideIndex_]);
            }
            else
            {
                currentSlideIndex_ = -1;
            }
        }

        private void StopEffect(int index)
        {
            GameObject currentSlide = Slides[index];
            var currentText = currentSlide.GetComponentInChildren<TextEffect>();
            if (currentText == null)
            {
                currentSlide.SetActive(false);
                return;
            }
            currentText.StopOnStartEffects();
            currentText.StartManualEffect("exit");
            var effect = currentText.FindManualEffect("exit");
            if (effect == null)
            {
                currentSlide.SetActive(false);
                return;
            }
            effect.onEffectCompleted.AddListener(() =>
            {
                currentSlide.SetActive(false);
                effect.onEffectCompleted.RemoveAllListeners();
            });
        }

        private void StartEffect(int index)
        {
            GameObject currentSlide = Slides[index];

            currentSlide.SetActive(true);
            var currentText = currentSlide.GetComponentInChildren<TextEffect>();
            if (currentText != null)
            {
                currentText.UpdateStyleInfos();
            }
        }

        private void StartCurrentEffect()
        {
            StartEffect(currentSlideIndex_);
        }
    }
}
