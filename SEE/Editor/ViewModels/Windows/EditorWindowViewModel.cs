using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Editor.Helpers;
using Editor.Views;

namespace Editor.ViewModels
{

    class EditorWindowViewModel : BaseViewModel
    {

        #region Ctor

        private Window _window;

        public EditorWindowViewModel(Window window)
        {
            _window = window;
        }

        #endregion

        #region Commands

        public ICommand OpenListsEditorCommand { get { return new DelegateCommand(OnOpenListsEditor); } }
        public ICommand OpenGroupsEditorCommand { get { return new DelegateCommand(OnOpenGroupsEditor); } }
        public ICommand OpenLecturersEditorCommand { get { return new DelegateCommand(OnOpenLecturersEditor); } }
        public ICommand OpenRoomsEditorCommand { get { return new DelegateCommand(OnOpenRoomsEditor); } }

        #endregion

        #region Command Handlers

        private void OnOpenGroupsEditor()
        {
            OpenListsEditorHelper(ListsEditorTab.Groups);
        }

        private void OnOpenLecturersEditor()
        {
            OpenListsEditorHelper(ListsEditorTab.Lecturers);
        }

        private void OnOpenRoomsEditor()
        {
            OpenListsEditorHelper(ListsEditorTab.Rooms);
        }

        private void OnOpenListsEditor()
        {
            OpenListsEditorHelper();
        }

        private void OpenListsEditorHelper(ListsEditorTab initTab = ListsEditorTab.Groups)
        {
            var vm = new ListsEditWindowViewModel(initTab);
            var window = new ListsEditWindow { DataContext = vm, Owner = _window};
            window.ShowDialog();
        }

        #endregion
    }
}
