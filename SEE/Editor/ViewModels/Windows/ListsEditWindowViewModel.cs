using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Editor.Helpers;
using ScheduleData;

namespace Editor.ViewModels
{
    enum ListsEditorTab
    {
        Groups,
        Lecturers,
        Rooms
    }

    class ListsEditWindowViewModel : BaseViewModel
    {

        #region Properties

        #region ActiveTab

        private ListsEditorTab _activeTab;
        public ListsEditorTab ActiveTab
        {
            get { return _activeTab; }
            set
            {
                if (_activeTab != value)
                {
                    _activeTab = value;
                    RaisePropertyChanged(() => ActiveTab);
                }
            }
        }

        #endregion

        #endregion

        #region Commands

        //public ICommand SetEditModeCommand { get { return new DelegateCommand(OnSetEditMode, CanExecuteSetEditMode); } }
        //public ICommand SetViewModeCommand { get { return new DelegateCommand(OnSetViewMode, CanExecuteSetViewMode); } }

        #endregion

        #region Ctor

        public ListsEditWindowViewModel()
        {
            ActiveTab = ListsEditorTab.Groups;
        }

        public ListsEditWindowViewModel(ListsEditorTab initTab)
        {
            ActiveTab = initTab;
        }

        #endregion

        #region Command Handlers

        

        #endregion

    }
}
