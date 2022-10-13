using UnityEngine;
using UnityEngine.SceneManagement;

namespace DemoQuiz {
    public class QuizCreateHandler : MonoBehaviour {
        public RectTransform questionscontent;
        public GameObject questionlineprefab;
        public StringEvt QuizName;

        public static QuizCreateHandler instance;

        public QuizCreateHandler() {
            instance = this;
        }
		public void Init() {
            QuizName.Invoke(DataHolder.tempquiz.text.GetValueByKey(DataHolder.tempquiz.quiznamekey));
            RenderQuestions();
		}
        public void ChangeQuizName(string value) {
            DataHolder.tempquiz.text.SetValue(DataHolder.tempquiz.quiznamekey, value);
        }
        public void SettleLayout() {
            RectUtil.SettleLayout(questionscontent);
        }
        public void CreateQuestion() {
            DataHolder.tempquiz.NewQuestion("question text");
            RenderQuestions();
        }
        public void RenderQuestions() {
            for (int i = 0, max = DataHolder.tempquiz.questions.Count; i < max; i++) {
                Question q = DataHolder.tempquiz.questions[i];
                Transform child = null;
                if (questionscontent.childCount > i) {
                    child = questionscontent.GetChild(i);
                    child.gameObject.SetActive(true);
                }
                if (!child) {
                    child = Instantiate(questionlineprefab, questionscontent).transform;
                }
                child.GetComponent<QuestionLine>().RenderQuestion(i);
            }
            if (questionscontent.childCount > DataHolder.tempquiz.questions.Count) {
                for (int i = DataHolder.tempquiz.questions.Count, max = questionscontent.childCount; i < max; i++) {
                    questionscontent.GetChild(i).gameObject.SetActive(false);
                }
            }
            SettleLayout();
        }
        public void SaveQuiz() {
            DataHolder.SaveQuizData();    
            //SceneManager.LoadScene(1);
        }
    }
}