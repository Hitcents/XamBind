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

		private string _searchTitle;

		public string SearchTitle
		{
			get { return _searchTitle; }
			set { _searchTitle = value; OnPropertyChanged("SearchTitle"); }
		}

		private bool _canSearch = true;

		public bool CanSearch
		{
			get { return _canSearch; }
			set { _canSearch = value; OnPropertyChanged("CanSearch"); }
		}

		public void Search()
		{
			Searched = true;
		}

		public bool Searched;
    }
}

