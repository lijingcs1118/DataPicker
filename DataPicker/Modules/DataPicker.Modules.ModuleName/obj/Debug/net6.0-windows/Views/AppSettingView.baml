   M S B A M L   `   `   `  ????   DataPicker.Modules.CsvModule7    1DataPicker.Modules.CsvModule.Views.AppSettingView   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      [ WPrism.DryIoc.Wpf, Version=8.1.97.5141, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59T PPrism.Wpf, Version=8.1.97.5141, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59&prismhttp://prismlibrary.com/   5      X TPropertyTools.Wpf, Version=3.1.0.0, Culture=neutral, PublicKeyToken=ea0c9f2b460934d0%pthttp://propertytools.org/wpf  5      !  Prism.Mvvm.ViewModelLocator    AutoWireViewModel  True5       ? 	Resources 5         TabHeaderErrorsStyle ?? Triggers ?? Binding ?? Value ?? Property ?? Value  CategoryControlTemplate1 ?? 
TargetType ? ColumnDefinitions	 ? RowDefinitions
 J? SnapsToDevicePixels  HeaderTemplate1 ?? Orientation  PropertyTools.Wpf.Bitmap   Source  HeaderTemplate2 ?? DataType  PageHeaderTemplate1??%?  &	        &	 ?     &	 _    &	 	    7   &	 ?    ?? 5	   
    5
      ?? 5       6   ?? *HasWarnings+ True6>   .a?5      ??  ? ??
 ? ???6=   5      ?? 5       6   ?? *	HasErrors+ True6<   .a?5      ??  ? ??
 ?  ??6=   5      5      ?? 5   
   " ??6<   .R?5      ? $??0 8 0 0q?6    5      ?? 5      $
??Auto ?6'   ?? 5      	 5      ?? 5      $
??Auto ?6$   ?? 5      $
??Auto ?6$   .+?5      ?? $??Header??6#   $
??True??6:   #
 z
 6U   ?? 5       $??1??6   $??4 3 0 0q?6,   ?? 5!      $??1??6#   $??2??60   5"      5#      ?? 5%   
   .R?5&      ?? $ 
Horizontal=?6   .+?5'        $ ../Images/home.png??6   ?? 5(      $??4 0 0 0q?6-   ??6   ?? 6-   5)      5*      ?? 5,   
   $ pt:Tab??6/   .R?5-      ?? $ 
Horizontal=?6   .+?5.        $ ../Images/chart.png??6   $??0 0 4 0q?69   ?? 5/      $??4 0 0 0q?64   8??  ??6   ?? *Header+6E   50      51      ?? 53   
   .R?54      ?? $ 
Horizontal=?6   .+?55      ?? $??24(?6-   $
??Bold"?6;   $??0 0 8 0q?6M   ??6   ?? 6M     56      $ ../Images/home.png??6   $??Center=?68   57      58      59      .??5:      ? .+?5;   
   $  PropertyTools.Wpf.PropertyGrid     CategoryControlTemplate# [ 5<         CategoryHeaderTemplate# [ 5=         TabHeaderTemplate# [ 5>         TabPageHeaderTemplate# [ 5?         DescriptionIcon$ ../Images/lightbulb.png??5@      $??8q?6E      SelectedObject 5;      ??  ?? Path$	 CSV?5A      5B      5C      