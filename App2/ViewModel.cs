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
            var url = "http://images.boomsbeat.com/data/images/full/595/bill-gates-jpg.jpg";
            People.Add(new Person("Bill", url));

        }

    }
}
