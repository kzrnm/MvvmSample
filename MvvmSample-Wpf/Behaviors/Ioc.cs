using System;
using System.ComponentModel;
using System.Windows;

namespace Kzrnm.MvvmSample.Wpf.Behaviors
{
public class Ioc
{
    public static CommunityToolkit.Mvvm.DependencyInjection.Ioc DefaultIoc { set; get; } = CommunityToolkit.Mvvm.DependencyInjection.Ioc.Default;
    public static Type GetAutoViewModel(DependencyObject obj) => (Type)obj.GetValue(AutoViewModelProperty);
    public static void SetAutoViewModel(DependencyObject obj, Type value) => obj.SetValue(AutoViewModelProperty, value);
    public static readonly DependencyProperty AutoViewModelProperty =
        DependencyProperty.RegisterAttached(
            "AutoViewModel",
            typeof(Type),
            typeof(Ioc),
            new FrameworkPropertyMetadata(null,
                FrameworkPropertyMetadataOptions.NotDataBindable,
                AutoViewModelChanged));

    private static void AutoViewModelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (DesignerProperties.GetIsInDesignMode(d))
            return;
        if (d is FrameworkElement elm && e.NewValue is Type type)
            elm.DataContext = DefaultIoc.GetService(type);
    }
}
}
