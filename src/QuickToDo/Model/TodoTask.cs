﻿using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace QuickToDo.Model
{
  public class TodoTask : INotifyPropertyChanged
  {
    private string _text;

    public string Text
    {
      get => _text;
      set
      {
        _text = value;
        OnPropertyChanged();
      }
    }

    public event PropertyChangedEventHandler PropertyChanged;
    
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
  }
}