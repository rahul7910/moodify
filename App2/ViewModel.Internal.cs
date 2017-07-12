using System;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.ComponentModel;
namespace App2
{
    partial class ViewModel: INotifyPropertyChanged
	{
		void OnNotifyPropertyChanged([CallerMemberName] string propertyName = "") =>

			 PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

	}
}

