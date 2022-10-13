using System;
using System.Collections.Generic;
using UnityEngine;

namespace DemoQuiz {
	public class PreviewOption : MonoBehaviour {
		
		private int index;
		public StringEvt OptionTextEvt;
		public BoolEvt AnswerEvt;
		private Action<int> answeraction;

		public void Init(int id, string text, Action<int> ansaction) {
			index = id;
			OptionTextEvt.Invoke(text);
			answeraction = ansaction;
		}
		public void OnSetAnswer(bool value) {
			if(!value || null == answeraction){ return; }
			answeraction.Invoke(index);
		}
		public void SetAsAnswer(bool value) {
			AnswerEvt.Invoke(value);			
		}
	}
}