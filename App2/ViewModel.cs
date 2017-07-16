using System.Collections.ObjectModel;
using System.ComponentModel;


namespace App2
{
    public partial class ViewModel
    {

        public event PropertyChangedEventHandler PropertyChanged;

        ObservableCollection<Person> _people = new ObservableCollection<Person>();

        public ObservableCollection<Person> People
        {
            get { return _people; }
            set
            {
                if (value != _people)
                {
                    _people = value;
                    OnNotifyPropertyChanged();
                }
            }
        }

        public void Load()
        {
            //Add more employees later
            var url = "https://pbs.twimg.com/profile_images/558109954561679360/j1f9DiJi.jpeg";
            var url2 = "https://4fede15c79aa965487f6-cd80b954b0e5e10e7798c3bef2da0fe9.ssl.cf2.rackcdn.com/6b9eb872-9f44-4aa0-af38-79ebbe19f3e0Steve%20Wozniak_Hi-res_v2.png";
            var url3 = "http://68.media.tumblr.com/98fe4616b3e92afd449f0aa4943f3ce8/tumblr_inline_n35o09pDQY1r3g1up.jpg"; 
            People.Add(new Person("Bill Gates", url));
            People.Add(new Person("Steve Wozniak", url2));
            People.Add(new Person("Steve Jobs", url3));



        }

    }
}
