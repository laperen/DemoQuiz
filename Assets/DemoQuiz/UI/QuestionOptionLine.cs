using System;
using UnityEngine;

namespace DemoQuiz {
	public class QuestionOptionLine : MonoBehaviour {
		public StringEvt OptionTextEvt;
		public BoolEvt AnswerEvt;

		private Action<string> optiontextchange;
		private Action<bool> setans;
		private Action dodelete;

		public void ChangeOptionText(string value) {
			if (null == optiontextchange) { return; }
			optiontextchange.Invoke(value);
		}
		public void SetAsAnswer(bool value) {
			if (null == setans) { return; }
			setans.Invoke(value);
		}
		public void DeleteOption() {
			if (null == dodelete) { return; }
			dodelete.Invoke();
		}
		public void RendederQuestionOption(int oindex, int qindex, Action deleteaction) {
			string optiontext = DataHolder.tempquiz.text.GetValueByKey(DataHolder.tempquiz.questions[qindex].optionkeys[oindex]);
			OptionTextEvt.Invoke(optiontext);
			optiontextchange = (string value) => {
				DataHolder.tempquiz.text.SetValue(DataHolder.tempquiz.questions[qindex].optionkeys[oindex], value);
			};
			bool hasans = DataHolder.tempquiz.questions[qindex].answers.Contains(oindex);
			AnswerEvt.Invoke(hasans);
			setans = (bool value) => {
				if (!value && hasans) {
					DataHolder.tempquiz.questions[qindex].answers.Remove(oindex);
				}
				if (value && !hasans) {
					DataHolder.tempquiz.questions[qindex].answers.Add(oindex);
				}
			};
			dodelete = deleteaction;
		}
	}
}