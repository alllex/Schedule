using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using Editor.Models;
using Editor.Repository;
using Editor.ViewModels;

namespace Editor.Views
{
    /// <summary>
    /// Interaction logic for EditorWindow.xaml
    /// </summary>
    public partial class EditorWindow : Window
    {
        public EditorWindow()
        {
            InitializeComponent();
            DataContextChanged += OnDataContextChanged;
        }

        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            Debug.WriteLine(GetType() + @": new DC = " + (DataContext == null ? "null" : DataContext.GetType().ToString()));
        }
    }
}
