   M S B A M L   `   `   `  ���� E  ADeckMancer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:DeckMancer
DeckMancer  /clr-namespace:DeckMancer.WPFDeckMancer.WPF  1clr-namespace:DeckMancer.CoreDeckMancer.Core  R NColorPicker, Version=3.4.1.0, Culture=neutral, PublicKeyToken=1c61eec504ce2276>.clr-namespace:ColorPicker;assembly=ColorPickerColorPicker N JMultiSelectTreeView, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullV>clr-namespace:MultiSelectTreeView;assembly=MultiSelectTreeViewMultiSelectTreeView G CGLWpfControl, Version=4.2.3.0, Culture=neutral, PublicKeyToken=null=.clr-namespace:OpenTK.Wpf;assembly=GLWpfControl
OpenTK.Wpf     DeckMancer.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     *localclr-namespace:DeckMancer     ,wpfclr-namespace:DeckMancer.WPF     .coreclr-namespace:DeckMancer.Core     Fcolorpicker.clr-namespace:ColorPicker;assembly=ColorPicker     Omstv>clr-namespace:MultiSelectTreeView;assembly=MultiSelectTreeView     GglWpfControl.clr-namespace:OpenTK.Wpf;assembly=GLWpfControl     -     � UseLayoutRounding$
  True�� � Title$ 
DeckMancer��$	��450��$	��800�� � Icon$ /DeckMancer.png�� � 	Resources 4   .DeckMancer.WPF.ItemsCountToVisibilityConverter #  ItemsCountToVisibilityConverter.   (DeckMancer.WPF.ByteArrayToImageConverter  ByteArrayToImageConverter)   #DeckMancer.WPF.EnumToArrayConverter  EnumConverter.   (DeckMancer.WPF.ComponentTemplateSelector  ComponentSelector   TransformTemplate �� 
TargetType �� Property �� Value � RowDefinitions  GridBorderStyle	 �� Padding
 � ColumnDefinitions �� Mode �� UpdateSourceTrigger   MeshTemplate
 � Tag  ButtonStyle J� IsHitTestVisible J� 	AllowDrop%  ColorPicker.PortableColorPicker  ColorPickerStyle  DefaultColorPickerStyle#  ColorPicker.PickerControlBase   
ColorState �� Path �� Delay �� DisplayMemberPath  ComboboxStyle �� Source   DeckMancer.Core.WarpMode �� 	Converter �� SelectedItem    DeckMancer.Core.FilterMode �� IsSnapToTickEnabled �� TickFrequency � Minimum � Maximum 	 HorizontalSlider � Value �� TextAlignment �� 
IsReadOnly J� SnapsToDevicePixels    CameraTemplate!   ScriptTemplate��%5  &	        &	      &	 
     &	      7  7  7  7  7  7  7  7  7  7  7  7 	 7  7  7  7           �� TransformTemplate��.R��  ���� '��       " ��.���  � �� �#a111��  � �� �#5333��  � ��
 �������  � ��$ 2q���  � ��$ 60���� '��       " ��.���  � ��
 �������  � ��$ 4 0 4 0q� �� $
��Auto ��� $
��Auto �.+�� .+��� $��3 3 0 0q����#5555$��1q�$��2��$��Stretch=�._�� 8��  .+��� $	 20 0 0 0q�$
��Bold"�
�������$��	Transform��$��Center=��� $��1��$��3 0 0 0q����#5555$��1q�$��2��$��Stretch=�._�� $��Stretch=�
 �� $
��Auto ��� $��* � �� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� $��4 5 5 5q�$��0��$��0��$��Position��$��Center=�� $��0 5 5 5q�$��0��$��1��
 �� $
��Auto ��� $��* ��� $
��Auto ��� $��* ��� $
��Auto ��� $��* �.+��� $��0��$��X:��$��Center=��� $��1������ *	PositionX+$ TwoWay=�$ PropertyChanged=��� $��2��$��Y:��$��Center=��� $��3������ *	PositionY+$ TwoWay=�$ PropertyChanged=��� $��4��$��Z:��$��Center=��� $��5������ *	PositionZ+$ TwoWay=�$ PropertyChanged=��� $��4 0 0 0q�$��1��$��0��$��Rotation��$��Center=�� $��0 0 5 0q�$��1��$��1��
 �� $
��Auto ��� $��* ��� $
��Auto ��� $��* ��� $
��Auto ��� $��* �.+��� $��0��$��X:��$��Center=��� $��1������ *	RotationX+$ TwoWay=�$ PropertyChanged=��� $��2��$��Y:��$��Center=��� $��3������ *	RotationY+$ TwoWay=�$ PropertyChanged=��� $��4��$��Z:��$��Center=��� $��5������ *	RotationZ+$ TwoWay=�$ PropertyChanged=��� $��4 0 0 0q�$��2��$��0��$��Scale��$��Center=�� $��0 5 5 5q�$��2��$��1��
 �� $
��Auto ��� $��* ��� $
��Auto ��� $��* ��� $
��Auto ��� $��* �.+��� $��0��$��X:��$��Center=��� $��1������ *ScaleX+$ TwoWay=�$ PropertyChanged=��� $��2��$��Y:��$��Center=��� $��3������ *ScaleY+$ TwoWay=�$ PropertyChanged=��� $��4��$��Z:��$��Center=��� $��5������ *ScaleZ+$ TwoWay=�$ PropertyChanged=� �� .R��  �� $
��Auto ��� $
��Auto �.+��  ���� '��       " ��.���  � �� �#a111��  � �� �#5333��  � ��
 �������  � ��$ 2q���  � ��$ 60���� '��       " ��.���  � ��
 �������  � ��$ 4 0 4 0q� �� $
��Auto ��� $
��Auto �.+�� .+��� $��3 3 0 0q����#5555$��1q�$��2��$��Stretch=�._�� 8�� 
 �� $
��Auto ��� $��* �.+��� $	 20 0 0 0q�$
��Bold"�
�������$
��Mesh��$��Center=��� -   $
 Mesh��$��2��$��Center=�$��0 1 10 0q�$��Remove��$��Right=�8�� �� $��1��$��3 0 0 0q����#5555$��1q�$��2��$��Stretch=�._�� $��0 0 0 4q�$��Stretch=�
 �� $
��Auto ��� $��* � �� $
��Auto ��� $
��Auto �.+�� -   $��2��$
 True��$
 True�� �� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� $��0��$	 0 0 0 0q�$��Center=����� *
Vertices+�� $��1��$	 0 0 0 0q�$��Center=����� *UVs+�� $��2��$	 0 0 0 0q�$��Center=����� *	Indices+�� -   $��1��$��2��$��0 4 0 0q�$��Set Mesh��8�� � $��1�� ���� '��       " ��.���  � �� �#a111��  � �� �#5333��  � ��
 �������  � ��$ 2q���  � ��$ 60���� '��       " ��.���  � ��
 �������  � ��$ 0 0 10 0q� �� $
��Auto ��� $
��Auto �.+�� .+��� $��3 3 0 0q����#5555$��1q�$��2��$��Stretch=�._�� 8�� 
 �� $
��Auto ��� $��* �.+��� $	 20 0 0 0q�$
��Bold"�
�������$��Material��$��Center=��� $��1��$��3 0 0 0q����#5555$��1q�$��2��$��Stretch=�._�� $��4 4 4 4q�$��Stretch=�
 �� $
��Auto ��� $��* ��� $
��Auto � �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� $��0��$	 0 0 0 0q�$��Color��$��Center=�  $��2�����#7558$��2q�$��20��$��0 0 5 3q�$��1��8  8��  �� $ Color�$ TwoWay=�$ 10���� $��1��$	 0 0 0 0q�$��	Warp Mode��$��Center=��� $��2��$��1��$��0 4 5 4q�$��1��$ .��8�� ���� "  8   �� *
WarpMode+$ TwoWay=��� $��2��$	 0 0 0 0q�$��Filter Mode��$��Center=��� $��2��$��2��$��0 4 5 4q�$��1��$ .��8��	 ���� "  8 
  �� *
FilterMode+$ TwoWay=��� $��3��$	 0 0 0 0q�$��Anisotropic��$��Center=��� $��0 2 0 0q�$��3��$��1��$
 True��$ 2Q�$ 0Q�$ 16Q�8��  �� *AnisotropicMode+�� $��4 2 4 0q�$��20��$ Center=�$
 True��$ False��$��3��$��2������ *AnisotropicMode+$ OneWay=��� $��3��$��Center=�$��4��$	 0 0 0 0q�$��Texture��$��Center=��� -   ���#355a���#5557$��2q�$��3��$��5��$
 True��._��� $��Uniform=�$
  True��$
 True��$��5 5 5 5q�$
��auto��$��Center=�$��Center=�$
��auto��$	��160������ *	Texture+8    �� .R��  ���� '��       " ��.���  � �� �#a111��  � �� �#5333��  � ��
 �������  � ��$ 2q���  � ��$ 60����  � ��$ 0 4 0 0q��� '��       " ��.���  � ��
 �������  � ��$ 4 0 4 0q� �� $
��Auto ��� $
��Auto �.+�� .+��� $��3 3 0 0q����#5555$��1q�$��2��$��Stretch=�._�� 8�� 
 �� $
��Auto ��� $��* �.+��� $	 20 0 0 0q�$
��Bold"�
�������$��Camera��$��Center=��� $��1��$��3 0 0 0q����#5555$��1q�$��2��$��Stretch=�._�� $��0 5 0 5q�$��Stretch=�
 �� $
��Auto ��� $��* ��� $��* � �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� $��0��$	 0 0 0 5q�$
��SIZE��$��Center=��� $��1��$	 0 0 0 0q�$
��NEAR��$��Center=��� $��2��$	 0 0 0 0q�$	��FAR��$��Center=��� $��3��$	 0 0 0 0q�$��Width��$��Center=��� $��4��$	 0 0 0 0q�$��Height��$��Center=��� $��1��$��0��$��0 0 0 5q�$��Center=����� *SIZE+�� $��1��$��1��$��0 0 0 0q�$��Center=����� *NEAR+�� $��1��$��2��$��0 4 0 5q�$��Center=����� *FAR+�� $��1��$��3��$��0 0 0 0q�$��Center=����� *Width+�� $��1��$��4��$��0 0 0 0q�$��Center=����� *Height+! �� .R��  ���� '��       " ��.���  � �� �#a111��  � �� �#5333��  � ��
 �������  � ��$ 2q���  � ��$ 60����  � ��$ 0 4 0 0q��� '��       " ��.���  � ��
 �������  � ��$ 4 0 4 0q� �� $
��Auto ��� $
��Auto �.+�� .+��� $��3 3 0 0q����#5555$��1q�$��2��$��Stretch=�._�� 8�� 
 �� $
��Auto ��� $��* �.+��� $	 20 0 0 0q�$
��Bold"�
�������$��Script��$��Center=��� -   $ Script��$��2��$��Center=�$��0 1 10 0q�$��Remove��$��Right=�8�� �� -   $��1��$��3 0 0 0q����#5555$��1q�$��2��$��Stretch=�$
 True��._�� $��0 5 0 5q�$��Stretch=�
 �� $
��Auto ��� $
��Auto ��� $��* � �� $
��Auto �.+��� $��0��$	 0 0 0 5q�$��Script: ��$��Center=��� $��80��$��1��$��1��$��0 0 0 4q�$��2q����#5333���#a111._��� $	��500��$
��Left=����� *Name+.��� 
 �� $��* � �� $��7* ��� $��3* �.+�� $��0��
 �� $��* ��� $
��Auto ��� $��4* ��� $
��Auto ��� $
��1.5* �.+�� $��0�� �� $
��Auto ��� $��* �.+�� $��0��#��[ .+��� -   " ��Name$" SceneObjectsTabControl��
������ 
������  
 TabItemStyle#��[
 # �� ContentTemplate# �� .R��� .@��� $��Scene Objects����� -	   $ �Name$$ GridSceneObjects��.+�-	  'MultiSelectTreeView.MultiSelectTreeView	  -
   $��TreeViewSceneObject��
������ 
������ $��Disabled=����� *Scenes+����   TreeViewItemSceneObject% �� BasedOn#% [ " Q����� & �� 	Resources&   DarkScrollBar��%   '��       7  �� " ��8%   ' � ContextMenu' �� .@�m� -   $��
GameObject��m� -   $��Scene���� m� -   $��Rename���� m� -   $��Delete�� ( �� DataType#
   DeckMancer.WPF.SceneViewModel) �� ItemsSource* �� Orientation  TextBoxTreeRenameStyle(   "DeckMancer.WPF.GameObjectViewModel��%�  (��       *,
 +)7  7  (�� �     *, +)7  7  �� "( 
 ) �� *
Children+.R��� $* 
Horizontal=�.+��� $��16��$��16��$��-2 0 0 0q����� *Image+8   �� -   $��0 1 0 1q�$��4 0 0 0q����� *Name+.������ 8%  " ��.���� "(  ) �� *
Children+.R��� $* 
Horizontal=�.+��� $��16��$��16��$��10 0 0 0q����� *Image+8   �� -   $��0 2 0 1q�$��4 0 0 0q����� *Name+.������ 8%  " ��.��� $��2��  GridCanvasStyle#��[ .+��� ���� *SelectedItem.Content++ �� ElementName$+ SceneObjectsTabControl��� $��1��  VerticalGridBorderStyle#��[ � $��2�� �� $
��Auto ��� $��* �.+�� $��0��#��[ 
 �� $��* ��� $��* �.+��� -   $" SceneTabControl��
������ 
������ #��[
 # �� .R��� .@��� $��Scene����� -   $$ 	GridScene��.+�  OpenTK.Wpf.GLWpfControl  -   ,  Name$, OpenTkControl���� -   $��1��$��25��$��0 2 0 0q����� " ��.���  � �� �� - �� 
TargetType"- ��.R�� .+��� $��BB��$��2q����#5555���#999._�F� $��5 2 3 2q�"����        � �  � �     6n	
��� � �. �� Stretch$
. Fill=�/ �� Triggers/ P� 0 P� Property0 � ��1 P� Value$
1 True��. ��� 2 �� 
TargetName$2 BB�� � �� �#9444� $��2��#��[ .+��� ���� *SelectedItem.Content+$+ SceneTabControl��� $��3��#��[ � $��4�� �� $
��Auto ��� $��* �.+�� $��0��#��[ .+��� -   $" InspectorTabControl��
������ 
������ #��[
 # �� .R��� .@��� $��	Inspector����� -   $$ GridInspector��.+��� $��Visible=� " ScrollViewerWithDarkScrollBars#��[ .��� 
 �� $��* ��� $
��Auto � �� $��* ��� $
��AUTO �.+��� -   $��ItemsControlComponent��#��[ ���� *
Components+�� $��1��$��2��$��3 0 0 0q�$��1�����#5559� $��1��$��2��y��� *Items.Count+$+ ItemsControlComponent��# [   �� $��* ��� $
��AUTO �.+��� $��0 5 0 5q�$��1�����#5559d� -   $��button��$��4 0 4 5q�$��1��  ToggleButtonStyle#��[ $	��200��$��Add Component��� -   $��PopupAddComponents��$��1��3 � VerticalOffset$3 30��$	��Top=�4 � 	StaysOpen$4 False��5 � PlacementTarget5 �� $+ button������ *	IsChecked+$+ button��.���� $��Center=�$	��200��$	��400�����	#FF373740���� $+ button��$ ActualWidth�$ PropertyChanged=�._�� $��2 2 2 2q� �� $��* �.+��� ���	#FF1E1E28  ListBoxFileStyle#��[ ���� " ��  ListBoxFileItemStyle#% [ .���  �@��
IsSelected$ False����  � |�$ False����  � ��
 �������  � ��$ 1q���  � �� �#5555��  � �� �#5555-   .@��� $
��Mesh���� $��Script��� $��2��#��[ .+��� ���� *SelectedItem.Content+$+ InspectorTabControl��� $��1�� �� $
��Auto ��� $��* �.+�� $��0��#��[ 
 �� $��* ��� $��* �.+�� $��0��.+��� -   $" FileDebugTabControl��
������ 
������ #��[
 # �� .R��� .@��� $��File Manager����� -   $$ GridFileManager�� �� $
��Auto ��� $��* �.+��� $��
2, 2, 2, 1q����	#FF3333446 �� CornerRadius$6 
5, 5, 5, 5��$��1q�._��� -   $��0��
������ 
������   ListBoxUrlFileStyle#��[ 7 �� SelectedIndex7 �� *	LastIndex+���� *FolderItems+���� &   HorizontalDarkScrollBar��%   '��       7  �� " ��8%   ���� .R�� 8 � Orientation$8 
Horizontal=����� .R��� ���� �� -   $��ListBoxFileManager��$��1��$��0,5,0,0q�$
 True������ *	FileItems+' �� .@�m� $��Create��.@�m� -   $��Folder��m� -    $��Script������ .R�� $8 
Horizontal=����� .R�� -!   
������ ' �� .@�m� -"   $��Delete�� �� $
��auto ��� $��* �.+��� $��0��$��48��$��48������ *ImageBuffer+# [ �� $��FileNameTextBox��-#   $��1��$��60��$��32��9 � Cursor$9 Arrow��
�������: �� TextWrapping$
: Wrap=�$��Center=�$ Center=����� *GetFullName+.������   TextBoxRenameStyle#% [ " ��.������ " ��#% [ .���  � �� �� " ��#% [ .�-$   �� $��Debug Console������ -%   ; ��Name$; ListBoxDebugOutput��$��Stretch=�$��2��$��Disabled=�< �� SelectionMode$< Extended=����	#FF1E1E28���	#FF1E1E28���� *Items+���� .R�� 
 �� $
��Auto ��� $
��Auto ��� $��* ��� $
��Auto �.+��� $��0��$��5q�$��Consolas)����	#FFC8A028���� *Date+�� $��1��$��23��= J� Opacity$	= 0.9Q�$	��Top=����� *ImageBuffer+# [ �� $��2��$��5q�$
��Wrap=�$��Consolas)����	#FFC8A028���� *	Message+�� $��3��$��5q�$��Consolas)����	#FFC8A028���� *	Duplicate+���� & ��%   '��       7  �� " ��8%   � $��1��.+��� -&   > ��Name$> ButtonsDebug��$* 
Horizontal=�$��Right=�.+��� $��0 0 5 0q�$��Clear��#��[ �� $��0 0 5 0q�$��ClearAll��#��[ � $��1��#��[ .+��� ���� *SelectedItem.Content+$+ FileDebugTabControl��