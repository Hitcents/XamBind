using System;

namespace XamBind.iOS.Tests
{
	public class TestViewModel : PropertyChangedBase
    {
		private string _text;

		public string Text
		{
			get { return _text; }
			set { _text = value; OnPropertyChanged("Text"); }
		}

		private string _buttonTitle;

		public string ButtonTitle
		{
			get { return _buttonTitle; }
			set { _buttonTitle = value; OnPropertyChanged("ButtonTitle"); }
		}
    }
}

