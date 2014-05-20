﻿using System;
using System.ComponentModel;
using System.Windows.Input;
using Editor.Helpers;
using Editor.Models;

namespace Editor.ViewModels.Cards
{
    public class ClassCardViewModel : HasProjectProperty
    {

        #region Properties

        #region Class

        private ClassRecord _class;
        public ClassRecord Class
        {
            get { return _class; }
            set
            {
                if (_class != value)
                {
                    _class = value;
                    RaisePropertyChanged(() => Class);
                }
            }
        }

        #endregion
        
        #region IsSelected

        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (_isSelected != value)
                {
                    _isSelected = value;
                    RaisePropertyChanged(() => IsSelected);
                }
            }
        }

        #endregion

        #region HasWarning

        private bool _hasWarning;

        public bool HasWarning
        {
            get { return _hasWarning; }
            set
            {
                if (_hasWarning != value)
                {
                    _hasWarning = value;
                    RaisePropertyChanged(() => HasWarning);
                }
            }
        }

        #endregion

        #region HasConflict

        private bool _hasConflict;

        public bool HasConflict
        {
            get { return _hasConflict; }
            set
            {
                if (_hasConflict != value)
                {
                    _hasConflict = value;
                    RaisePropertyChanged(() => HasConflict);
                }
            }
        }

        #endregion
        
        #endregion

        #region Commands

        #endregion

        #region Command Handlers

        

        #endregion
        
        #region Ctor

        public ClassCardViewModel(ClassRecord @class)
        {
            Class = @class;
            PropertyChanged += OnPropertyChanged;
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var s = sender as ClassCardViewModel;
            if (s == null) return;
            if (e.PropertyName == "Class")
            {
                if (Class == null)
                {
                    HasWarning = false;
                    HasConflict = false;
                }
            }
        }

        #endregion

    }
}
