﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPFPractice.Model
{
    public class WindowCloser
    {
        public static bool GetEnableWindowClosing(DependencyObject obj)
        {
            return (bool)obj.GetValue(EnableWindowClosingProperty);
        }

        public static void SetEnableWindowClosing(DependencyObject obj, bool value)
        {
            obj.SetValue(EnableWindowClosingProperty, value);
        }

        // Using a DependencyProperty as the backing store for EnableWindowClosing.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EnableWindowClosingProperty =
            DependencyProperty.RegisterAttached("EnableWindowClosing", typeof(bool), typeof(WindowCloser), new PropertyMetadata(false, OnEnableWindowClosingChanged));

        private static void OnEnableWindowClosingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Window window)
            {
                window.Loaded += (s, ev) =>
                {
                    bool closeRequestFromVM = false;
                    if (window.DataContext is ICloseWindows vm)
                    {
                        vm.Closed += () =>
                        {
                            closeRequestFromVM = true;
                            window.Close();
                        };
                        window.Closing += (_s, _e) =>
                        {
                            _e.Cancel = !closeRequestFromVM;
                            window.Dispatcher.BeginInvoke((Action)(() =>
                            {
                                if (!closeRequestFromVM)
                                    vm.Close();
                            }));
                        };
                    }
                };
            }
        }
    }
}
