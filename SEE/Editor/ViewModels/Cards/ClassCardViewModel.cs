using System.Windows.Input;
using Editor.Helpers;
using Editor.Models;

namespace Editor.ViewModels.Cards
{
    class ClassCardViewModel : HasClassesScheduleProperty
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

        #endregion

        #region Commands

        public ICommand CopyClassCommand { get { return new DelegateCommand(OnCopyClassCommand); } }
        public ICommand PasteClassCommand { get { return new DelegateCommand(OnPasteClassCommand); } }

        #endregion

        #region Command Handlers

        #region Copy

        private void OnCopyClassCommand()
        {
            var @class = new ClassRecord();
            ClassRecord.Copy(Class, @class);
            ClipboardService.SetData(@class);
        }

        #endregion

        #region Paste

        private void OnPasteClassCommand()
        {
            var cliped = ClipboardService.GetData<ClassRecord>();
            ClassRecord.Copy(cliped, Class);
        }


        #endregion

        #endregion
        
        #region Ctor

        public ClassCardViewModel(ClassRecord @class)
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
