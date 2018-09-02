using DG.Tweening;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace jam_jam
{
    public class MainMenuController : MonoBehaviour
    {

        public RectTransform movablePoint;

        public void GoToFirstLevel()
        {
            LevelsManager.Instance.SetCurrenLevel(1);
            SceneManager.LoadScene(1);
        }

        public void MoveToLevels()
        {
            DOTween.To(() => movablePoint.anchoredPosition, pos => movablePoint.anchoredPosition = pos, new Vector2(1920, 0), 1);
        }

        public void MoveToMain()
        {
            DOTween.To(() => movablePoint.anchoredPosition, pos => movablePoint.anchoredPosition = pos, Vector2.zero, 1);
        }

        public void MoveToOptions()
        {
            DOTween.To(() => movablePoint.anchoredPosition, pos => movablePoint.anchoredPosition = pos, new Vector2(-1920, 0), 1);
        }

        public void OpenURL(string url)
        {
            Application.OpenURL(url);
        }
    }
}