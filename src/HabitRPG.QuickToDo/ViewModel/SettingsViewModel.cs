﻿using System;
using System.Diagnostics;
using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using HabitRPG.QuickToDo.Helpers;
using HabitRPG.QuickToDo.Model;
using HabitRPG.QuickToDo.Services;

namespace HabitRPG.QuickToDo.ViewModel
{
  public class SettingsViewModel : ViewModelBase
  {
    private readonly IDialogService _dialogService;
    private readonly ISettingsService _settingsService;
    private readonly IAnalyticsTracker _analyticsTracker;

    private Settings _settings;

    public RelayCommand<Window> CloseWindowCommand { get; private set; }
    public RelayCommand<Window> SaveSettingsCommand { get; private set; }

    public RelayCommand NavigateToHabitRpgComCommand { get; private set; }

    public SettingsViewModel(IDialogService dialogService, ISettingsService settingsService, IAnalyticsTracker analyticsTracker)
    {
      if (dialogService == null)
      {
        throw new ArgumentNullException("dialogService");
      }

      if (settingsService == null)
      {
        throw new ArgumentNullException("settingsService");
      }

      if (analyticsTracker == null)
      {
        throw new ArgumentNullException("analyticsTracker");
      }

      _dialogService = dialogService;
      _settingsService = settingsService;
      _analyticsTracker = analyticsTracker;

      _settings = _settingsService.GetSettings();

      CloseWindowCommand = new RelayCommand<Window>(CloseWindow);
      SaveSettingsCommand = new RelayCommand<Window>(SaveSettings);
      NavigateToHabitRpgComCommand = new RelayCommand(NavigateToHabitRpgCom);
    }

    public Settings Settings
    {
      get { return _settings; }
      set
      {
        _settings = value;
        RaisePropertyChanged("Settings");
      }
    }

    private void SaveSettings(Window window)
    {
      _settingsService.SetSettings(_settings);
      _dialogService.ShowInformationMessage("Please restart application to apply changes.");
      _analyticsTracker.TrackEventAsync(ViewTitle.Settings, "SaveSettings");

      if (window != null)
      {
        window.Close();
      }
    }

    private void NavigateToHabitRpgCom()
    {
      Process.Start(new ProcessStartInfo("https://habitrpg.com/#/options/settings/api"));
    }

    private void CloseWindow(Window window)
    {
      if (window != null)
      {
        window.Close();
      }
    }
  }
}