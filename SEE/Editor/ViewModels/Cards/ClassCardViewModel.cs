using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Editor.Helpers;
using Editor.Models;
using Editor.UserControls;

namespace Editor.ViewModels
{
    class ClassCardViewModel : ScheduleListenerViewModel
    {

        #region Properties

        #region Class

        private Class _class;
        public Class Class
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

        #endregion

        #region Commands

        public ICommand CopyClassCommand { get { return new DelegateCommand(OnCopyClassCommand); } }
        public ICommand PasteClassCommand { get { return new DelegateCommand(OnPasteClassCommand); } }

        #endregion

        #region Command Handlers

        #region Copy

        private void OnCopyClassCommand()
        {
            var @class = new Class();
            Class.CopySLC(Class, @class);
            ClipboardService.SetData(@class);
        }

        #endregion

        #region Paste

        private void OnPasteClassCommand()
        {
            var cliped = ClipboardService.GetData<Class>();
            Class.CopySLC(cliped, Class);
        }


        #endregion

        #endregion
        
        #region Ctor

        public ClassCardViewModel(Class @class)
        {
            Class = @class;
            IsSelected = false;
        }

        public ClassCardViewModel()
        {
            IsSelected = false;
        }


        #endregion

    }
}
