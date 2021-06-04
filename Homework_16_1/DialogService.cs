using System;
using System.Collections.Generic;
using System.Windows;

namespace Homework_16_1
{
    class DialogService : IDialogService
    {
        Window owner;
        public Dictionary<Type, Type> ViewModelMap { get; }

        public DialogService(Window owner)
        {
            ViewModelMap = new Dictionary<Type, Type>();
            this.owner = owner;
        }

        public void Register<TDialogViewModel, TDialogView>()
            where TDialogViewModel : ICloseRequest
            where TDialogView : IDialog
        {
            if (ViewModelMap.ContainsKey(typeof(TDialogViewModel)))
            {
                throw new ArgumentException("Viewmodel already mapped");
            }
            ViewModelMap.Add(typeof(TDialogViewModel), typeof(TDialogView));
        }

        public bool? Show<TDialogViewModel>(TDialogViewModel dialogViewModel) where TDialogViewModel : ICloseRequest
        {
            var view = ViewModelMap[typeof(TDialogViewModel)];
            IDialog dialog = (IDialog)Activator.CreateInstance(view);
            EventHandler<CloseRequestArgs> eventHandler = null;
            eventHandler = (sender, args) =>
            {
                dialogViewModel.CloseRequest -= eventHandler;
                if (args.DialogResult.HasValue)
                {
                    dialog.DialogResult = args.DialogResult;
                }
                else
                {
                    dialog.Close();
                }
            };
            dialogViewModel.CloseRequest += eventHandler;

            dialog.DataContext = dialogViewModel;
            dialog.Owner = owner;
            return dialog.ShowDialog();
        }
    }
}
