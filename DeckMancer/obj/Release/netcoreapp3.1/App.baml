   M S B A M L   `   `   `  ���� E  ADeckMancer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:DeckMancer
DeckMancer  /clr-namespace:DeckMancer.WPFDeckMancer.WPF  N JMultiSelectTreeView, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullV>clr-namespace:MultiSelectTreeView;assembly=MultiSelectTreeViewMultiSelectTreeView     DeckMancer.App   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     *localclr-namespace:DeckMancer     ,wpfclr-namespace:DeckMancer.WPF     Omstv>clr-namespace:MultiSelectTreeView;assembly=MultiSelectTreeView       �� 	Resources     DarkScrollBarRepeatButton �� 
TargetType �� Property �� Value �� 
TargetType  ScrollBarThumbHor �� 
StartPoint �� EndPoint � Offset � Color  DarkScrollBar	 ]�Name
 ]� IncreaseRepeatButton ]� Thumb ]� DecreaseRepeatButton  HorizontalScrollBarThumbHor  HorizontalDarkScrollBar " ScrollViewerWithDarkScrollBars � ColumnDefinitions � RowDefinitions ��Name �� Orientation �� ScrollableHeight � Maximum �� ViewportHeight �� ViewportSize �� VerticalOffset � Value* �� #ComputedVerticalScrollBarVisibility �� ScrollableWidth �� ViewportWidth �� HorizontalOffset, �� %ComputedHorizontalScrollBarVisibility  CommonButtonStyle  
MouseLeave �� A �� R �� G �� B  	MouseOver 	 BorderBrush  �� Color 
 ButtonStyle! �� BasedOn" ��Name# J� RenderTransformOrigin$ �� Triggers% ]� 
Conditions& �� Property' �� Value( �� 
TargetName) �� ScaleX* �� ScaleY+ N� EnterActions, ��Name- �� 
TargetName. �� TargetProperty	/ �� To0 g� Duration1 N� ExitActions  ToggleButtonStyle  GridCanvasStyle  GridBorderStyle  VerticalGridBorderStyle  TextBoxRenameStyle2 �� Triggers3 �� Binding4 �� RelativeSource5 �� Value6 J� 	AllowDrop7 �� TextAlignment8 P� Property9 P� Value: �� AncestorType  TabItemStyle; �� CornerRadius< �� Padding= � 	Resources> T� Y  ListBoxFileItemStyle  ListBoxFileStyle? �� 	Resources  ListBoxUrlFileStyle-  'System.Windows.Input.KeyboardNavigation@ ��Name  TextBoxTreeRenameStyleA �� PaddingB ^� 
ConditionsC �� Binding  TreeViewItemFocusVisual  
GlyphColor  ExpandCollapseToggleStyle'  !System.Windows.VisualStateManagerD   VisualStateGroups%  System.Windows.VisualStateGroupE   States   System.Windows.VisualStateF   
StoryboardG R� KeyTime  System.Windows.VisibilityH   HiddenI R� ValueJ   VisibleK F�Name  TreeViewItemSceneObjectL �� Mode-  'MultiSelectTreeView.MultiSelectTreeViewM d�NameN �� 	ClickModeO d� 	IsCheckedP �� PathQ ��Name  ComboBoxToggleButton  ComboBoxTextBox  ComboboxStyle  ComboboxItemStyleR J� IsHitTestVisibleS �� SelectionBoxItemT �� SelectionBoxItemTemplateU ��NameV �� 
IsReadOnlyW �� 
IsReadOnlyX �� IsDropDownOpenY � AllowsTransparencyZ J� SnapsToDevicePixels[ � ActualWidth\ �� MaxDropDownHeight] K� IsItemsHost^   DirectionalNavigation_ P� 
SourceName  SliderRepeatButton  SliderRepeatButton1  SliderThumb 
  Slider ! HorizontalSlider " ColorPickerStyle` �� MergedDictionariesa �� Source��%83  &	        &	 �     &	 �    7   7  7   &	     &	 c    7   7  7   &	 �    7  7  &	 �    &	 �    &	 �    &		     &	
 /    7  7 	 7  7  7  &	 �
    7 	 7  7  7  &	 �    &	 �    &	 @    &	 �    &	 �    7  7  &	 ,    7  7  &	 �    7  &	 U    &	 �    &	 �    &	     &	 ,    &	 p    7  7  &	 �$    &	 �%    &	 &    7  7  &	 �+    &	 -    &	 �-    &	 �.    &	  @/    7  7  7  &	! 0    7   &	" �0    �� " ��.���  � ��
 ���� ��  � ��
 ���� ��  � �� �� " ��.R��� #��z��#��z��#��z��._��� #��z��#��z���� " n�.���  �@J�SnapsToDevicePixels$
 True����  �@�OverridesDefaultStyle$
 true����  � ��$ False����  � |�$ false����  � �� �� " n�.R��� #��z��$��0q�$��10��$��	0,0,2,-10q����� $	 1,1&�$	 0,1&�.��� $ 0Q�$
 Gray��� $	 0.2Q�$
 Gray��� $	 0.2Q�$ Transparent���� " ��.���  �@J�SnapsToDevicePixels$
 True����  �@�OverridesDefaultStyle$
 true����  � ��$ 50����  � �� �� " ��.R�� .+�]� $	 
PART_Track��$
��true��
 �� $��ScrollBar.LineDownCommand��8��   n� 8��  �� $��ScrollBar.LineUpCommand��8�� �� " n�.���  �@J�SnapsToDevicePixels$
 True����  �@�OverridesDefaultStyle$
 true����  � ��$ False����  � |�$ false����  � �� �� " n�.R��� #��z��$��0q�$��10��$��0,0,0,2q����� $	 1,1&�$	 1,0&�.��� $ 0Q�$
 Gray��� $	 0.3Q�$
 Gray��� $	 0.3Q�$ Transparent���� " ��.���  �@J�SnapsToDevicePixels$
 True����  �@�OverridesDefaultStyle$
 true����  � ��$ 50����  � �� �� " ��.R�� .+�]� $	 
PART_Track��$��False��
 �� $ ��ScrollBar.LineRightCommand��8��   n� 8��  �� $��ScrollBar.LineLeftCommand��8�� �� " ��.���  � �� �� " ��.R��  �� $��* ��� $
��Auto � �� $��* ��� $
��Auto �.+��� $��0��$��0��#��z���� $ PART_VerticalScrollBar��$��1��$��0��$ Vertical=�# z # z # z #y�z 8��  �� $ PART_HorizontalScrollBar��$��0��$��1��$ 
Horizontal=�# z # z # z #y�z 8�� �� " ��.���  � ��$ 10(���  � ��$ 18����  � ��$ 50����  � ��$ Stretch=���  � ��$ Stretch=���  �@J�Opacity$	 0.7Q���  � ��$ 2q���  � �� �	#FF373740�� $	 255��$ 69��$ 69��$ 69���� $	 255��$	 105��$	 105��$	 105���� $  	#FF5F5F70���� " ��8!   .���  � ��
 �������  � �� �� " ��.R��� $" TemplateBorder��$# 0.5,0.5&�8�� #��z������ 
BtnBgColor��8   ._��� $��Center=�$��Center=�$ ]� % �� & �@J�IsMouseOver$
' True���� & � {�$
' True��./��� $( TemplateBorder�� � z� V� .~��� $
) 1.05Q�$
* 1.05Q�+ �� .���� $, 
EnterMouse��.���� $- 
BtnBgColor��$. Color�8/  $0 0:0:0:0D�1 �� .���� $, 
LeaveMouse��.���� $- 
BtnBgColor��$. Color�8/  $0 0:0:0:0D�]� % �� & � ��$
' True��./��� $( TemplateBorder�� � z� V� .~��� $	) 0.9Q�$	* 0.9Q��� " d�.���  � ��$ Stretch=���  � ��$ Stretch=���  �@J�Opacity$	 0.7Q���  � ��$ 2q���  � �� �	#FF373740��  � ��
 �������  � �� �� " d�.R��� $" TemplateBorder��$# 0.5,0.5&�8��  #��z������ 
BtnBgColor��8   ._��� $��Center=�$��Center=�$ ]� % �� & �@J�IsMouseOver$
' True���� & � {�$
' True��./��� $( TemplateBorder�� � z� V� .~��� $
) 1.05Q�$
* 1.05Q�+ �� .���� $, 
EnterMouse��.���� $- 
BtnBgColor��$. Color�8/  $0 0:0:0:0D�1 �� .���� $, 
LeaveMouse��.���� $- 
BtnBgColor��$. Color�8/  $0 0:0:0:0D�]� % �� & � ��$
' True��./��� $( TemplateBorder�� � z� V� .~��� $	) 0.9Q�$	* 0.9Q��� " �.���  � �� �	#FF1E1E28�� " �.���  � ��$ 20����  � �� �� $ 0.5,1&�$ 0.5,0&�.��� $ 	#FF1E1E28��� $ DarkGray��$ 5Q��� " �.���  � ��$ 5����  � �� �� $ 1,0.5&�$ 0,0.5&�.��� $ 	#FF2E2E38��$ 0Q�� $ 	#FF3E3E48��$	 0.5Q�� $ 	#FF2E2E38��$ 1Q��� " ��2 �� 3 �� *IsKeyboardFocused+4 �� *Self+	5 false.a���  � ��
 ���� ��  � �� �� " ��.R��� #��z��$6 False��$��CharacterEllipsis=�$��Center=�$7 Center=�$
��Wrap=�P� 8 �@J�	IsFocused$9 False��. ���  �@��
IsReadOnly$ False����  � |�$
 True��P� 8 �@J�	IsFocused$
9 True��. ���  � �� �	#55000000�� 3 �� *
IsSelected+4 �� ": ��	5 false.a���  �@��
IsReadOnly$
 True����  � |�$ False���� " ��.���  � �� �	#FF373740��  � ��
 �   ���  � ��$ 0,0,5,0q���  � ��$ 12(���  �@J�SnapsToDevicePixels$
 True����  � �� �� " ��.R��� $
" body��$; 10, 0, 0, 10��._��� $" 
textBorder��$< 	15,0,15,0q�$; 	10,0,0,10�����	#0FFFFFFF._��� #��z��= ���� '��       " ��.���  � ��
 �����$ P� 8 �@J�IsMouseOver$
9 True��. ��� $
( body�� � �� �� 8    P� 8 �@��
IsSelected$
9 True��. ��� $
( body�� � �� �� 8   �� $( 
textBorder�� � z� �� $
* 0.95Q��� $
( body�� � z� T� $> 1Q��� $
( body�� � ��$ 0,2,0,0q��� $
( body�� �@��CornerRadius$ 10,0,0,0���� $( 
textBorder�� �@��CornerRadius$ 10,0,0,0��P� 8 �@��
IsSelected$9 False��. ��� $
( body�� � ��$ 0,2,0,0q��� " ��.���  � �� �� " ��.R��� $" TemplateBorder��#��z��#��z��$# 0.5,0.5&����� 
BtnBgColor��$  Transparent��._��� $��Center=�$��Center=�$ ]� % �� & �@J�IsMouseOver$
' True���� & � {�$
' True��./��� $( TemplateBorder�� � z� V� .~��� $
) 1.05Q�$
* 1.05Q�+ �� .���� $, 
EnterMouse��.���� $- 
BtnBgColor��$. Color�8/   $0 0:0:0:0D�1 �� .���� $, 
LeaveMouse��.���� $- 
BtnBgColor��$. Color�/ Transparent$0 0:0:0:0D�P� 8 �@��
IsSelected$
9 True��. ��� $( TemplateBorder�� � �� �� 8   ��  � ��
 ���� ��  � ��$ 0q��� " ��.���  � ��$ Disabled=���  �@��SelectionMode$ Extended=���  � ��
 ���� ��  � ��
 ���� ? ���� '��       " ��8!   �� " ��.���  � ��$ Center=���  � ��$ Center=���  �@ DirectionalNavigation$
 None=���  � ��$ False����  �@�FocusVisualStyle Z� ��  � ��$ 0,0,5,0q���  � �� �� " ��.R�� $# 0.5,0.5&�
������  �� $
��auto ��� $
��auto �.+��� $
" item��$; 
5, 5, 5, 5��._��� $@ TextUrl������ " ��.���  � ��
 ������� 
�������$��5,0,0,0q�$��1��$��\��$ P� 8 �@J�IsMouseOver$
9 True��. ��� $( TextUrl�� � ��
 �������  �@�Cursor$
 Hand��P� 8 �@��
IsSelected$
9 True��. ��� $( TextUrl�� � ��
 ������� $( TextUrl�� � ��$
 Bold"��� " ��.�-   2 �� 3 �� *	IsEditing+	5 false.a���  � ��
 ���� ��  � �� �� " ��.R��� $A 0 2 0 1q�#��z��
�������$6 False��$��CharacterEllipsis=�$��Center=�$7 Center=�$
��Wrap=��� 3 �� *	IsFocused+4 �� ": Q�	5 false.a���  �@��
IsReadOnly$ False����  � |�$
 True���� 3 �� *	IsFocused+4 �� ": Q�5 true.a���  � |�$
 True��^� B �� C �� *	IsFocused+4 �� ": Q�' true�� C �� *
IsSelected+4 �� ": Q�' true�� C �� *	IsEditing+' true.0���  � �� �	#55000000��  �@+�FocusedElement �� 4 �� *Self+�� 3 �� *
IsSelected+4 �� ": Q�	5 false.a���  �@��
IsReadOnly$
 True����  � |�$ False���� .���  � �� �� .R��� ��	#AAAA���� " d�.���  � |�$ False����  � �� �� " d�.R�� $��15��$��13��
������ D   CheckStates��.E   Checked��.F �� .��T� $. (UIElement.Visibility)�$- 	Collapsed��..�i� $G 0��#I ZH T� $. (UIElement.Visibility)�$- Expanded��..�i� $G 0��#I ZJ   	Unchecked��  Indeterminate��.+�F� $K 	Collapsed��$
��Left=�$��Center=�$��1,1,1,1q�"���� 	=     р��  	= � 	=  z 	���� #  �  F� $K Expanded��$
��Left=�$��Center=�$��3,0,0,1q�"����     	= � z  	= � 	= ��� 	$y�Hidden=����� #  �  �� " Q�? ���� $  Transparent��(��       * SystemColors.HighlightBrushKey+)�� $  Transparent��(��       *1/SystemColors.InactiveSelectionHighlightBrushKey+).���  �@Q�
IsExpanded �� *
IsExpanded+$L TwoWay=���  �@ IsItemSelected �� *
IsSelected+$L TwoWay=���  � ��
 �������  � ��$ Normal"���  � ��$ 0 0 0 0q���  �@�FocusVisualStyle8   ��  � �� �� " Q�.R��� .+��� $" Bd��#��z��#��z��#��z��#< z��._��  �� $��19 ��� .+�d� $M Expander��$��0��8�� $N Press=�O �� $P 
IsExpanded�4 �� *TemplatedParent+�� $@ PART_Header��$��1��$��Stretch=�$��Stretch=�$��Header���� $Q 	ItemsHost��$��0 0 0 0q�$ P� 8 �@Q�
IsExpanded$9 false��. ��� $( 	ItemsHost�� � y�$ 	Collapsed=�P� 8 �@��HasItems$9 false��. ��� $( Expander�� � y�$ Hidden=�]� % �� & � ��$' false���� & � ��$
' Auto��./��� $( PART_Header�� � ��$ 75��]� % �� & � ��$' false���� & � ��$
' Auto��./��� $( PART_Header�� � ��$ 19��P� 8 �@ IsItemSelected$
9 true��. ��� $( Bd�� � �� �#AAAA�� # � �� � ��P� 8 � {�$9 false��. ��� # � �� � ��2 P� 8 �@J�IsMouseOver$
9 True��. ���  � �� �#5555�� " d�.R��  �� �� $��20 �.+��� $" Border��$��2��$; 0�����	#FF3F3F3F���	#FF97A0A5$��1q��� $��0��$; 0��$��1q����	#FF3F3F3F���	#FF97A0A5$��0,0,1,0q�F� $K Arrow��$��1��
�������$��Center=�$��Center=�=����        �    �� � 	= ��[ � z �� � z     � 	=  	= 	�� " ��.R��� $" PART_ContentHost��$|�False��#��z���� " ��.���  � ��# �  ��  �@J�SnapsToDevicePixels$
 true����  �@�OverridesDefaultStyle$
 true����  � ��$
 Auto=���  � ��$
 Auto=���  � ��$
 true����  � ��$ 15����  � ��
 �������  � �� �� " ��.R�� .+�d� $��ToggleButton��8��  $��2��$|�false��$N Press=�O �� $P IsDropDownOpen�$L TwoWay=�4 �� *TemplatedParent+�� $��ContentSite��$R False��#��zS #��zT #��z��$��3,3,23,3q�$��Center=�$
��Left=��� $U PART_EditableTextBox��8�� $
��Left=�$��Center=�$��3,3,23,3q�$
|�True�����	#FF3F3F3F
��� � �$y�Hidden=�#W zV ��Z� � $��Popup��$��Bottom=�#��zX $
Y True��$|�False��$��Slide=�.��� $��DropDown��$
Z True��#��z[ #��z\ .+��� $" DropDownBorder�����	#FF3F3F3F$��1q����#888888�� $��4,6,4,6q�$
Z True��.���� $
] True��$^ 	Contained=�$ P� 8 �@��HasItems$9 false��. ��� $( DropDownBorder�� � ��$ 95��P� 8 � {�$9 false��. ���  � �� �#888888P� 8 �@��
IsGrouping$
9 true��. ���  � ��$ false��P� $_ Popup��8 �@�AllowsTransparency$
9 true��. ��� $( DropDownBorder�� �@��CornerRadius$ 0���� $( DropDownBorder�� � ��$ 0,2,0,0q�P� 8 �@��
IsEditable$
9 true��. ���  � ��$ false���� $( PART_EditableTextBox�� � y�$ Visible=��� $( ContentSite�� � y�$ Hidden=�2 �� " ��.���  �@J�SnapsToDevicePixels$
 true����  � ��
 �������  �@�OverridesDefaultStyle$
 true����  � �� �� " ��.R��� $��Border��$< 2q�$
Z true��._��� $ P� 8 �@��IsHighlighted$
9 true��. ��� $( Border�� � �� �	#FF4F4F4FP� 8 � {�$9 false��. ���  � �� �#888888�� " ��.���  �@J�SnapsToDevicePixels$
 true����  �@�OverridesDefaultStyle$
 true����  � ��$ false����  � |�$ false����  � �� �� " ��.R��� $��1q�
���   �
���   �$��3���� " ��.���  �@J�SnapsToDevicePixels$
 true����  �@�OverridesDefaultStyle$
 true����  � �� �� " ��.R��� $
Z True��
�������$��0q�$��3���� " n�.���  �@J�SnapsToDevicePixels$
 true����  �@�OverridesDefaultStyle$
 true����  � �� �� " n�.R�A� $��10��$��10��
��������� " ��.R��  �� $
��Auto ��� $
��Auto �#��z���� $
��Auto �.+�]� $	 
PART_Track��$��1�� �� 8��  $��Slider.DecreaseLarge�� n� 8�� 
 �� 8�� $��Slider.IncreaseLarge���� " ��.���  � |�$ False����  �@J�SnapsToDevicePixels$
 true����  �@�OverridesDefaultStyle$
 true��2 P� 8 �@��Orientation$9 
Horizontal=�. ���  � ��$ 21����  � ��$ 20����  � ��8   �� ` �� $Va Ppack://application:,,,/ColorPicker;component/Styles/DefaultColorPickerStyle.xaml?�