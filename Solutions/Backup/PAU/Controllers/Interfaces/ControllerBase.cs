using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using DevComponents.WpfRibbon;
using System.Windows;
using System.ComponentModel;

namespace PAU.Controllers.Interfaces
{
    public class ControllerBase : IController, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = null;
        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private Dictionary<ICommand, CommandBinding> _bindings = new Dictionary<ICommand, CommandBinding>();

        public ControllerBase()
        {
        }

        public virtual void Connect(ICommand command, IButtonDropDownCommandExtender buttonDropDownCommandExtender)
        {
            RibbonCommandManager.Connect(command, buttonDropDownCommandExtender);
        }

        public UIElement View
        {
            get;
            set;
        }

        public virtual void UnBind(ICommand command)
        {
            if (View != null)
            {
                CommandBinding binding;
                if (_bindings.TryGetValue(command, out binding))
                {
                    View.CommandBindings.Remove(binding);

                    _bindings.Remove(command);
                }
            }
        }

        public virtual void Bind(ICommand command, ExecutedRoutedEventHandler executed)
        {
            if (View != null)
            {
                CommandBinding binding = new CommandBinding(command, executed);
                View.CommandBindings.Add(binding);

                // Unbind
                UnBind(command);

                if (!_bindings.ContainsKey(command))
                    _bindings.Add(command, binding);
            }
        }

        public virtual void Bind(ICommand command, ExecutedRoutedEventHandler executed, CanExecuteRoutedEventHandler canExecute)
        {
            if (View != null)
            {
                CommandBinding binding = new CommandBinding(command, executed, canExecute);
                View.CommandBindings.Add(binding);

                UnBind(command);

                if (!_bindings.ContainsKey(command))
                    _bindings.Add(command, binding);
            }
        }

        public virtual void Loaded(UIElement view)
        {
            View = view;
        }
    }
}
